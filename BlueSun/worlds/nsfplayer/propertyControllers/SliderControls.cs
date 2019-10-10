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

            current = calcDefaultPercentage();
            bumpPercentage = .05f;
        }

        private float calcDefaultPercentage() {
            return (Default - Min) / (Max - Min);
        }

        private void updateSwitchPosition() {
            sliderSwitch.LocalPosition = new Point((int) MathHelper.Lerp(-50, 50, current), sliderSwitch.LocalPosition.Y);
        }

        protected override void Update() {
            if (RawKeyboardInput.IsPressed(LeftBumpKey)) {
                current -= RawKeyboardInput.IsHeld(Keys.LeftShift, Keys.RightShift) ? bumpPercentage * .5f : bumpPercentage;
                OnAdjustControl(MathHelper.Lerp(Min, Max, current));
            } else if (RawKeyboardInput.IsPressed(RightBumpKey)) {
                current += RawKeyboardInput.IsHeld(Keys.LeftShift, Keys.RightShift) ? bumpPercentage * .5f : bumpPercentage;
                OnAdjustControl(MathHelper.Lerp(Min, Max, current));
            }
        }

        internal void Reset() {
            if (sliderSwitch != null) {
                current = calcDefaultPercentage();
            }
        }
    }
}