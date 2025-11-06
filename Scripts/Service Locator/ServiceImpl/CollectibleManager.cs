using System;
using System.Collections.Generic;
using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class CollectibleManager : Node2D, IService, ITick {
    private readonly Random _random = new();

    private GameClock _gameClock;
    private PackedScene _shrimp;
    private ShrimpRepository _shrimpRepository;
    private float _shrimpSpawnDistanceFromPlayer;
    private int _leftXBound;
    private int _rightXBound;
    private int _ySpawnPosition;
    private int _shrimpSpawnMinTickDelay = 20;
    private int _shrimpSpawnMaxTickDelay = 80;
    private int _shrimpSpawnTicksLeft = 0;
    private int _shrimpMoveSpeed = 1;
    private int _minShrimpAmountInclusive = 1;
    private int _maxShrimpAmountExclusive = 10;
    private ulong _collectibleId = 0;

    public void Initialize(ShrimpRepository shrimpRepository, PackedScene shrimp, int leftXBound,
        int rightXBound, int ySpawnPosition, float playerYPosition) {
        YSortEnabled = true;
        _shrimpRepository = shrimpRepository;
        _shrimp = shrimp;
        _leftXBound = leftXBound;
        _rightXBound = rightXBound;
        _ySpawnPosition = ySpawnPosition;
        _shrimpSpawnDistanceFromPlayer = playerYPosition - _ySpawnPosition;
    }

    public void Reset() {
        _shrimpRepository.RemoveAllShrimps();
        _shrimpSpawnTicksLeft = 0;
    }
    
    public void PhysicsTick(double delta) {
        _SpawnShrimp();
        _MoveShrimp();
    }

    public void DestroyShrimp(ulong shrimpId) {
        _shrimpRepository.RemoveShrimp(shrimpId);
    }

    private void _MoveShrimp() {
        Dictionary<ulong, Shrimp>.ValueCollection shrimps = _shrimpRepository.GetShrimps();
        foreach (Shrimp shrimp in shrimps) {
            shrimp.Position = new Vector2(shrimp.Position.X, shrimp.Position.Y + _shrimpMoveSpeed);
            float scale = Math.Min((shrimp.Position.Y - _ySpawnPosition) / _shrimpSpawnDistanceFromPlayer, 1);
            shrimp.Scale = new Vector2(scale, scale);
        }
    }

    private void _SpawnShrimp() {
        _shrimpSpawnTicksLeft--;

        if (_shrimpSpawnTicksLeft > 0) {
            return;
        }

        _shrimpSpawnTicksLeft = _random.Next(_shrimpSpawnMinTickDelay, _shrimpSpawnMaxTickDelay);

        Shrimp shrimp = (Shrimp)_shrimp.Instantiate();
        shrimp.YSortEnabled = true;
        shrimp.Initialize(_collectibleId++, _random.Next(_minShrimpAmountInclusive, _maxShrimpAmountExclusive));
        shrimp.Position = new Vector2(_random.Next(_leftXBound, _rightXBound), _ySpawnPosition);
        _shrimpRepository.AddShrimp(shrimp.GetId(), shrimp);
        AddChild(shrimp);
    }
}