using System;
using System.Windows.Forms;
using pigeon.console;
using pigeon.utilities.extensions;
using pigeon.Properties;

namespace pigeon.winforms {
    public partial class PigeonUi : Form {
        public PigeonUi() {
            InitializeComponent();
        }

        private void PigeonUi_Load(object sender, EventArgs e) {
            populateObjectTree();

            Pigeon.EngineEventRegistry.RegisterEventHandler<ConsoleLogChangedEvent>(this.onConsoleLogChanged);
            refreshConsoleLog();

            updatePauseButton();
        }

        private void updatePauseButton() {
            utilitiesPauseButton.BackgroundImage = Pigeon.Instance.PauseWorld ? Resources.play : Resources.pause;
        }

        private void pauseButton_Click(object sender, EventArgs e) {
            Pigeon.Console.ExecuteCommand("pauseworld");
            updatePauseButton();
        }

        private void tickButton_Click(object sender, EventArgs e) {
            Pigeon.Console.ExecuteCommand("tick");
            updatePauseButton();
        }

        private void PigeonUi_FormClosed(object sender, FormClosedEventArgs e) {
            Pigeon.EngineEventRegistry.UnregisterEventHandler<ConsoleLogChangedEvent>(this.onConsoleLogChanged);
        }

        #region menu
        private void animatorToolStripMenuItem_Click(object sender, EventArgs e) {
            new AnimationTool().Show();
        }

        private void takeScreenshotToolStripMenuItem_Click(object sender, EventArgs e) {
            Pigeon.Console.ExecuteCommand("screenshot");
        }

        private void openSaveDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
            Pigeon.Console.ExecuteCommand("savedir");
        }

        private void sandboxWorldToolStripMenuItem_Click(object sender, EventArgs e) {
            Pigeon.Console.ExecuteCommand("sandbox");
        }
        #endregion

        private void utilitiesResolutionTextFields_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        private void utilitiesResolutionSetButton_Click(object sender, EventArgs e) {
            var x = utilitiesResolutionXTextfield.Text.ToInt();
            var y = utilitiesResolutionYTextfield.Text.ToInt();

            if (x > 0 && y > 0) {
                Pigeon.Renderer.SetCustomResolution(x, y);
            }
        }
    }
}
