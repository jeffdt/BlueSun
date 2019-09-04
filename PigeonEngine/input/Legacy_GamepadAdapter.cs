using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Pigeon.utilities.helpers;

namespace Pigeon.Input {
	public class Legacy_GamepadAdapter<T> {
			private readonly Dictionary<T, Buttons> buttomMap;

			public Legacy_GamepadAdapter(Dictionary<T, Buttons> initialMapping) {
				buttomMap = new Dictionary<T, Buttons>(initialMapping);
			}

			public Buttons GetMappedKey(T input) {
				return buttomMap[input];
			}

			public void RemapInputs(Dictionary<T, Buttons> newInputMapping) {
				foreach (var key in newInputMapping.Keys) {
					buttomMap[key] = newInputMapping[key];
				}
			}

			public bool IsPressed(T input) {
				return Legacy_GamepadReader.IsPressed(buttomMap[input]);
			}

			public bool IsHeld(T input) {
				return Legacy_GamepadReader.IsHeld(buttomMap[input]);
			}

			public bool IsLJSPressed(SimpleDirections direction) {
				switch(direction) {
					case SimpleDirections.Up:
						return Legacy_GamepadReader.IsLeftJoystickPressedUp();
					case SimpleDirections.Down:
						return Legacy_GamepadReader.IsLeftJoystickPressedDown();
					case SimpleDirections.Left:
						return Legacy_GamepadReader.IsLeftJoystickPressedLeft();
					default:
						return Legacy_GamepadReader.IsLeftJoystickPressedRight();
				}
			}

			public bool IsLJSHeld(SimpleDirections direction) {
				switch (direction) {
					case SimpleDirections.Up:
						return Legacy_GamepadReader.IsLeftJoystickHeldUp();
					case SimpleDirections.Down:
						return Legacy_GamepadReader.IsLeftJoystickHeldDown();
					case SimpleDirections.Left:
						return Legacy_GamepadReader.IsLeftJoystickHeldLeft();
					default:
						return Legacy_GamepadReader.IsLeftJoystickHeldRight();
				}
			}

			public bool IsDpadPressed(SimpleDirections direction) {
				return Legacy_GamepadReader.IsDpadPressed(direction);
			}

			public bool IsDpadHeld(SimpleDirections direction) {
				return Legacy_GamepadReader.IsDpadHeld(direction);
			}
	}
}