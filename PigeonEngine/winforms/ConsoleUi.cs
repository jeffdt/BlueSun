using System;
using System.Drawing;
using System.Windows.Forms;
using pigeon.pgnconsole;

namespace pigeon.winforms {
    public partial class PigeonUi {
        private void consoleCommandText_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char) Keys.Enter) {
                e.Handled = true;
                sendConsoleCommand();
            }
        }

        private void consoleCommandCommit_Click(object sender, EventArgs e) {
            sendConsoleCommand();
        }

        private void sendConsoleCommand() {
            Pigeon.Console.ExecuteCommand(consoleCommandText.Text, true);
            consoleCommandText.Text = string.Empty;

            refreshConsoleLog();
        }

        private void refreshConsoleLog() {
            consoleLogText.Clear();

            var messages = Pigeon.Console.MessageLog.GetAllMessages();
            for (int index = 0; index < messages.Count; index++) {
                var message = messages[index];

                addMessageToLog(message);
            }

            consoleLogText.ScrollToCaret();
        }

        private void onConsoleLogChanged(object sender, EventArgs e) {
            var newMessage = ((ConsoleLogChangedEvent) e).Message;
            addMessageToLog(newMessage);
        }

        private void addMessageToLog(LogMessage message) {
            if (message.Type == LogMessageTypes.Error) {
                consoleLogText.SelectionColor = Color.Red;
                consoleLogText.SelectionFont = new Font(consoleLogText.Font, FontStyle.Regular);
            } else if (message.Type == LogMessageTypes.Command) {
                consoleLogText.SelectionColor = Color.LimeGreen;
                consoleLogText.SelectionFont = new Font(consoleLogText.Font, FontStyle.Bold);
            } else if (message.Type == LogMessageTypes.Info) {
                consoleLogText.SelectionColor = Color.DodgerBlue;
                consoleLogText.SelectionFont = new Font(consoleLogText.Font, FontStyle.Regular);
            }

            consoleLogText.AppendText(message.Text);
            consoleLogText.AppendText(Environment.NewLine);
        }
    }
}
