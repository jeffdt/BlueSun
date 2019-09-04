﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Pigeon.Data;

namespace PigeonEngine.Input {
	public static class KeyBinds {
		private const string BINDINGS_FILEPATH = "keys.cfg";

		private static SerializableDictionary<Keys, string> keyBinds { get; set; }
		public static List<Keys> BoundKeys { get; private set; }

		public static void Load() {
			if (!PlayerData.FileExists(BINDINGS_FILEPATH)) {
				createBlankFile();
			} else {
				keyBinds = PlayerData.Deserialize<SerializableDictionary<Keys, string>>(BINDINGS_FILEPATH);
				updateBoundKeysList();
			}
		}

		public static Keys ParseToKey(string keyStr) {
			return (Keys) Enum.Parse(typeof(Keys), keyStr, true);
		}

		public static string GetKeyBind(Keys key) {
			string value;
			return keyBinds.TryGetValue(key, out value) ? value : null;
		}

		public static void BindKey(Keys key, string command) {
			keyBinds[key] = command;
			updateBoundKeysList();
			PlayerData.Serialize(keyBinds, BINDINGS_FILEPATH);
		}

		public static void UnbindKey(Keys key) {
			keyBinds.Remove(key);
			updateBoundKeysList();
			PlayerData.Serialize(keyBinds, BINDINGS_FILEPATH);
		}

		public static void Reset() {
			createBlankFile();
		}

		private static void createBlankFile() {
			keyBinds = new SerializableDictionary<Keys, string>();
			updateBoundKeysList();
			PlayerData.Serialize(keyBinds, BINDINGS_FILEPATH);
		}

		private static void updateBoundKeysList() {
			BoundKeys = new List<Keys>(keyBinds.Keys);
		}
	}
}
