using System.IO;
using System.Xml.Serialization;

namespace PigeonEngine.utilities {
	public static class XmlOps {
		public static string ToXmlString<T>(this T input) {
			using (var writer = new StringWriter()) {
				input.ToXml(writer);
				return writer.ToString();
			}
		}

		public static void ToXml<T>(this T objectToSerialize, StringWriter writer) {
			var xmlSerializer = new XmlSerializer(typeof(T));
			xmlSerializer.Serialize(writer, objectToSerialize);
		}
	}
}
