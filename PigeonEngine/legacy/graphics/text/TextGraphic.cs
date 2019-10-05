using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pigeon.gfx;
using pigeon.utilities;
using pigeon.utilities.extensions;

namespace pigeon.legacy.graphics.text {
    public class TextGraphic : Graphic {
        private SpriteFont _font;

        public SpriteFont Font {
            get { return _font; }
            set {
                var oldFont = _font;
                _font = value;

                if (oldFont != null) {
                    setSizing();
                }
            }
        }

        private readonly Justification just;

        private string text;

        public string Text {
            get { return text; }
            set {
                text = value;

                for (int i = 0; i < text.Length; i++) {
                    var character = text[i];
                    if (!Font.Characters.Contains(character) && character != '\r' && character != '\n') {
                        throw new ArgumentException(string.Format("Font {0} cannot draw character '{1}' ({2})", Font, character, character.Ascii()));
                    }
                }

                setSizing();
            }
        }

        private void setSizing() {
            Size = Font.MeasureString(text).ToPoint();
            justify();
        }

        private void justify() {
            switch (just) {
                case Justification.Left:
                    Offset = new Point(0, Size.Y / 2);
                    break;
                case Justification.Center:
                    Offset = Size.Div(2);
                    break;
                case Justification.Right:
                    Offset = new Point(Size.X, Size.Y / 2);
                    break;
                case Justification.TopLeft:
                    Offset = Point.Zero;
                    break;
                case Justification.TopCenter:
                    Offset = new Point(Size.X / 2, 0);
                    break;
                case Justification.TopRight:
                    Offset = new Point(Size.X, 0);
                    break;
                case Justification.BottomLeft:
                    Offset = new Point(0, Size.Y);
                    break;
                case Justification.BottomCenter:
                    Offset = new Point(Size.X / 2, Size.Y);
                    break;
                case Justification.BottomRight:
                    Offset = new Point(Size.X, Size.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public TextGraphic(string txt, SpriteFont font, Color color) {
            Font = font;
            just = Justification.Center;
            Color = color;
            Text = txt;
        }

        public TextGraphic(string txt, SpriteFont font, Color color, Justification justification) {
            Font = font;
            just = justification;
            Color = color;
            Text = txt;
        }

        public TextGraphic(string txt, SpriteFont font, Color color, Point manualOffset) {
            Font = font;
            just = Justification.TopLeft;
            Offset = manualOffset;
            Color = color;
            Text = txt;
        }

        public override void Update() { }

        public override void Draw(Vector2 position, float layer) {
            Renderer.SpriteBatch.DrawString(Font, text, position, Color, Rotation, Offset.ToVector2(), Scale, Flip, layer);
        }
    }
}
