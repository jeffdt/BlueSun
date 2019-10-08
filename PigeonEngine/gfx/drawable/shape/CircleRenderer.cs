using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pigeon.gfx;
using pigeon.gfx.drawable.image;
using pigeon.gfx.drawable.shape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigeonEngine.gfx.drawable.shape {
    public class CircleRenderer : ImageRenderer {
        public Color FillColor = Color.White;

        public int Radius;

        private Color[] pixels;

        protected override void Initialize() {
            InitialAnchor = new Point(Radius, Radius);

            initialTexture = new Texture2D(Renderer.GraphicsDeviceMgr.GraphicsDevice, Radius * 2, Radius * 2);
            pixels = new Color[initialTexture.Width * initialTexture.Height];

            int center = Radius;
            int centerSq = center * center;

            for (int row = 0; row < initialTexture.Height; row++) {
                for (int col = 0; col < initialTexture.Width; col++) {
                    int flatIndex = initialTexture.Height * row + col;

                    int xDistance = Radius - col;
                    int yDistance = Radius - row;
                    int distanceFromCenter = xDistance * xDistance + yDistance * yDistance;

                    pixels[flatIndex] = distanceFromCenter <= centerSq - 1? FillColor : Color.Transparent;
                }
            }

            initialTexture.SetData(pixels);

            base.Initialize();
        }
    }
}
