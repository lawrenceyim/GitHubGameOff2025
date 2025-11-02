using System;
using Godot;

namespace InputSystem {
	public abstract class InputEventDto {
		private readonly string _identifier;
		private readonly bool _pressed;

		protected InputEventDto(string identifier, bool pressed) {
			_identifier = identifier;
			_pressed = pressed;
		}

		public string Identifier() {
			return _identifier;
		}

		public bool Pressed() {
			return _pressed;
		}
	}

	public class KeyEventDto : InputEventDto {
		public KeyEventDto(string identifier, bool pressed) : base(identifier, pressed) { }
	}

	public class MouseEventDto : InputEventDto {
		private readonly Vector2 _position;

		public MouseEventDto(string identifier, bool pressed, Vector2 position) : base(identifier, pressed) {
			_position = position;
		}

		public Vector2 Position() {
			return _position;
		}
	}
}
