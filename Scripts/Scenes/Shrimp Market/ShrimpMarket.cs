using System;
using Godot;
using InputSystem;
using ServiceSystem;

public partial class ShrimpMarket : Node2D, ITick, IInputState {
    [Export]
    private Sprite2D _freshShrimpSprite;

    [Export]
    private Sprite2D _staleShrimpSprite;

    [Export]
    private Sprite2D _grossShrimpSprite;

    [Export]
    private Button _freshShrimpButton;

    [Export]
    private Button _staleShrimpButton;

    [Export]
    private Button _grossShrimpButton;

    [Export]
    private Label _priceLabel;

    private Color _highlightColor = new Color(1, 1, 0, 1);
    private Color _regularColor = new Color(1, 1, 1, 1);
    private InputStateMachine _inputStateMachine;
    private int _shrimpPrice;

    public override void _Ready() {
        _InitializeDependencies();
        _InitializeSignals();

        _inputStateMachine.SetState(this);
        _shrimpPrice = _GetShrimpPrice();
        _priceLabel.Text = $"Today's Price\n{_shrimpPrice}";
    }

    public override void _ExitTree() {
        _inputStateMachine.SetState(null);
    }

    public void PhysicsTick(double delta) {
        throw new System.NotImplementedException();
    }

    public void ProcessInput(InputEventDto dto) {
        // switch (dto) {
        //     case MouseButtonPressedDto mouseButtonPressed:
        //         _SellShrimp(_GetShrimpType());
        //         break;
        // }
    }

    private void _SellShrimp(ShrimpType type) {
        double modifier = type switch {
            ShrimpType.Fresh => 2,
            ShrimpType.Stale => 1,
            _ => .5
        };

        int profit = (int)(_shrimpPrice * modifier);
        // Empty shrimp
        // Add profit to player money
    }

    private void _Highlight(Sprite2D sprite, bool highlight) {
        sprite.SelfModulate = highlight ? _highlightColor : _regularColor;
    }

    private void _HighlightObject(ShrimpType type, bool hovered) {
        switch (type) {
            case ShrimpType.Fresh:
                _Highlight(_freshShrimpSprite, hovered);
                break;
            case ShrimpType.Stale:
                _Highlight(_staleShrimpSprite, hovered);
                break;
            case ShrimpType.Gross:
                _Highlight(_grossShrimpSprite, hovered);
                break;
        }
    }

    private int _GetShrimpPrice() {
        Random random = new Random();
        int minPrice = 10;
        int maxPrice = 100;
        return random.Next(minPrice, maxPrice + 1);
    }

    private void _InitializeDependencies() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _inputStateMachine = serviceLocator.GetService<InputStateMachine>(ServiceName.InputStateMachine);
    }

    private void _InitializeSignals() {
        _freshShrimpButton.MouseEntered += () => _HighlightObject(ShrimpType.Fresh, true);
        _freshShrimpButton.MouseExited += () => _HighlightObject(ShrimpType.Fresh, false);
        _freshShrimpButton.Pressed += () => _SellShrimp(ShrimpType.Fresh);

        _staleShrimpButton.MouseEntered += () => _HighlightObject(ShrimpType.Stale, true);
        _staleShrimpButton.MouseExited += () => _HighlightObject(ShrimpType.Stale, false);
        _staleShrimpButton.Pressed += () => _SellShrimp(ShrimpType.Stale);

        _grossShrimpButton.MouseEntered += () => _HighlightObject(ShrimpType.Gross, true);
        _grossShrimpButton.MouseExited += () => _HighlightObject(ShrimpType.Gross, false);
        _grossShrimpButton.Pressed += () => _SellShrimp(ShrimpType.Gross);
    }
}