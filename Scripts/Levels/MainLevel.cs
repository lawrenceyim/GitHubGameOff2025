using System.Collections.Generic;
using Godot;
using Godot.NativeInterop;
using InputSystem;
using RepositorySystem;
using ServiceSystem;

public partial class MainLevel : Node2D, IInputState, ITick {
	[Export]
	private AnimatedSprite2D _player;

	[Export]
	private Area2D _playerHitBox;

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

	private const string _moveLeft = "A";
	private const string _moveRight = "D";
	private const string _pause = "Escape";

	private GameClock _gameClock;
	private int _ticksLeftInWave;
	private PlayerDataService _playerDataService;
	private CollectibleManager _collectibleManager;
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
			if (keyEventDto.Identifier == _pause && keyEventDto.Pressed) {
				GD.Print("Pause pressed");
				_gameClock.SetPauseState(!_gameClock.IsPaused());
			}

			_keyPressed[keyEventDto.Identifier] = keyEventDto.Pressed;
		}
	}

	public override void _ExitTree() {
		base._ExitTree();
		_gameClock.RemoveActiveScene(GetInstanceId());
		_gameClock.RemoveActiveScene(_collectibleManager.GetInstanceId());
	}

	public void PhysicsTick(double delta) {
		_MovePlayer();
		_TickDownTimer();
		_collectibleManager.PhysicsTick(delta);
	}

	private void _InstantiateLevel() {
		_serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
		_playerDataService = _serviceLocator.GetService<PlayerDataService>(ServiceName.PlayerData);
		_gameClock = _serviceLocator.GetService<GameClock>(ServiceName.GameClock);
		RepositoryLocator repositoryLocator = _serviceLocator.GetService<RepositoryLocator>(ServiceName.RepositoryLocator);
		ShrimpRepository shrimpRepository = repositoryLocator.GetRepository<ShrimpRepository>(RepositoryName.Shrimp);
		PackedSceneRepository packedSceneRepository = repositoryLocator.GetRepository<PackedSceneRepository>(RepositoryName.PackedScene);
		PackedScene shrimpPackedScene = packedSceneRepository.GetPackedScene(PackedSceneId.Shrimp);
		InputStateMachine inputStateMachine = _serviceLocator.GetService<InputStateMachine>(ServiceName.InputStateMachine);

		inputStateMachine?.SetState(this);

		_collectibleManager = new CollectibleManager();
		AddChild(_collectibleManager);
		_collectibleManager?.Initialize(shrimpRepository, shrimpPackedScene, _collectibleLeftXBound,
			_collectibleRightXBound, _collectibleYSpawnPosition, _player.Position.Y);
		_gameClock.AddActiveScene(this, GetInstanceId());
		_collectibleOutOfBoundsArea.AreaEntered += _DestroyCollectible;
		_ticksLeftInWave = _secondsPerWave * Engine.PhysicsTicksPerSecond;
		_playerHitBox.AreaEntered += _HandlePlayerCollision;
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


	private void _HandlePlayerCollision(Area2D area) {
		if (area.GetParent() is Shrimp shrimp) {
			_playerDataService.AddShrimp(ShrimpType.Fresh, shrimp.GetAmount());
			_collectibleManager.DestroyShrimp(shrimp.GetId());
			GD.Print($"Caught {shrimp.GetAmount()}. Fresh shrimp count: {_playerDataService.GetShrimp(ShrimpType.Fresh)}");
		}
	}
}
