using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigeonEngine.utilities.extensions {
    public static class SpriteBatchExtensions {
        public static void BeginPixelPerfect(this SpriteBatch spriteBatch, Matrix transformMatrix) {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, transformMatrix);
        }
    }
}
