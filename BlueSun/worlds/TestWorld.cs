using BlueSun.resources;
using Microsoft.Xna.Framework;
using pigeon.component;
using pigeon.core;
using pigeon.gameobject;
using pigeon.sound;
using pigeon.input;
using Microsoft.Xna.Framework.Input;
using pigeon.sound.music;
using pigeon.gfx.drawable.shape;

namespace BlueSun.src.worlds {
    class TestWorld : World {
        protected override void Load() {
            RectRenderer rectRenderer = new RectRenderer() {
                BorderThickness = 10,
                Rect = new Rectangle(0, 0, 30, 30)
            };

            var obj = new GameObject("TitleScreen", 0f);
            obj.AddComponent(rectRenderer);
            obj.AddComponent(new SimpleController());
            obj.AddComponent(new RectTester());
            AddObj(obj);

            BackgroundColor = Palette.DarkGray;

            Music.PlayTrack(0);
            Music.StereoDepth = .4f;
        }

        protected override void Unload() { }
    }
}

class RectTester : Component {
    RectRenderer rectRenderer;

    protected override void Initialize() {
        rectRenderer = GetComponent<RectRenderer>();
    }

    protected override void Update() {
        if (RawKeyboardInput.IsHeld(Keys.LeftShift, Keys.RightShift)) {
            rectRenderer.Image.Color = Color.DodgerBlue;
        } else {
            rectRenderer.Image.Color = Color.White;
        }
    }
}

