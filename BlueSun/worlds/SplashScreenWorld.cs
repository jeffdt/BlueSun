using Bazaar.resources;
using Microsoft.Xna.Framework;
using Pigeon;
using Pigeon.component;
using Pigeon.Core;
using Pigeon.Gfx;
using Pigeon.squab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSun.src.worlds {
    class SplashScreenWorld : World {
        protected override void Load() {
            RectRenderer rectRenderer = new RectRenderer(RectRenderer.DrawModes.Bordered) {
                BorderColor = Palette.White,
                BorderThickness = 5,
                FillColor = Palette.White,
                Rect = new Rectangle(0, 0, 20, 20)
            };

            var obj = new Squabject("TitleScreen", 0f);
            obj.AddComponent(rectRenderer);
            obj.AddComponent(new SimpleController());
            AddObj(obj);
        }

        protected override void Unload() { }
    }
}
