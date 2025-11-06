using Godot;
using ServiceSystem;

public partial class Player : AnimatedSprite2D {
    [Export]
    private Area2D _hitbox;

    private ServiceLocator _serviceLocator;
    private PlayerDataService _playerDataService;
    private CollectibleManager _collectibleManager;
    
    public override void _Ready() {
        base._Ready();
        _hitbox.AreaEntered += HandleCollision;
        _serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _playerDataService = _serviceLocator.GetService<PlayerDataService>(ServiceName.PlayerData);
        _collectibleManager =  _serviceLocator.GetService<CollectibleManager>(ServiceName.CollectibleManager);
    }

    private void HandleCollision(Area2D area) {
        if (area.GetParent() is Shrimp shrimp) {
            // _playerDataService.
            _playerDataService.AddShrimp(ShrimpType.Fresh, 1);
            _collectibleManager.DestroyShrimp(shrimp.Id());
            GD.Print($"Fresh shrimp count: {_playerDataService.GetShrimp(ShrimpType.Fresh)}");
        }
    }
}