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
    private Area2D _freshShrimpArea2D;

    [Export]
    private Area2D _staleShrimpArea2D;

    [Export]
    private Area2D _grossShrimpArea2D;

    private Color _highlightColor = new Color(1, 1, 0, 1);
    private Color _regularColor = new Color(1, 1, 1, 1);
    private HoveredObject _hoveredObject = HoveredObject.None;
    private InputStateMachine _inputStateMachine;

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _inputStateMachine = serviceLocator.GetService<InputStateMachine>(ServiceName.InputStateMachine);
        _inputStateMachine.SetState(this);
        
        _freshShrimpArea2D.MouseEntered += () => _InitializeHighlight(HoveredObject.FreshShrimp, true);
        _staleShrimpArea2D.MouseEntered += () => _InitializeHighlight(HoveredObject.StaleShrimp, true);
        _grossShrimpArea2D.MouseEntered += () => _InitializeHighlight(HoveredObject.GrossShrimp, true);
        _freshShrimpArea2D.MouseExited += () =>  _InitializeHighlight(HoveredObject.FreshShrimp, false);
        _staleShrimpArea2D.MouseExited += () =>  _InitializeHighlight(HoveredObject.StaleShrimp, false);
        _grossShrimpArea2D.MouseExited += () =>  _InitializeHighlight(HoveredObject.GrossShrimp, false);
    }

    public override void _ExitTree() {
        _inputStateMachine.SetState(null);
    }
    
    public void PhysicsTick(double delta) {
        throw new System.NotImplementedException();
    }
    
    public void ProcessInput(InputEventDto dto) {
                
    }

    private void _Highlight(Sprite2D sprite, bool highlight) {
        sprite.SelfModulate = highlight ? _highlightColor : _regularColor;
    }

    private void _InitializeHighlight(HoveredObject obj, bool hovered) {
        switch (obj) {
            case HoveredObject.FreshShrimp:
                _Highlight(_freshShrimpSprite, hovered);
                break;
            case HoveredObject.StaleShrimp:
                _Highlight(_staleShrimpSprite, hovered);
                break;
            case HoveredObject.GrossShrimp:
                _Highlight(_grossShrimpSprite, hovered);
                break;
        }

        _hoveredObject = hovered ? obj : HoveredObject.None;
    }

    private enum HoveredObject {
        None,
        FreshShrimp,
        StaleShrimp,
        GrossShrimp,
    }
}