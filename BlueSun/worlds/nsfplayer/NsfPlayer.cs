using BlueSun.resources;
using pigeon.core;
using pigeon.gameobject;
using pigeon.gfx;
using Microsoft.Xna.Framework;
using pigeon.legacy.graphics.text;
using BlueSun.src.parameters;
using pigeon.gfx.drawable.text;

namespace BlueSun.src.worlds {
    internal class NsfPlayer : World {
        protected override void Load() {
            BackgroundColor = Palette.DarkGray;

            AddObj(new GameObject("sfx").AddComponent(new SfxController()));
            AddObj(buildPlayer());
            AddObj(buildControls());
        }

        private static GameObject buildPlayer() {
            GameObject musicObj = new GameObject("music") { WorldPosition = Display.ScreenCenter };
            musicObj.AddComponent(new SongPlayer());

            GameObject songTextObj = new GameObject("songtext") { LocalLayer = .9f, FlatLocalPosition = new Point(0, -10) };
            songTextObj.AddComponent(new TextRenderer().SetAll("", Fonts.Console, Palette.White, Justification.Center));
            musicObj.AddChild(songTextObj);

            GameObject flavorTextObj = new GameObject("flavortext") { LocalLayer = .9f, FlatLocalPosition = new Point(2, 10) };
            flavorTextObj.AddComponent(new TextRenderer().SetAll("from the album:", Fonts.Console, Palette.LightGray, Justification.Center));
            musicObj.AddChild(flavorTextObj);

            GameObject folderTextObj = new GameObject("albumtext") { LocalLayer = .9f, FlatLocalPosition = new Point(0, 22) };
            folderTextObj.AddComponent(new TextRenderer().SetAll("", Fonts.Console, Palette.LightGray, Justification.Center));
            musicObj.AddChild(folderTextObj);
            return musicObj;
        }

        private static GameObject buildControls() {
            GameObject controlsObj = new GameObject("controls");
            controlsObj.AddComponent(new SongControls());



            return controlsObj;
        }

        protected override void Unload() { }
    }
}
