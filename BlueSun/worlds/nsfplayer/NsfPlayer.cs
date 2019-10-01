using BlueSun.resources;
using pigeon.core;
using pigeon.gameobject;
using pigeon.gfx;
using Microsoft.Xna.Framework;
using pigeon.legacy.graphics.text;
using BlueSun.src.parameters;

namespace BlueSun.src.worlds {
    internal class NsfPlayer : World {
        protected override void Load() {
            BackgroundColor = Palette.DarkGray;

            AddObj(new GameObject("sfx").AddComponent(new SfxController()));
            AddObj(buildMusicController());
        }

        private static GameObject buildMusicController() {
            GameObject musicObj = new GameObject("music") { WorldPosition = Display.ScreenCenter };
            musicObj.AddComponent(new SongPlaybackController());

            GameObject songTextObj = new GameObject("song") { LocalLayer = .9f, FlatLocalPosition = new Point(0, -10) };
            songTextObj.AddComponent(new TextRenderer().SetAll("", Fonts.Console, Palette.White, Justification.Center));
            musicObj.AddChild(songTextObj);

            GameObject flavorTextObj = new GameObject("flavor") { LocalLayer = .9f, FlatLocalPosition = new Point(2, 10) };
            flavorTextObj.AddComponent(new TextRenderer().SetAll("from the album:", Fonts.Console, Palette.LightGray, Justification.Center));
            musicObj.AddChild(flavorTextObj);

            GameObject folderTextObj = new GameObject("album") { LocalLayer = .9f, FlatLocalPosition = new Point(0, 22) };
            folderTextObj.AddComponent(new TextRenderer().SetAll("", Fonts.Console, Palette.LightGray, Justification.Center));
            musicObj.AddChild(folderTextObj);
            return musicObj;
        }

        protected override void Unload() { }
    }
}
