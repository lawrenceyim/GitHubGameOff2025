using System.Collections.Generic;
using Godot;
using InputSystem;
using RepositorySystem;
using ServiceSystem;

public partial class MainLevel : Node2D, IInputState {
    [Export]
    private AnimatedSprite2D _player;

    [Export]
    private int _leftXBound;

    [Export]
    private int _rightXBound;

    [Export]
    private int _ySpawnPosition;

    [Export]
    private Area2D _outOfBoundsArea;

    private CollectibleManager _collectibleManager;
    private float _horizontalMoveSpeed = 10; // Per physic tick
    private const string _moveLeft = "A";
    private const string _moveRight = "D";
    private ServiceLocator _serviceLocator;

    private Dictionary<string, bool> _keyPressed = new() {
        { _moveLeft, false },
        { _moveRight, false }
    };

    public override void _Ready() {
        InstantiateLevel();
    }

    public void ProcessInput(InputEventDto dto) {
        if (dto is KeyDto keyEventDto) {
            _keyPressed[keyEventDto.Identifier] = keyEventDto.Pressed;
        }
    }

    public override void _Process(double delta) {
        base._Process(delta);
        // update Horse UI
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);

        MovePlayer();

        // move horse left and right based on input
        // move all obstacles, shrimps, and power ups down the screen
    }

    private void InstantiateLevel() {
        _serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        InputStateMachine inputStateMachine = _serviceLocator.GetService<InputStateMachine>(ServiceName.InputStateMachine);
        inputStateMachine?.SetState(this);

        RepositoryLocator repositoryLocator = _serviceLocator.GetService<RepositoryLocator>(ServiceName.RepositoryLocator);
        ShrimpRepository shrimpRepository = repositoryLocator.GetRepository<ShrimpRepository>(RepositoryName.Shrimp);
        PackedSceneRepository packedSceneRepository = repositoryLocator.GetRepository<PackedSceneRepository>(RepositoryName.PackedScene);
        PackedScene shrimpPackedScene = packedSceneRepository.GetPackedScene(PackedSceneId.Shrimp);
        _collectibleManager = _serviceLocator.GetService<CollectibleManager>(ServiceName.CollectibleManager);
        _collectibleManager?.Initialize(shrimpRepository, shrimpPackedScene, _leftXBound, _rightXBound, _ySpawnPosition);
        
        _outOfBoundsArea.AreaEntered += DestroyOutOfBoundsObject;
    }

    private void DestroyOutOfBoundsObject(Area2D area) {
        if (area.GetParent() is Shrimp shrimp) {
            _collectibleManager.DestroyShrimp(shrimp.Id());
        }
    }

    private void MovePlayer() {
        Vector2 newPosition = Vector2.Zero;
        newPosition.X = (_keyPressed[_moveLeft] ? -1 : 0) + (_keyPressed[_moveRight] ? 1 : 0);
        newPosition *= _horizontalMoveSpeed;
        newPosition += _player.Position;
        if (newPosition.X > _rightXBound) {
            newPosition.X = _rightXBound;
        }
        else if (newPosition.X < _leftXBound) {
            newPosition.X = _leftXBound;
        }

        _player.Position = newPosition;
    }
}