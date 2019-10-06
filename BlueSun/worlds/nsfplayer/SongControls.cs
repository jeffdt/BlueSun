using pigeon.gameobject;

namespace BlueSun.src.worlds {
    internal class SongControls : Component {
        protected override void Initialize() {
            
        }

        protected override void Update() {
            
        }

        public void ResetAll() {
            Object.FindChild("voices").GetComponent<VoicesController>().Reset();
        }
    }
}