﻿using Microsoft.Xna.Framework.Graphics;

namespace PigeonEngine.utilities.extensions {
    public static class SpriteFontExtensions {
        public static int MeasureWidth(this SpriteFont font, string text) {
            return (int) font.MeasureString(text).X;
        }
    }
}
