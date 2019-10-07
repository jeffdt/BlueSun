using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using pigeon.gameobject;
using pigeon.input;
using PigeonEngine.utilities.extensions;

namespace BlueSun.worlds.nsfplayer.propertyControllers {
    internal class SliderControls : Component {
        public delegate void AdjustControl(float newValue);
        
        public AdjustControl OnAdjustControl;
        public float Min { get; set; }
        public float Max { get; set; }
        public float Default { get; set; }
        public float BumpAmount { get; set; }
        public Keys LeftBumpKey { get; set; }
        public Keys RightBumpKey { get; set; }

        private float _current;
        private float current {
            get { return _current; }
            set {
                _current = value.Clamp(0, 1);
                updateSwitchPosition();
            }
        }
        private float bumpPercentage;

        private GameObject sliderSwitch;

        protected override void Initialize() {
            sliderSwitch = Object.FindChild("slider-switch");

            current = (Default - Min) / (Max - Min);
            // bumpPercentage =  BumpAmount / (Max - Min);
            bumpPercentage = .05f;
        }

        private void updateSwitchPosition() {
            sliderSwitch.LocalPosition = new Point((int) MathHelper.Lerp(-50, 50, current), sliderSwitch.LocalPosition.Y);
        }

        protected override void Update() {
            if (RawKeyboardInput.IsPressed(LeftBumpKey)) {
                current -= bumpPercentage;
                OnAdjustControl(MathHelper.Lerp(Min, Max, current));
            } else if (RawKeyboardInput.IsPressed(RightBumpKey)) {
                current += bumpPercentage;
                OnAdjustControl(MathHelper.Lerp(Min, Max, current));
            }
        }
    }
}