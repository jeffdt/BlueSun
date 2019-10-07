using pigeon.gameobject;

namespace BlueSun.worlds.nsfplayer.propertyControllers {
    internal class SongControls : Component {
        protected override void Initialize() {

        }

        protected override void Update() {

        }

        public void ResetAll() {
            Object.FindChild("voices").GetComponent<VoiceControls>().Reset();
        }
    }
}