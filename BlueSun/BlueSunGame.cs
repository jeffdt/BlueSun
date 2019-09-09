using Bazaar.resources;
using BlueSun.src.parameters;
using BlueSun.src.worlds;
using Microsoft.Xna.Framework;
using pigeon.console;
using pigeon.core;
using pigeon.data;
using pigeon.gfx;
using pigeon.sound;
using System.Reflection;

namespace BlueSun {
    internal class BlueSunGame : pigeon.Pigeon {
        protected override string WindowTitle { get { return "Blue Sun"; } }
        protected override string Version { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }

        protected override bool StartMouseVisible { get { return true; } }
        protected override int FrameRate { get { return Display.FrameRate; } }
        protected override Color DefaultBkgdColor { get { return Palette.White; } }
        protected override Renderer GetRenderer { get { return new Renderer(Display.ScreenWidth, Display.ScreenHeight, Display.InitialScale); } }
        protected override TextureTemplateProcessor TemplateProcessor { get { return null; } }
        protected override World InitialWorld { get { return new NsfPlayer(); } }

        protected override ConsoleOptions ConsoleOpts {
            get {
                return new ConsoleOptions {
                    Font = Fonts.Console,
                    PanelColor = Color.DarkSlateGray,
                    BufferColor = Color.MintCream,
                    HistoryColor = Color.Gray,
                    InfoColor = Color.DodgerBlue,
                    ErrorColor = Color.Red,
                    LogHistoryLimit = 64,
                    BufferPosition = new Vector2(5, Display.ScreenHeight - 10),
                    BottomMessagePosition = new Vector2(5, Display.ScreenHeight - 20),
                    LineWrapWidth = Display.ScreenWidth - 15,
                    CommandDisplayLength = 25,
                };
            }
        }

        protected override void Load() {
            
        }

        protected override void InitializeGame() {

        }
    }
}
