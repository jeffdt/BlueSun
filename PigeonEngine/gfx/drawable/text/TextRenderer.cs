using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pigeon.legacy.graphics.text;
using pigeon.gameobject;
using PigeonEngine.gfx.drawable;

namespace pigeon.gfx.drawable.text {
    public class TextRenderer : Component, IRenderable {
        public delegate string TimeBasedUpdater();
        public delegate string EventBasedUpdater(object sender, EventArgs evt);

        private TextGraphic textGraphic;

        private string _text;
        public string Text {
            get { return _text; }
            set {
                _text = value;
                if (textGraphic != null) {
                    textGraphic.Text = _text;
                }
            }
        }

        private Color _color;
        public Color Color {
            get { return _color; }
            set {
                _color = value;
                if (textGraphic != null) {
                    textGraphic.Color = _color;
                }
            }
        }

        public Point Size {
            get { return textGraphic.Size; }
        }

        private SpriteFont _font;
        public SpriteFont Font {
            get { return _font; }
            set {
                _font = value;

                if (textGraphic != null) {
                    textGraphic.Font = _font;
                }
            }
        }

        public Justifications Justification;

        public TextRenderer SetAll(string text, SpriteFont font, Color color, Justifications justification = Justifications.Center) {
            textGraphic = new TextGraphic(text, font, color, justification);
            return this;
        }

        protected override void Initialize() {
            textGraphic = new TextGraphic(_text, _font, _color, Justification);
        }

        protected override void Update() { }

        public void Draw() {
            if (Enabled) {
                textGraphic.Draw(Object.WorldPosition.ToVector2(), Object.DrawLayer);
            }
        }
    }
}
