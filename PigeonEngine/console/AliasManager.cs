using System.Collections.Generic;
using System.Linq;
using pigeon.data;

namespace PigeonEngine.console {
	public class AliasManager {
		private const string FILENAME = "alias.cfg";
		private SerializableDictionary<string, List<string>> aliases;

		public void Load() {
			if (PlayerData.FileExists(FILENAME)) {
				aliases = PlayerData.Deserialize<SerializableDictionary<string, List<string>>>(FILENAME);
			} else {
				createNewFile();
			}
		}

		public bool Exists(string aliasName) {
			return aliases.ContainsKey(aliasName);
		}

		public List<string> GetNames() {
			return aliases.Keys.ToList();
		} 

		public List<string> Get(string aliasName) {
			List<string> value;
			return aliases.TryGetValue(aliasName, out value) ? value : null;
		}

		public void Set(string aliasName, string aliasValue) {
			aliases[aliasName] = aliasValue.Split(new [] {';'}).ToList();
			save();
		}

		public void Reset() {
			createNewFile();
		}

		private void createNewFile() {
			aliases = new SerializableDictionary<string, List<string>>();
			save();
		}

		private void save() {
			PlayerData.Serialize(aliases, FILENAME);
		}
	}
}
