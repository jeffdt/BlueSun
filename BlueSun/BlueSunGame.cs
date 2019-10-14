using BlueSun.resources;
using BlueSun.src.parameters;
using Microsoft.Xna.Framework;
using pigeon.pgnconsole;
using pigeon.core;
using pigeon.data;
using System.Reflection;
using pigeon;
using pigeon.gfx;
using BlueSun.worlds.nsfplayer;
using BlueSun.worlds.collision;

namespace BlueSun {
    internal class BlueSunGame : Pigeon {
        public override DisplayParams DisplayParams { get { return new DisplayParams(Display.ScreenWidth, Display.ScreenHeight, Display.InitialScale, Display.FrameRate); } }

        protected override string WindowTitle { get { return "Blue Sun"; } }
        protected override string Version { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        protected override bool StartMouseVisible { get { return true; } }
        protected override int FrameRate { get { return Display.FrameRate; } }
        protected override Color DefaultBkgdColor { get { return Palette.White; } }
        protected override string SaveFolderName => "Blue Sun";
        protected override TextureTemplateProcessor TemplateProcessor { get { return null; } }
        protected override World InitialWorld { get { return new CollisionWorld(); } }

        protected override PGNConsoleOptions ConsoleOpts { get { return new PGNConsoleOptions(); } }

        protected override void LoadGame() {
        }

        protected override void InitializeGame() {
        }
    }
}
