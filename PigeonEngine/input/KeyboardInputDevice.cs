using Microsoft.Xna.Framework.Input;
using Pigeon.Input;

namespace PigeonEngine.Input {
	public class KeyboardInputDevice : InputDevice {
		private static readonly Keys [] DIR_WSAD = { Keys.W, Keys.D, Keys.S, Keys.A };
		private static readonly Keys[] DIR_ARROW = { Keys.Up, Keys.Right, Keys.Down, Keys.Left };

		private readonly Keys[] keyMap;
		
		public KeyboardInputDevice(Keys [] keyMap) {
			this.keyMap = keyMap;
		}

		public override void Update() { }

		public override bool IsButtonPressed(int buttonNumber) {
			return RawKeyboardInput.IsPressed(keyMap[buttonNumber]);
		}

		public override bool IsButtonHeld(int buttonNumber) {
			return RawKeyboardInput.IsHeld(keyMap[buttonNumber]);
		}

		public override bool IsAnyPressed() {
			return RawKeyboardInput.IsAnyPressed();
		}

		public override bool IsAnyHeld() {
			return RawKeyboardInput.IsAnyKeyHeld();
		}
	}
}
