using pigeon.gameobject;

namespace BlueSun.src.worlds {
    internal class VoiceController : Component {
        private readonly GameObject[] enabledVoices = new GameObject[8];
        private readonly GameObject[] disabledVoices = new GameObject[8];

        protected override void Initialize() {
            for (int i = 0; i < 8; i++) {
                enabledVoices[i] = Object.FindChild(string.Format("{0}-on", i + 1));
                disabledVoices[i] = Object.FindChild(string.Format("{0}-off", i + 1));
            }
        }

        protected override void Update() {
            
        }
    }
}