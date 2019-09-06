using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using pigeon.input;
using pigeon.gameobject;
using pigeon.utilities;

namespace pigeon.component {
    public class SimpleController : Component {
        public int Speed = 100;

        private int lastX;
        private int lastY;

        protected override void Initialize() { }

        protected override void Update() {
            if (RawKeyboardInput.IsHeld(Keys.LeftShift, Keys.RightShift)) {
                Object.Stop();
                updateBumpControl();
            } else {
                updateSmoothControl();
            }
        }

        private void updateSmoothControl() {
            int newX = lastX;
            if (RawKeyboardInput.IsHeld(Keys.Left)) {
                newX = -1;
            } else if (RawKeyboardInput.IsHeld(Keys.Right)) {
                newX = 1;
            } else {
                newX = 0;
            }

            int newY = lastY;
            if (RawKeyboardInput.IsHeld(Keys.Up)) {
                newY = -1;
            } else if (RawKeyboardInput.IsHeld(Keys.Down)) {
                newY = 1;
            } else {
                newY = 0;
            }

            if (newX != lastX || newY != lastY) {
                Object.Velocity = new Vector2(newX * Speed, newY * Speed);
            }

            lastX = newX;
            lastY = newY;
        }

        private void updateBumpControl() {
            if (RawKeyboardInput.IsPressed(Keys.Left)) {
                Object.FlatLocalPosition = Object.FlatLocalPosition.AddToX(-1);
            } else if (RawKeyboardInput.IsPressed(Keys.Right)) {
                Object.FlatLocalPosition = Object.FlatLocalPosition.AddToX(1);
            }

            if (RawKeyboardInput.IsPressed(Keys.Up)) {
                Object.FlatLocalPosition = Object.FlatLocalPosition.AddToY(-1);
            } else if (RawKeyboardInput.IsPressed(Keys.Down)) {
                Object.FlatLocalPosition = Object.FlatLocalPosition.AddToY(1);
            }
        }
    }
}
