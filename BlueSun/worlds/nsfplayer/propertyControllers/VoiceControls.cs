using Microsoft.Xna.Framework.Input;
using pigeon.gameobject;
using pigeon.input;
using pigeon.sound.music;

namespace BlueSun.worlds.nsfplayer.propertyControllers {
    internal class ChannelControls : Component {
        private readonly GameObject[] enabledVoices = new GameObject[8];

        private readonly Keys[] numRowKeys = { Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8 };

        private readonly Keys[] numPadKeys = { Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8 };

        protected override void Initialize() {
            for (int v = 0; v < 8; v++) {
                enabledVoices[v] = Object.FindChild(string.Format("{0}-on", v + 1));
            }
        }

        protected override void Update() {
            for (int v = 0; v < 8; v++) {
                if (RawKeyboardInput.IsPressed(numRowKeys[v], numPadKeys[v])) {
                    bool isMuted = enabledVoices[v].DrawEnabled;

                    Music.SetVoiceMute(v, isMuted);

                    enabledVoices[v].DrawEnabled = !isMuted;
                }
            }
        }

        internal void Reset() {
            for (int v = 0; v < 8; v++) {
                if (enabledVoices[v] != null) {
                    enabledVoices[v].DrawEnabled = true;
                }
            }
        }
    }
}