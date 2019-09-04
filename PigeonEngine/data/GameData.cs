using System;
using System.Collections.Generic;
using System.IO;
using PigeonEngine.data;

namespace Pigeon.Data {
	public static class GameData {
		private static string contentDirectory;

		public static void Initialize() {
            string dir = "current dir: " + Directory.GetCurrentDirectory();
            System.Console.WriteLine(dir);
			Directory.SetCurrentDirectory("Content");
			contentDirectory = Directory.GetCurrentDirectory();
		}

		public static bool FileExists(String filename) {
			return PigeonEngine.data.Data.FileExists(filename);
		}

		public static string[] Read(string filename) {
			return PigeonEngine.data.Data.ReadFile(filename);
		}

		public static T Deserialize<T>(string filename) where T : class {
			return PigeonEngine.data.Data.DeserializeObject<T>(contentDirectory, filename);
		}

		// example: GameData.GetFileList(@"data\rooms\mp\*.lvl")
		public static List<String> GetFileList(string searchPattern, bool includeExtension = true) {
			return PigeonEngine.data.Data.GetFileList(contentDirectory, searchPattern, includeExtension);
		}

		public static List<List<string>> ReadCsvFile(string filename) {
			return PigeonEngine.data.Data.ReadCsvFile(Path.Combine(contentDirectory, filename));
		}

		public static string[] GetContentFiles(string directory) {
			string path = directory;

			if (!Directory.Exists(path)) {
				return new string[0];
			}

			string[] filePaths = Directory.GetFiles(path, @"*.xnb", SearchOption.AllDirectories);
			for (int index = 0; index < filePaths.Length; index++) {
				filePaths[index] = filePaths[index].Replace(path + @"\", "");
				filePaths[index] = filePaths[index].Replace(@".xnb", "");
			}

			return filePaths;
		}
	}
}
