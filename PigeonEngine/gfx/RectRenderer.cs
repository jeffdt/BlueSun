using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pigeon.squab;
using Pigeon.utilities.extensions;

namespace Pigeon.Gfx {
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
            Color[] pixels = null;

            switch (DrawMode) {
                case DrawModes.Filled:
                    pixels = buildFillPixels();
                    break;
                case DrawModes.Bordered:
                    pixels = buildBorderPixels();
                    break;
                case DrawModes.FilledBordered:
                    throw new NotImplementedException();
            }

            initialTexture.SetData(pixels);

            base.Initialize();
        }

        private Color[] buildFillPixels() {
            var pixels = new Color[initialTexture.Width * initialTexture.Height];

            for (int i = 0; i < pixels.Length; i++) {
                pixels[i] = FillColor;
            }

            return pixels;
        }

        private Color[] buildBorderPixels() {
            int width = initialTexture.Width;
            int height = initialTexture.Height;

            var pixels = new Color[width * height];

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

            return pixels;
        }
    }
}
