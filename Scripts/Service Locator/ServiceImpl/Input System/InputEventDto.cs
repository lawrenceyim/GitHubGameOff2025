using Godot;

namespace InputSystem {
    public abstract class InputEventDto { }

    public class KeyDto : InputEventDto {
        public readonly string Identifier;
        public readonly bool Pressed;

        public KeyDto(string identifier, bool pressed) {
            Identifier = identifier;
            Pressed = pressed;
        }
    }

    public class MouseMotionDto : InputEventDto {
        public readonly Vector2 Position;
        public readonly Vector2 Relative;

        public MouseMotionDto(Vector2 position, Vector2 relative) {
            Position = position;
            Relative = relative;
        }
    }

    public class MouseButtonPressedDto : InputEventDto {
        public readonly string Identifier;
        public readonly Vector2 Position;

        public MouseButtonPressedDto(string identifier, Vector2 position) {
            Identifier = identifier;
            Position = position;
        }
    }

    public class MouseButtonReleasedDto : InputEventDto {
        public readonly string Identifier;
        public readonly Vector2 Position;

        public MouseButtonReleasedDto(string identifier, Vector2 position) {
            Identifier = identifier;
            Position = position;
        }
    }
}