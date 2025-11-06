using System.Collections.Generic;
using Godot;
using InputSystem;
using RepositorySystem;
using ServiceSystem;

public partial class MainLevel : Node2D, IInputState {
    [Export]
    private AnimatedSprite2D _player;

    [Export]
    private int _collectibleLeftXBound;

    [Export]
    private int _collectibleRightXBound;

    [Export]
    private int _collectibleYSpawnPosition;

    [Export]
    private Area2D _collectibleOutOfBoundsArea;

    [Export]
    private int _playerLeftXBound;

    [Export]
    private int _playerRightXBound;
    
    [Export]
    private float _horizontalMoveSpeed; // Per physic tick

    [Export]
    private int _secondsPerWave; 

    private int _ticksLeftInWave;
    
    private CollectibleManager _collectibleManager;
    private const string _moveLeft = "A";
    private const string _moveRight = "D";
    private ServiceLocator _serviceLocator;

    private Dictionary<string, bool> _keyPressed = new() {
        { _moveLeft, false },
        { _moveRight, false }
    };

    public override void _Ready() {
        _InstantiateLevel();
    }

    public void ProcessInput(InputEventDto dto) {
        if (dto is KeyDto keyEventDto) {
            _keyPressed[keyEventDto.Identifier] = keyEventDto.Pressed;
        }
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);

        _MovePlayer();
        _TickDownTimer();
    }

    private void _InstantiateLevel() {
        _serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        InputStateMachine inputStateMachine = _serviceLocator.GetService<InputStateMachine>(ServiceName.InputStateMachine);
        inputStateMachine?.SetState(this);

        RepositoryLocator repositoryLocator = _serviceLocator.GetService<RepositoryLocator>(ServiceName.RepositoryLocator);
        ShrimpRepository shrimpRepository = repositoryLocator.GetRepository<ShrimpRepository>(RepositoryName.Shrimp);
        PackedSceneRepository packedSceneRepository = repositoryLocator.GetRepository<PackedSceneRepository>(RepositoryName.PackedScene);
        PackedScene shrimpPackedScene = packedSceneRepository.GetPackedScene(PackedSceneId.Shrimp);
        _collectibleManager = _serviceLocator.GetService<CollectibleManager>(ServiceName.CollectibleManager);
        _collectibleManager?.Initialize(shrimpRepository, shrimpPackedScene, _collectibleLeftXBound,
            _collectibleRightXBound, _collectibleYSpawnPosition, _player.Position.Y);

        _collectibleOutOfBoundsArea.AreaEntered += _DestroyCollectible;
        
        _ticksLeftInWave = _secondsPerWave * Engine.PhysicsTicksPerSecond;
    }

    private void _TickDownTimer() {
        _ticksLeftInWave--;
        if (_ticksLeftInWave == 0) {
            GD.Print("End of wave");
            // Go to end of day screen
        }
    }

    private void _DestroyCollectible(Area2D area) {
        if (area.GetParent() is Shrimp shrimp) {
            _collectibleManager.DestroyShrimp(shrimp.GetId());
        }
    }

    private void _MovePlayer() {
        Vector2 newPosition = Vector2.Zero;
        newPosition.X = (_keyPressed[_moveLeft] ? -1 : 0) + (_keyPressed[_moveRight] ? 1 : 0);
        newPosition *= _horizontalMoveSpeed;
        newPosition += _player.Position;
        if (newPosition.X > _playerRightXBound) {
            newPosition.X = _playerRightXBound;
        }
        else if (newPosition.X < _playerLeftXBound) {
            newPosition.X = _playerLeftXBound;
        }

        _player.Position = newPosition;
    }
}