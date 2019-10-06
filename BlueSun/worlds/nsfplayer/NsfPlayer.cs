using BlueSun.resources;
using BlueSun.src.parameters;
using Microsoft.Xna.Framework;
using pigeon.core;
using pigeon.gameobject;
using pigeon.gfx.drawable.shape;
using pigeon.gfx.drawable.text;

namespace BlueSun.src.worlds {
    internal class NsfPlayer : World {
        private const int voiceButtonSize = 9;

        protected override void Load() {
            BackgroundColor = Color.DimGray;

            // AddObj(new GameObject("sfx").AddComponent(new SfxController()));
            AddObj(buildPlayer());
            AddObj(buildControls());
        }

        private static GameObject buildPlayer() {
            GameObject musicObj = new GameObject("music") { WorldPosition = new Point(Display.ScreenCenterX, 50) };
            musicObj.AddComponent(new SongPlayer());

            GameObject songTextObj = new GameObject("songtext") { Layer = .9f, FlatLocalPosition = new Point(0, -10) };
            songTextObj.AddComponent(new TextRenderer() { Text = "", Font = Fonts.Console, Color = Palette.White, Justification = Justifications.Center });
            musicObj.AddChild(songTextObj);

            GameObject flavorTextObj = new GameObject("flavortext") { Layer = .9f, FlatLocalPosition = new Point(2, 0) };
            flavorTextObj.AddComponent(new TextRenderer() { Text = "from the album:", Font = Fonts.Console, Color = Palette.LightGray, Justification = Justifications.Center });
            musicObj.AddChild(flavorTextObj);

            GameObject folderTextObj = new GameObject("albumtext") { Layer = .9f, FlatLocalPosition = new Point(0, 10) };
            folderTextObj.AddComponent(new TextRenderer() { Text = "", Font = Fonts.Console, Color = Palette.LightGray, Justification = Justifications.Center });
            musicObj.AddChild(folderTextObj);
            return musicObj;
        }

        private static GameObject buildControls() {
            GameObject controlsObj = new GameObject("controls").AddComponent(new SongControls());

            controlsObj.AddChild(buildStereoControls());
            controlsObj.AddChild(buildBassControls());
            controlsObj.AddChild(buildTrebleControls());
            controlsObj.AddChild(buildTempoControls());
            controlsObj.AddChild(buildVoiceControls());

            return controlsObj;
        }

        private static GameObject buildVoiceControls() {
            GameObject voices = new GameObject("voices") { FlatLocalPosition = new Point(Display.ScreenCenterX - (4 * (voiceButtonSize + 1)), 80) };
            voices.AddComponent(new VoiceController());

            for (int v = 0; v < 8; v++) {
                GameObject voiceOn = buildSingleVoice(v, Color.WhiteSmoke, Color.DimGray, "on", true);
                GameObject voiceOff = buildSingleVoice(v, Color.DimGray, Color.Black, "off", false);
                voices.AddChild(voiceOn).AddChild(voiceOff);
            }

            return voices;
        }

        private static GameObject buildSingleVoice(int voiceIndex, Color fillColor, Color textColor, string nameSuffix, bool startsEnabled) {
            var voice = new GameObject(string.Format("{0}-" + nameSuffix, voiceIndex + 1)) { FlatLocalPosition = new Point(voiceIndex * (voiceButtonSize + 1), 0), UpdateDisabled = !startsEnabled, DrawDisabled = !startsEnabled };
            voice.AddComponent(new RectRenderer() {
                DrawMode = RectRenderer.DrawModes.Filled,
                FillColor = fillColor,
                Rect = new Rectangle(0, 0, voiceButtonSize, voiceButtonSize)
            });
            voice.AddChild(new GameObject("text") { FlatLocalPosition = new Point(4, 2) }.AddComponent(new TextRenderer() { Text = (voiceIndex + 1).ToString(), Color = textColor, Font = Fonts.Console, Justification = Justifications.TopCenter }));
            return voice;
        }

        private static GameObject buildTempoControls() {
            return new GameObject();
        }

        private static GameObject buildTrebleControls() {
            return new GameObject();
        }

        private static GameObject buildBassControls() {
            return new GameObject();
        }

        private static GameObject buildStereoControls() {
            return new GameObject();
        }

        protected override void Unload() { }
    }
}
