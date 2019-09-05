using Bazaar.resources;
using Microsoft.Xna.Framework;
using pigeon.component;
using pigeon.core;
using pigeon.gfx;
using pigeon.squab;

namespace BlueSun.src.worlds {
    class SplashScreenWorld : World {
        protected override void Load() {
            RectRenderer rectRenderer = new RectRenderer(RectRenderer.DrawModes.FilledBordered) {
                BorderColor = Palette.TrueWhite,
                BorderThickness = 1,
                FillColor = Palette.Black,
                Rect = new Rectangle(0, 0, 20, 20)
            };

            var obj = new Squabject("TitleScreen", 0f);
            obj.AddComponent(rectRenderer);
            obj.AddComponent(new SimpleController());
            AddObj(obj);

            BackgroundColor = Palette.DarkGray;
        }

        protected override void Unload() { }
    }
}
