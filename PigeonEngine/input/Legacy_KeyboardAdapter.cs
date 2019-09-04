﻿using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Pigeon.utilities.helpers;

namespace Pigeon.Input {
	public class Legacy_KeyboardAdapter<T> {
		private readonly Dictionary<T, Keys> keyMap;

		public Legacy_KeyboardAdapter(Dictionary<T, Keys> initialMapping) {
			keyMap = new Dictionary<T, Keys>(initialMapping);
		}

		public Keys GetMappedKey(T input) {
			return keyMap[input];
		}

		public void RemapInputs(Dictionary<T, Keys> newInputMapping) {
			foreach (var key in newInputMapping.Keys) {
				keyMap[key] = newInputMapping[key];
			}
		}

		public bool IsPressed(SimpleDirections dir) {
			switch(dir) {
				case SimpleDirections.Up:
					return RawKeyboardInput.IsPressed(Keys.Up);
				case SimpleDirections.Down:
					return RawKeyboardInput.IsPressed(Keys.Down);
				case SimpleDirections.Left:
					return RawKeyboardInput.IsPressed(Keys.Left);
				default:
					return RawKeyboardInput.IsPressed(Keys.Right);
			}
		}

		public bool IsPressed(T input) {
			return RawKeyboardInput.IsPressed(keyMap[input]);
		}

		public bool IsHeld(SimpleDirections dir) {
			switch (dir) {
				case SimpleDirections.Up:
					return RawKeyboardInput.IsHeld(Keys.Up);
				case SimpleDirections.Down:
					return RawKeyboardInput.IsHeld(Keys.Down);
				case SimpleDirections.Left:
					return RawKeyboardInput.IsHeld(Keys.Left);
				default:
					return RawKeyboardInput.IsHeld(Keys.Right);
			}
		}

		public bool IsHeld(T input) {
			return RawKeyboardInput.IsHeld(keyMap[input]);
		}
	}
}