using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pigeon.gfx {
    public class RectRenderer : ImageRenderer {
        public enum DrawModes { Filled, Bordered, FilledBordered }

        // x/y represent offsets from Object.Position
        public Rectangle Rect;

        public Color FillColor;
        public Color BorderColor;
        public int BorderThickness = 1;

        public DrawModes DrawMode = DrawModes.Filled;

        public RectRenderer(DrawModes drawMode) {
            DrawMode = drawMode;
        }

        protected override void Initialize() {
            initialTexture = new Texture2D(Renderer.GraphicsDeviceMgr.GraphicsDevice, Rect.Width, Rect.Height);
            Color[] pixels = new Color[initialTexture.Width * initialTexture.Height];

            switch (DrawMode) {
                case DrawModes.Filled:
                    setFillPixels(pixels);
                    break;
                case DrawModes.Bordered:
                    setBorderPixels(pixels, Rect.Width, Rect.Height);
                    break;
                case DrawModes.FilledBordered:
                    setFillPixels(pixels);
                    setBorderPixels(pixels, Rect.Width, Rect.Height);
                    break;
            }

            initialTexture.SetData(pixels);

            base.Initialize();
        }

        private void setFillPixels(Color[] pixels) {
            for (int i = 0; i < pixels.Length; i++) {
                pixels[i] = FillColor;
            }
        }

        private void setBorderPixels(Color[] pixels, int width, int height) {
            // top
            for (int row = 0; row < BorderThickness; row++) {
                for (int col = 0; col < width; col++) {
                    pixels[row * width + col] = BorderColor;
                }
            }

            // bottom
            for (int row = height - BorderThickness; row < height; row++) {
                for (int col = 0; col < width; col++) {
                    pixels[row * width + col] = BorderColor;
                }
            }

            // left
            for (int row = 0; row < height; row++) {
                for (int col = 0; col < BorderThickness; col++) {
                    pixels[row * width + col] = BorderColor;
                }
            }

            // right
            for (int row = 0; row < height; row++) {
                for (int col = width - BorderThickness; col < width; col++) {
                    pixels[row * width + col] = BorderColor;
                }
            }
        }
    }
}
