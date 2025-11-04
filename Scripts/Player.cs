using EventSystem;
using Godot;
using ServiceSystem;

public partial class Player : AnimatedSprite2D {
	[Export]
	private Area2D _hitbox;
	
	public override void _Ready() {
		base._Ready();
		_hitbox.AreaEntered += HandleCollision;
	}

	private void HandleCollision(Area2D area) {
		if (area.GetParent() is Shrimp shrimp) {
		}
	}
}
