using System;
using System.Collections.Generic;
using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class CollectibleManager : Node, IService {
    private PackedScene _shrimp;
    private ShrimpRepository _shrimpRepository;
    private int _leftXBound;
    private int _rightXBound;
    private int _ySpawnPosition;

    private ulong _collectibleId = 0;
    private readonly Random _random = new();

    private int _shrimpSpawnMinTickDelay = 20;
    private int _shrimpSpawnMaxTickDelay = 80;
    private int _shrimpSpawnTicksLeft = 0;
    private int _shrimpMoveSpeed = 1;
    private float _shrimpTravelDistance;

    public void Initialize(ShrimpRepository shrimpRepository, PackedScene shrimp, int leftXBound,
        int rightXBound, int ySpawnPosition, float playerYPosition) {
        _shrimpRepository = shrimpRepository;
        _shrimp = shrimp;
        _leftXBound = leftXBound;
        _rightXBound = rightXBound;
        _ySpawnPosition = ySpawnPosition;
        _shrimpTravelDistance = playerYPosition - _ySpawnPosition;
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        SpawnShrimp();
        MoveShrimp();
    }

    public void DestroyShrimp(ulong shrimpId) {
        _shrimpRepository.RemoveShrimp(shrimpId);
    }

    private void MoveShrimp() {
        Dictionary<ulong, Shrimp>.ValueCollection shrimps = _shrimpRepository.GetShrimps();
        foreach (Shrimp shrimp in shrimps) {
            shrimp.Position = new Vector2(shrimp.Position.X, shrimp.Position.Y + _shrimpMoveSpeed);
            float scale = Math.Min((shrimp.Position.Y - _ySpawnPosition) / _shrimpTravelDistance, 1);
            shrimp.Scale = new Vector2(scale, scale);
        }
    }

    private void SpawnShrimp() {
        _shrimpSpawnTicksLeft--;

        if (_shrimpSpawnTicksLeft > 0) {
            return;
        }

        _shrimpSpawnTicksLeft = _random.Next(_shrimpSpawnMinTickDelay, _shrimpSpawnMaxTickDelay);

        Shrimp shrimp = (Shrimp)_shrimp.Instantiate();
        shrimp.Initialize(_collectibleId++);
        shrimp.Position = new Vector2(_random.Next(_leftXBound, _rightXBound), _ySpawnPosition);
        _shrimpRepository.AddShrimp(shrimp.Id(), shrimp);
        AddChild(shrimp);
    }
}