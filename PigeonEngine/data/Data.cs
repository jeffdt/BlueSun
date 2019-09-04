using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Pigeon;
using Pigeon.utilities.extensions;

namespace PigeonEngine.data {
	public static class Data {
		private static readonly char [] commaSeparator = { ',' };

		public static string FormattedTimestamp() {
			var now = DateTime.Now;
			return string.Format(@"{0:000}.{1:00}.{2:00}_{3:00}.{4:00}.{5:00}.{6:000}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
		}

		public static OpenFileDialog CreateLoadDialog(string initialDir, string fileFilter) {
			var openDialog = new OpenFileDialog {
				InitialDirectory = initialDir,
				Filter = fileFilter, // @"room files (*.room)|*.room"
				RestoreDirectory = true
			};

			return openDialog;
		}

		#region file operations
		public static void WriteToFile(string fullPathFilename, string[] data) {
			File.WriteAllLines(fullPathFilename, data);
		}

		public static void AppendToFile(string filename, string message) {
			using (StreamWriter w = File.AppendText(filename)) {
				w.WriteLine(message);
			}
		}

		public static string[] ReadFile(string fullPathFilename) {
			return File.ReadAllLines(fullPathFilename);
		}

		public static List<List<string>> ReadCsvFile(string fullPathFilename) {
			var output = new List<List<string>>();

			var rows = File.ReadAllLines(fullPathFilename);

			for (int i = 0; i < rows.Length; i++) {
				var row = new List<string>();
				
				var cells = rows[i].Split(commaSeparator);
				for (int j = 0; j < cells.Length; j++) {
					row.Add(cells[j]);
				}

				output.Add(row);
			}

			return output;
		}

		public static void SerializeObject<T>(T data, string fullPathFilename) {
			var writer = new StreamWriter(fullPathFilename);
			var serializer = new XmlSerializer(typeof(T));
			serializer.Serialize(writer, data);
			writer.Close();
		}

		public static T DeserializeObject<T>(string directory, string filename) where T : class {
			return DeserializeObject<T>(Path.Combine(directory, filename));
		}

		public static T DeserializeObject<T>(string filePath) where T : class {
			if (!File.Exists(filePath)) {
				var dir = Directory.GetCurrentDirectory();
                Pigeon.Pigeon.Console.LogError("file not found: " + filePath);
				return null;
			}

			var serializer = new XmlSerializer(typeof(T));
			using (var reader = new StreamReader(filePath)) {
				return (T) serializer.Deserialize(reader);
			}
		}

		public static List<string> GetFileList(string searchLocation, string searchPattern, bool includeExtension = true) {
			DirectoryInfo dirInfo = new DirectoryInfo(searchLocation);
			var fileInfos = dirInfo.GetFiles(searchPattern);

			List<string> fileNames = new List<string>();

			if (includeExtension) {
				foreach (var fileInfo in fileInfos) {
					fileNames.Add(fileInfo.Name);
				}
			} else {
				foreach (var fileInfo in fileInfos) {
					fileNames.Add(Path.GetFileNameWithoutExtension(fileInfo.Name));
				}
			}

			return fileNames;
		}

		public static bool FileExists(String fullPathFilename) {
			return File.Exists(fullPathFilename);
		}
		#endregion

		#region directory operations
		public static bool DirectoryExists(String fullPathDir) {
			return Directory.Exists(fullPathDir);
		}

		public static void CreateDirectory(string fullPathDirectoryName) {
			if (!Directory.Exists(fullPathDirectoryName)) {
				Directory.CreateDirectory(fullPathDirectoryName);
			}
		}
		#endregion

		#region screenshots

		//		public static void SavePng(Texture2D texture, String fileName, String relativeDirectory = null) {
		//			string directoryPath;
		//
		//			if (relativeDirectory == null) {
		//				directoryPath = UserDataPath;
		//			} else {
		//				directoryPath = Path.Combine(UserDataPath, relativeDirectory);
		//				if (!Directory.Exists(directoryPath)) {
		//					Directory.CreateDirectory(directoryPath);
		//				}
		//			}
		//
		//			string fullPath = Path.Combine(directoryPath, fileName);
		//
		//			using (Stream stream = File.Create(fullPath)) {
		//				texture.SaveAsPng(stream, texture.Width, texture.Height);
		//			}
		//		}

		public static void SaveAsPng_Pigeon(this Texture2D texture, string fullPathFilename) {
			saveAsPng(texture.ToBitmap(), fullPathFilename);
		}

		// helper method to save bitmaps as pngs
		private static void saveAsPng(this Bitmap bitmap, string fullPathFilename) {
			using (Stream stream = File.Create(fullPathFilename)) {
				bitmap.Save(stream, ImageFormat.Png);
			}

            Pigeon.Pigeon.Console.Log(string.Format("Saving {0}...", fullPathFilename));
		}
		#endregion
	}
}
