using Microsoft.Xna.Framework;

namespace pigeon.pgnconsole {
    public class PGNConsoleOptions {
        public Color PanelColor = Color.DarkSlateGray;
        public Color BufferColor = Color.SkyBlue;
        public Color HistoryColor = Color.SteelBlue;
        public Color InfoColor = Color.MintCream;
        public Color ErrorColor = Color.Red;
        public int LogHistoryLimit = 64;
        public int CommandDisplayLength = 25;    // hack for characters that can fit in buffer since it can't be calced easily
        public int CommandHistory = 16;
    }
}
