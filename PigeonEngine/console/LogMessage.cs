using System;

namespace Pigeon.Console {
	public class LogMessage {
		public String Text;
		public LogMessageTypes Type;

		public LogMessage(string text, LogMessageTypes type) {
			Text = text;
			Type = type;
		}
	}
}
