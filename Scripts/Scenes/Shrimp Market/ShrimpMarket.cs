using Godot;

public partial class ShrimpMarket : Node2D {
    [Export]
    private Sprite2D _freshShrimpSprite;

    [Export]
    private Sprite2D _staleShrimpSprite;

    [Export]
    private Sprite2D _grossShrimpSprite;
    
    [Export]
    private Area2D _freshShrimpArea2D;

    [Export]
    private Area2D _staleShrimpArea2D;

    [Export]
    private Area2D _grossShrimpArea2D;

    private Color _highlightColor = new Color(1, 1, 0, 1);
    private Color _regularColor = new Color(1, 1, 1, 1);

    public override void _Ready() {
        _freshShrimpArea2D.MouseEntered += () => _Highlight(_freshShrimpSprite, true);
        _staleShrimpArea2D.MouseEntered += () => _Highlight(_staleShrimpSprite, true);
        _grossShrimpArea2D.MouseEntered += () => _Highlight(_grossShrimpSprite, true);
        _freshShrimpArea2D.MouseExited += () => _Highlight(_freshShrimpSprite, false);
        _staleShrimpArea2D.MouseExited += () => _Highlight(_staleShrimpSprite, false);
        _grossShrimpArea2D.MouseExited += () => _Highlight(_grossShrimpSprite, false);
    }

    private void _Highlight(Sprite2D sprite, bool highlight) {
        sprite.SelfModulate = highlight ? _highlightColor : _regularColor;
    }
}