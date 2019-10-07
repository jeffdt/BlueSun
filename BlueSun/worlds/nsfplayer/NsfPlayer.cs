using BlueSun.resources;
using BlueSun.src.parameters;
using BlueSun.worlds.nsfplayer.propertyControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using pigeon.core;
using pigeon.gameobject;
using pigeon.gfx.drawable.shape;
using pigeon.gfx.drawable.text;
using pigeon.sound.music;
using static BlueSun.worlds.nsfplayer.propertyControllers.SliderControls;

namespace BlueSun.worlds.nsfplayer {
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

            GameObject songTextObj = new GameObject("songtext") { Layer = .9f, LocalPosition = new Point(0, -10) };
            songTextObj.AddComponent(new TextRenderer() { Text = "", Font = Fonts.Console, Color = Palette.White, Justification = Justifications.Center });
            musicObj.AddChild(songTextObj);

            GameObject flavorTextObj = new GameObject("flavortext") { Layer = .9f, LocalPosition = new Point(2, 0) };
            flavorTextObj.AddComponent(new TextRenderer() { Text = "from the album:", Font = Fonts.Console, Color = Palette.LightGray, Justification = Justifications.Center });
            musicObj.AddChild(flavorTextObj);

            GameObject folderTextObj = new GameObject("albumtext") { Layer = .9f, LocalPosition = new Point(0, 10) };
            folderTextObj.AddComponent(new TextRenderer() { Text = "", Font = Fonts.Console, Color = Palette.LightGray, Justification = Justifications.Center });
            musicObj.AddChild(folderTextObj);
            return musicObj;
        }

        private static GameObject buildControls() {
            GameObject controlsObj = new GameObject("controls").AddComponent(new SongControls());

            controlsObj.AddChild(buildVoiceControls());
            
            controlsObj.AddChild(buildTempoControl());
            controlsObj.AddChild(buildStereoControls());
            controlsObj.AddChild(buildBassControls());
            controlsObj.AddChild(buildTrebleControls());

            return controlsObj;
        }

        private static GameObject buildVoiceControls() {
            GameObject voices = new GameObject("voices") { LocalPosition = new Point(Display.ScreenCenterX - 4 * (voiceButtonSize + 1) + 1, 80), Layer = .1f };
            voices.AddComponent(new VoiceControls());

            for (int v = 0; v < 8; v++) {
                GameObject voiceOn = buildSingleVoice(v, Color.WhiteSmoke, Color.DimGray, "on", .2f);
                GameObject voiceOff = buildSingleVoice(v, Color.DimGray, Color.WhiteSmoke, "off", .1f);
                voices.AddChild(voiceOn).AddChild(voiceOff);
            }

            return voices;
        }

        private static GameObject buildSingleVoice(int voiceIndex, Color fillColor, Color textColor, string nameSuffix, float layer) {
            var voice = new GameObject(string.Format("{0}-" + nameSuffix, voiceIndex + 1)) { LocalPosition = new Point(voiceIndex * (voiceButtonSize + 1), 0), Layer = layer };
            voice.AddComponent(new RectRenderer() {
                DrawStyle = ShapeDrawStyles.FilledBordered,
                BorderThickness = 1,
                BorderColor = Color.WhiteSmoke,
                FillColor = fillColor,
                Rect = new Rectangle(0, 0, voiceButtonSize, voiceButtonSize)
            });
            voice.AddChild(new GameObject("text") { Layer = .05f, LocalPosition = new Point(4, 2) }.AddComponent(new TextRenderer() { Text = (voiceIndex + 1).ToString(), Color = textColor, Font = Fonts.Console, Justification = Justifications.TopCenter }));
            return voice;
        }

        private static GameObject buildTempoControl() {
            string controlType = "tempo";
            int startY = 110;
            float minValue = 0.01f;
            float maxValue = 2f;
            float defaultValue = 1.0f;
            float bumpAmount = .1f;
            Keys bumpLeftKey = Keys.A;
            Keys bumpRightKey = Keys.S;
            AdjustControl onAdjust = (float value) => Music.Tempo = value;

            return buildSliderControl(controlType, startY, minValue, maxValue, defaultValue, bumpAmount, bumpLeftKey, bumpRightKey, onAdjust);
        }

        private static GameObject buildStereoControls() {
            string controlType = "stereo";
            int startY = 140;
            float minValue = 0;
            float maxValue = 1;
            float defaultValue = 1;
            float bumpAmount = .1f;
            Keys bumpLeftKey = Keys.D;
            Keys bumpRightKey = Keys.F;
            AdjustControl onAdjust = (float value) => Music.StereoDepth = value;

            return buildSliderControl(controlType, startY, minValue, maxValue, defaultValue, bumpAmount, bumpLeftKey, bumpRightKey, onAdjust);
        }

        private static GameObject buildTrebleControls() {
            string controlType = "treble";
            int startY = 170;
            float minValue = -50;
            float maxValue = 5;
            float defaultValue = 0;
            float bumpAmount = .1f;
            Keys bumpLeftKey = Keys.G;
            Keys bumpRightKey = Keys.H;
            AdjustControl onAdjust = (float value) => Music.Treble = value;

            return buildSliderControl(controlType, startY, minValue, maxValue, defaultValue, bumpAmount, bumpLeftKey, bumpRightKey, onAdjust);
        }

        private static GameObject buildBassControls() {
            string controlType = "bass";
            int startY = 200;
            float minValue = 1;
            float maxValue = 1000;
            float defaultValue = 90;
            float bumpAmount = .1f;
            Keys bumpLeftKey = Keys.J;
            Keys bumpRightKey = Keys.K;
            AdjustControl onAdjust = (float value) => Music.Bass = value;

            return buildSliderControl(controlType, startY, minValue, maxValue, defaultValue, bumpAmount, bumpLeftKey, bumpRightKey, onAdjust);
        }



        private static GameObject buildSliderControl(string controlType, int startY, float minValue, float maxValue, float defaultValue, float bumpAmount, Keys bumpLeftKey, Keys bumpRightKey, AdjustControl onAdjust) {
            GameObject controlObj = new GameObject(controlType) { LocalPosition = new Point(Display.ScreenCenterX, startY) };
            controlObj.AddComponent(new SliderControls() { Min = minValue, Max = maxValue, Default = defaultValue, BumpAmount = bumpAmount, LeftBumpKey = bumpLeftKey, RightBumpKey = bumpRightKey, OnAdjustControl = onAdjust });
            controlObj.AddChild(new GameObject("label-name").AddComponent(new TextRenderer() { Font = Fonts.Console, Text = controlType, Color = Color.WhiteSmoke, Justification = Justifications.Center }));
            controlObj.AddChild(new GameObject("slider-rod") { LocalPosition = new Point(0, 8) }.AddComponent(new RectRenderer() {
                Rect = new Rectangle(-50, 0, 100, 1),
                DrawStyle = ShapeDrawStyles.Filled,
                FillColor = Color.LightGray
            }));
            controlObj.AddChild(new GameObject("slider-switch") { LocalPosition = new Point(0, 4), Layer = .1f }.AddComponent(new RectRenderer() {
                Rect = new Rectangle(-1, 0, 3, 9),
                DrawStyle = ShapeDrawStyles.FilledBordered,
                BorderThickness = 1,
                BorderColor = Color.WhiteSmoke,
                FillColor = Color.DimGray
            }));
            return controlObj;
        }

        protected override void Unload() { }
    }
}
