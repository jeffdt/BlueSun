using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pigeon.gfx.drawable.image;

namespace pigeon.gfx.drawable.shape {
    public class RectRenderer : ImageRenderer {
        public enum DrawModes { Filled, Bordered, FilledBordered }

        public Rectangle Rect; // x/y represent offsets from Object.Position. if Object.Position is upper left, x/y will be 0,0.
        public Color FillColor = Color.White;
        public Color BorderColor = Color.Black;
        public int BorderThickness = 1;
        public DrawModes DrawMode = DrawModes.FilledBordered;

        private Color[] pixels;

        protected override void Initialize() {
            initialTexture = new Texture2D(Renderer.GraphicsDeviceMgr.GraphicsDevice, Rect.Width, Rect.Height);
            pixels = new Color[initialTexture.Width * initialTexture.Height];

            switch (DrawMode) {
                case DrawModes.Filled:
                    setFillPixels(FillColor);
                    break;
                case DrawModes.Bordered:
                    setBorderPixels(BorderColor);
                    break;
                case DrawModes.FilledBordered:
                    setFillPixels(FillColor);
                    setBorderPixels(BorderColor);
                    break;
            }

            initialTexture.SetData(pixels);

            base.Initialize();
        }

        private void setFillPixels(Color fillColor) {
            for (int i = 0; i < pixels.Length; i++) {
                pixels[i] = fillColor;
            }
        }

        private void setBorderPixels(Color borderColor) {
            int width = Rect.Width;
            int height = Rect.Height;

            // top
            for (int row = 0; row < BorderThickness; row++) {
                for (int col = 0; col < width; col++) {
                    pixels[row * width + col] = borderColor;
                }
            }

            // bottom
            for (int row = height - BorderThickness; row < height; row++) {
                for (int col = 0; col < width; col++) {
                    pixels[row * width + col] = borderColor;
                }
            }

            // left
            for (int row = 0; row < height; row++) {
                for (int col = 0; col < BorderThickness; col++) {
                    pixels[row * width + col] = borderColor;
                }
            }

            // right
            for (int row = 0; row < height; row++) {
                for (int col = width - BorderThickness; col < width; col++) {
                    pixels[row * width + col] = borderColor;
                }
            }
        }
    }
}
