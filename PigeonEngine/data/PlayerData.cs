using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using PigeonEngine.data;

namespace Pigeon.Data {
	public static class PlayerData {
		public static string UserDataPath { get; private set; }

		public static void Initialize() {
			UserDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Bazaar");
			DirectoryInfo directory = new DirectoryInfo(UserDataPath);
			if (!directory.Exists) {
				directory.Create();
			}
		}

		private static string buildPath(string filename) {
			return Path.Combine(UserDataPath, filename);
		}

		#region file operations
		public static string[] Read(string filename) {
			return PigeonEngine.data.Data.ReadFile(buildPath(filename));
		}

		public static void AppendToFile(string filename, string message) {
            PigeonEngine.data.Data.AppendToFile(buildPath(filename), message);
		}

		public static void WriteToFile(string filename, string[] data) {
            PigeonEngine.data.Data.WriteToFile(buildPath(filename), data);
		}

		public static void Serialize<T>(T data, string filename) {
            PigeonEngine.data.Data.SerializeObject(data, buildPath(filename));
		}

		public static T Deserialize<T>(String filename) where T : class {
			return PigeonEngine.data.Data.DeserializeObject<T>(UserDataPath, filename);
		}

		public static bool FileExists(String filename) {
			return PigeonEngine.data.Data.FileExists(buildPath(filename));
		}

		public static List<String> GetFileList(string searchPattern, bool includeExtension = true) {
			return PigeonEngine.data.Data.GetFileList(UserDataPath, searchPattern, includeExtension);
		}

		public static void SaveAsTimestampedPng(this Texture2D texture) {
			var screenshotFolder = Path.Combine(UserDataPath, "screenshots");
			if (!DirectoryExists(screenshotFolder)) {
				Directory.CreateDirectory(screenshotFolder);
			}

			texture.SaveAsPng_Pigeon(Path.Combine(screenshotFolder, PigeonEngine.data.Data.FormattedTimestamp() + ".png"));
		}
		#endregion

		#region directory operations
		public static bool DirectoryExists(String dir) {
			return PigeonEngine.data.Data.DirectoryExists(buildPath(dir));
		}
		
		public static void CreateDirectory(string directoryName) {
            PigeonEngine.data.Data.CreateDirectory(buildPath(directoryName));
		}
		#endregion
	}
}
