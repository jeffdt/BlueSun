using BlueSun.resources;
using pigeon.core;
using pigeon.gameobject;
using pigeon.gfx;
using Microsoft.Xna.Framework;
using pigeon.legacy.graphics.text;
using BlueSun.src.parameters;

namespace BlueSun.src.worlds {
    class NsfPlayer : World {
        protected override void Load() {
            BackgroundColor = Palette.DarkGray;

            GameObject controllerObj = new GameObject("main") { WorldPosition = Display.ScreenCenter };
            controllerObj.AddComponent(new MusicController());

            GameObject songTextObj = new GameObject("song") { LocalLayer = .9f,  FlatLocalPosition = new Point(0, -10) };
            songTextObj.AddComponent(new TextRenderer().SetAll("", Fonts.Console, Palette.White, Justification.Center));
            controllerObj.AddChild(songTextObj);

            GameObject flavorTextObj = new GameObject("flavor") { LocalLayer = .9f, FlatLocalPosition = new Point(2, 10) };
            flavorTextObj.AddComponent(new TextRenderer().SetAll("from the album:", Fonts.Console, Palette.LightGray, Justification.Center));
            controllerObj.AddChild(flavorTextObj);

            GameObject folderTextObj = new GameObject("folder") { LocalLayer = .9f, FlatLocalPosition = new Point(0, 22) };
            folderTextObj.AddComponent(new TextRenderer().SetAll("", Fonts.Console, Palette.LightGray, Justification.Center));
            controllerObj.AddChild(folderTextObj);





            AddObj(controllerObj);
        }

        protected override void Unload() { }
    }
}
