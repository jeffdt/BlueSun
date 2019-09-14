using Microsoft.Xna.Framework;
using pigeon.utilities.extensions;

namespace pigeon.pgnconsole {
    public class PGNConsoleOptions {
        public Rectangle PanelRect = new Rectangle(new Point(10, 0), new Point(Pigeon.Renderer.BaseResolutionX - 20, 3 * Pigeon.Renderer.BaseResolutionY / 4));
        public Color PanelColor = Color.Navy.WithAlpha(150);
        public Color BufferColor = Color.SkyBlue;
        public Color HistoryColor = Color.SkyBlue;
        public Color InfoColor = Color.MintCream;
        public Color ErrorColor = Color.Red;

        public int LogHistoryLimit = 64;
        public int CommandHistory = 16;

        public int TextInset = 5;
    }
}
