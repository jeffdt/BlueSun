using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using pigeon.utilities.extensions;

namespace pigeon.data {
	public class Properties {
		public readonly String Filename;
		
		private readonly Dictionary<String, String> dictionary;
		
		public Properties(String file) {
			Filename = file;
			dictionary = new Dictionary<String, String>();

			if (File.Exists(Filename))
				loadFromFile(Filename);
			else
				File.Create(Filename);
		}

		private string GetRaw(String field) {
			string value;
			return (dictionary.TryGetValue(field, out value)) ? (value) : (null);
		}

		private string getReq(string field) {
			if (!dictionary.ContainsKey(field)) {
				throw new KeyNotFoundException(String.Format("required field '{0}' missing in property file {1}", field, Filename));
			}
			return dictionary[field];
		}

		public bool GetBool(String field) {
			return getReq(field).ToBool();
		}

		public bool TryBool(String field, bool defaultValue) {
			string value = GetRaw(field);
			return (value == null) ? defaultValue : value.ToBool();
		}

		public int GetInt(String field) {
			return getReq(field).ToInt();
		}

		public int TryInt(String field, int defaultValue) {
			string value = GetRaw(field);
			return (value == null) ? defaultValue : value.ToInt();
		}

		public float GetFloat(String field) {
			return getReq(field).ToFloat();
		}

		public float TryFloat(String field, float defaultValue) {
			string value = GetRaw(field);
			return (value == null) ? defaultValue : value.ToFloat();
		}

		public String GetString(String field) {
			return getReq(field);
		}

		public String TryString(String field, String defaultValue) {
			string value = GetRaw(field);
			return value ?? defaultValue;
		}

		public T GetEnum<T>(String field) {
			return getReq(field).ToEnum<T>();
		}

		public Vector2 GetVector2(String field) {
			return getReq(field).ToVector2();
		}

		public Vector2 TryVector2(String field, Vector2 defaultValue) {
			string value = GetRaw(field);
			return (value == null) ? defaultValue : value.ToVector2();
		}

		public List<String> GetStringList(string field) {
			return parseList("string", field).ToList();
		}

		public List<String> TryStringList(string field) {
			string value = GetRaw(field);
			return value == null ? new List<string>() : GetStringList(field);
		}

		public List<Vector2> GetVector2List(string field) {
			return parseList("vector", field).Select(str => str.ToVector2()).ToList();
		}

		private string[] parseList(string typeDescription, string field) {
			string[] strings = getReq(field).Split('|', ';');

			if (strings.Length == 0) {
				throw new FormatException(String.Format("{0} list could not be parsed for field '{1}'", typeDescription, field));
			}

			return strings;
		}

		private void loadFromFile(String file) {
			foreach (String line in File.ReadAllLines(file)) {
				if ((!String.IsNullOrEmpty(line)) &&
					(!line.StartsWith(";")) &&
					(!line.StartsWith("#")) &&
					(!line.StartsWith("'")) &&
					(line.Contains('='))) {
					int index = line.IndexOf('=');
					String key = line.Substring(0, index).Trim();
					String value = line.Substring(index + 1).Trim();

					if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
						(value.StartsWith("'") && value.EndsWith("'"))) {
						value = value.Substring(1, value.Length - 2);
					}

					try {
						// catch duplicates
						dictionary.Add(key, value);
					} catch(ArgumentException e) {
						throw new ArgumentException(String.Format("property {0} in property file {1} was defined more than once", e.ParamName, Filename), e);
					}
				}
			}
		}
	}
}
