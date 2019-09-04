using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pigeon.legacy.graphics.text;
using Pigeon.squab;

namespace Pigeon.Gfx {
	public class TextRenderer : Component, Drawable {
		public delegate string TimeBasedUpdater();
		public delegate string EventBasedUpdater(object sender, EventArgs evt);

		private TextGraphic textGraphic;

        public String Text {
            get { return textGraphic.Text; }
            set { textGraphic.Text = value; }
        }

        public Color Color {
            get { return textGraphic.Color; }
            set { textGraphic.Color = value; }
        }

		public Point Size {
			get { return textGraphic.Size; }
    	}

		public SpriteFont Font {
			get { return textGraphic.Font; }
			set { textGraphic.Font = value; }
		}

		public TextRenderer SetAll(string text, SpriteFont font, Color color, Justification justification = Justification.Center) {
			textGraphic = new TextGraphic(text, font, color, justification);
			return this;
		}

		protected override void Initialize() { }
		protected override void Update() { }

		public void Draw() {
			if (Enabled) {
				textGraphic.Draw(Object.WorldPosition.ToVector2(), Object.DrawLayer);
			}
		}
	}
}
