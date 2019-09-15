using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using pigeon.legacy.entities;
using pigeon.legacy.graphics;

namespace pigeon.tools {
    internal class Cursor : Entity {
        public Image Regular, Clicked;

        public Cursor(Image regular, Image clicked)
            : base(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), regular) {
            Layer = int.MaxValue;
            Regular = regular;
            Clicked = clicked;
            Regular.Parallax = new Vector2();
            Clicked.Parallax = new Vector2();
        }

        public override void Update() {
            Position.X = Mouse.GetState().X;
            Position.Y = Mouse.GetState().Y;

            Graphic = Mouse.GetState().LeftButton == ButtonState.Pressed ? Clicked : Regular;

            base.Update();
        }
    }
}
