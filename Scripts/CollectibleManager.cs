using System;
using System.Collections.Generic;
using Godot;

public partial class CollectibleManager : Node {
    [Export]
    private PackedScene _shrimp;

    private int _leftXBound;
    private int _rightXBound;
    private int _ySpawnPosition;

    private ulong _collectibleId = 0;
    private Random _random = new Random();


    private int _shrimpSpawnMinTickDelay = 100;
    private int _shrimpSpawnMaxTickDelay = 500;
    private int _shrimpSpawnTicksLeft = 0;
    private float _shrimpFallSpeed = 2f;

    private Dictionary<ulong, Shrimp> _shrimps = new();

    public override void _Ready() { }

    public void Initialize(int leftXBound, int rightXBound, int ySpawnPosition) {
        _leftXBound = leftXBound;
        _rightXBound = rightXBound;
        _ySpawnPosition = ySpawnPosition;
    }
    
    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        SpawnShrimp();
        MoveShrimps();
    }
    
    public void DestroyShrimp(ulong shrimpId) {
        Shrimp shrimp = _shrimps[shrimpId];
        _shrimps.Remove(shrimpId);
        shrimp.CallDeferred(nameof(QueueFree));
    }

    private void MoveShrimps() {
        foreach (Shrimp shrimp in _shrimps.Values) {
            shrimp.Position = new  Vector2(shrimp.Position.X, shrimp.Position.Y + _shrimpFallSpeed);
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
        _shrimps[shrimp.Id()] = shrimp;
        AddChild(shrimp);
    }
}