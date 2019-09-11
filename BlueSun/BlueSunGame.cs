using BlueSun.resources;
using BlueSun.src.parameters;
using BlueSun.src.worlds;
using Microsoft.Xna.Framework;
using pigeon.console;
using pigeon.core;
using pigeon.data;
using pigeon.gfx;
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
