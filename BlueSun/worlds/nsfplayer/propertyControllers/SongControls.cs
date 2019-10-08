using pigeon.gameobject;

namespace BlueSun.worlds.nsfplayer.propertyControllers {
    internal class SongControls : Component {
        protected override void Initialize() {

        }

        protected override void Update() {

        }

        public void ResetAll() {
            Object.FindChild("channels").GetComponent<ChannelControls>().Reset();
            Object.FindChild("tempo").GetComponent<SliderControls>().Reset();
            Object.FindChild("stereo").GetComponent<SliderControls>().Reset();
            Object.FindChild("treble").GetComponent<SliderControls>().Reset();
            Object.FindChild("bass").GetComponent<SliderControls>().Reset();
        }
    }
}