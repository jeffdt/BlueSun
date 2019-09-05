using Microsoft.Xna.Framework.Graphics;
using pigeon.data;

namespace Bazaar.resources {
    public static class Fonts {
        public static SpriteFont Coin { get { return ResourceCache.Font("coin"); } }
        public static SpriteFont Console { get { return ResourceCache.Font("console"); } }
        public static SpriteFont Dialog { get { return ResourceCache.Font("dialog"); } }
        public static SpriteFont OneToneNumber { get { return ResourceCache.Font("bazaarScore"); } }
        public static SpriteFont OneToneText { get { return ResourceCache.Font("bazaarText"); } }
        public static SpriteFont ScoreLightBackground { get { return ResourceCache.Font("scoreLightBackground"); } }
        public static SpriteFont TwoTone { get { return ResourceCache.Font("bazaarTwoTone"); } }
        public static SpriteFont HighScoreName { get { return ResourceCache.Font("highScoreName"); } }
        public static SpriteFont Title { get { return ResourceCache.Font("title"); } }
    }
}