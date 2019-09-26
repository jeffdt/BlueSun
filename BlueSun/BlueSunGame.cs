﻿using BlueSun.resources;
using BlueSun.src.parameters;
using BlueSun.src.worlds;
using Microsoft.Xna.Framework;
using pigeon.pgnconsole;
using pigeon.core;
using pigeon.data;
using pigeon.gfx;
using PigeonEngine.gfx;
using System.Reflection;

namespace BlueSun {
    internal class BlueSunGame : pigeon.Pigeon {
        public override DisplayParams DisplayParams { get { return new DisplayParams(Display.ScreenWidth, Display.ScreenHeight, Display.InitialScale, Display.FrameRate); } }

        protected override string WindowTitle { get { return "Blue Sun"; } }
        protected override string Version { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        protected override bool StartMouseVisible { get { return true; } }
        protected override int FrameRate { get { return Display.FrameRate; } }
        protected override Color DefaultBkgdColor { get { return Palette.White; } }
        protected override TextureTemplateProcessor TemplateProcessor { get { return null; } }
        protected override World InitialWorld { get { return new NsfPlayer(); } }

        protected override PGNConsoleOptions ConsoleOpts {
            get {
                return new PGNConsoleOptions();
            }
        }

        protected override void LoadGame() {
        }

        protected override void InitializeGame() {
        }
    }
}
