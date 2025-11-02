using System;
using Godot;

namespace InputSystem {
	public partial class InputController : Node {
		public event Action<InputEventDto> InputFromPlayer;

		public override void _Input(InputEvent @event) {
			switch (@event) {
				case InputEventKey eventKey:
					if (eventKey.Echo) {
						return;
					}

					InputFromPlayer?.Invoke(new KeyEventDto(
						OS.GetKeycodeString(eventKey.PhysicalKeycode),
						eventKey.Pressed
					));
					break;
				case InputEventMouseButton eventMouseButton:
					InputFromPlayer?.Invoke(new MouseEventDto(
						eventMouseButton.ButtonIndex.ToString(),
						eventMouseButton.Pressed,
						GetViewport().GetCamera2D().GetGlobalMousePosition()
					));
					break;
				case InputEventMouseMotion:
					break;
				case InputEventJoypadButton:
					break;
				case InputEventJoypadMotion:
					break;
			}
		}
	}
}
