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

        public int Rad;
        public float RadAdjust = 0f; // making circles with pixels can be ugly. tweak this to manually bump the final product till it looks nice.

        private Color[] pixels;

        protected override void Initialize() {
            InitialAnchor = new Point(Rad, Rad);

            initialTexture = new Texture2D(Renderer.GraphicsDeviceMgr.GraphicsDevice, Rad * 2, Rad * 2);
            pixels = new Color[initialTexture.Width * initialTexture.Height];

            int center = Rad;
            int centerSq = Rad * Rad;

            for (int row = 0; row < initialTexture.Height; row++) {
                for (int col = 0; col < initialTexture.Width; col++) {
                    int flatIndex = initialTexture.Height * row + col;

                    int xDistance = Rad - col;
                    int yDistance = Rad - row;
                    int distanceFromCenter = xDistance * xDistance + yDistance * yDistance;

                    pixels[flatIndex] = distanceFromCenter <= centerSq - 1 + RadAdjust ? FillColor : Color.Transparent;
                }
            }

            initialTexture.SetData(pixels);

            base.Initialize();
        }
    }
}
