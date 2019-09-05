namespace pigeon.console {
    public class LogMessage {
        public string Text;
        public LogMessageTypes Type;

        public LogMessage(string text, LogMessageTypes type) {
            Text = text;
            Type = type;
        }
    }
}
