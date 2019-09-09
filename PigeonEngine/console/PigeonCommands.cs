using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using pigeon.core;
using pigeon.data;
using pigeon.debug;
using pigeon.gfx;
using pigeon.input;
using pigeon.sound;
using pigeon.gameobject;
using pigeon.time;
using pigeon.utilities.extensions;
using pigeon.winforms;

namespace pigeon.console {
    public static class PigeonCommands {
        public static Dictionary<string, ConsoleCommand> Build() {
            return new Dictionary<string, ConsoleCommand> {
				// console manipulation
				{ "clear", clearScreen },
                { "help", findCommands },
                { "repeat", repeat },

				// binds + aliases
				{ "alias", alias },
                { "unbind", unbind},
                { "bind", bind },
                { "findkey", findKeyboardKeyName },

				// audio
				{ "bgmequalizer", bgmEqualizer },
                { "bgmmutevoice", bgmMuteVoice },
                { "bgmmutevoices", bgmMuteVoices },
                { "bgmplay", bgmPlayTrack },
                { "bgmpause", bgmPause },
                { "bgmresume", bgmResume },
                { "bgmstop", bgmStop },
                { "bgmstereo", bgmStereoDepth },
                { "bgmfade", bgmFade },
                { "bgmtempo", bgmTempo },
                { "bgmvol", setVolumeBgm },
                { "sfxvol", setVolumeSfx },

				// toggled debug info
				{ "togglecd", toggleCd },
                { "togglehitbox", toggleHitbox },
                { "togglemouse", toggleMouse},
                { "toggleposition", togglePosition },
                { "toggleysort", toggleYSort },

				// engine
				{ "exit", exitApp },
                { "gamespeed", gameSpeed},
                { "reset", reset },
                { "savedir", openSaveDir },
                { "showui", showUi },
                { "tick", tick },
				
				// objects
				{ "components", listComponents },
                { "bump", bump },
                { "hide", hideDrawable },
                { "inspect", inspect },
                { "objects", allObjects },

				// graphics
				{ "fullscreen", fullscreen },
                { "lcd", lcd },
                { "scale", setScale },
                { "screenshot", takeScreenshot },

				// world
				{ "pauseworld", pauseWorld },
                { "sandbox", createSandboxWorld },

				// vars
				{ "vload", loadVarPreset },
                { "var", setVar },
                { "vsave", saveVarPreset },
                { "vreset", resetVar },
            };
        }

        #region console manipulation
        private static void clearScreen(string args) {
            for (int i = 0; i < Pigeon.Console.Options.LogHistoryLimit; i++) {
                Pigeon.Console.Log("");
            }
        }

        private static void findCommands(string args) {
            if (string.IsNullOrWhiteSpace(args)) {
                Pigeon.Console.Log("Available engine commands:");
                Pigeon.Console.Log(ConsoleUtilities.BracketedList(Pigeon.Console.EngineCommandNames));

                if (Pigeon.Console.GameCommandNames.Count > 0) {
                    Pigeon.Console.Log("Available game commands:");
                    Pigeon.Console.Log(ConsoleUtilities.BracketedList(Pigeon.Console.GameCommandNames));
                }
            } else {
                var matches = Pigeon.Console.AllCommandNames.FindStringMatches(args);

                if (matches.Count == 0) {
                    Pigeon.Console.Log(string.Format("no commands match for '{0}'", args));
                } else {
                    Pigeon.Console.Log(string.Format("commands matching '{0}':", args));
                    Pigeon.Console.Log(ConsoleUtilities.BracketedList(matches));
                }
            }
        }

        private static void repeat(string args) {
            var count = string.IsNullOrWhiteSpace(args) ? 1 : args.ToFloat();

            for (int i = 0; i < count; i++) {
                Pigeon.Console.RepeatPrevious();
            }
        }
        #endregion

        #region graphics
        private static void lcd(string args) {
            if (string.IsNullOrEmpty(args)) {
                Renderer.LcdDisplay = !Renderer.LcdDisplay;
            } else {
                Renderer.LcdDisplay = args.SplitArgs()[0].ToBool();
            }
        }

        private static void fullscreen(string args) {
            bool before = Pigeon.Renderer.IsFullScreen;
            bool after;

            if (string.IsNullOrEmpty(args)) {
                after = !Pigeon.Renderer.IsFullScreen;
            } else {
                after = args.ToBool();
            }

            Pigeon.Renderer.IsFullScreen = after;

            ConsoleUtilities.LogVariableChange("full screen", before, after);
        }

        private static void setScale(string args) {
            if (string.IsNullOrEmpty(args)) {
                Pigeon.Console.Log("current draw scale: " + Pigeon.Renderer.DrawScale);
                return;
            }
            int after = args.ToInt();

            if (after < 1 || after > 7) {
                Pigeon.Console.LogError("Scale must be between 1 and 7");
            } else {
                int before = Pigeon.Renderer.DrawScale;
                Pigeon.Renderer.DrawScale = after;
                ConsoleUtilities.LogVariableChange("draw scale", before, after);
            }
        }

        private static void takeScreenshot(string args) {
            Pigeon.Renderer.Screenshot();
        }
        #endregion

        #region engine
        private static void exitApp(string args) {
            Pigeon.ExitApp();
        }

        private static void gameSpeed(string args) {
            if (string.IsNullOrEmpty(args)) {
                ConsoleUtilities.LogVariable("game speed", GameSpeed.Multiplier);
            } else {
                var oldValue = GameSpeed.Multiplier;
                var newValue = args.ToFloat();
                GameSpeed.Multiplier = newValue;
                ConsoleUtilities.LogVariableChange("game speed", oldValue, newValue);
            }
        }

        private static void reset(string args) {
            Pigeon.Reset();
        }

        private static void tick(string args) {
            if (!Pigeon.Instance.PauseWorld) {
                Pigeon.Instance.PauseWorld = true;
            }

            Pigeon.Instance.UpdateGameplay();
        }

        private static void showUi(string args) {
            new PigeonUi().Show();
        }

        private static void openSaveDir(string args) {
            Process.Start(PlayerData.UserDataPath);
        }
        #endregion

        #region toggled debug info
        private static void toggleCd(string args) {
            var before = World.DrawColliderDebugInfo;
            var after = !before;
            World.DrawColliderDebugInfo = after;
            ConsoleUtilities.LogVariableChange("collision debug", before, after);
        }

        private static void toggleHitbox(string args) {
            var before = GameObject.DrawHitboxesGlobal;
            var after = !before;
            GameObject.DrawHitboxesGlobal = after;
            ConsoleUtilities.LogVariableChange("hitbox debug", before, after);
        }

        private static void toggleMouse(string args) {
            var before = MouseReader.UpdateState;
            var after = !before;
            MouseReader.UpdateState = after;
            ConsoleUtilities.LogVariableChange("mouse updates", before, after);
        }

        private static void togglePosition(string args) {
            var before = GameObject.DrawPositionsGlobal;
            var after = !before;
            GameObject.DrawPositionsGlobal = after;
            ConsoleUtilities.LogVariableChange("position debug", before, after);
        }

        private static void toggleYSort(string args) {
            var before = YSorter.DrawEnabled;
            var after = !before;
            YSorter.DrawEnabled = after;
            ConsoleUtilities.LogVariableChange("ysort debug", before, after);
        }
        #endregion

        #region objects
        private static void allObjects(string args) {
            StringBuilder builder = new StringBuilder();
            GameObject obj = string.IsNullOrEmpty(args) ? Pigeon.World.ObjRoot : Pigeon.World.FindObj(args);
            describeChildren(obj, builder);
        }

        private static void bump(string args) {
            var splitArgs = args.SplitArgs();
            var obj = Pigeon.World.FindObj(splitArgs[1]);

            var bumpDir = Point.Zero;
            switch (splitArgs[0].ToLower()) {
                case "up":
                    bumpDir.Y = -1;
                    break;
                case "down":
                    bumpDir.Y = 1;
                    break;
                case "left":
                    bumpDir.X = -1;
                    break;
                case "right":
                    bumpDir.X = 1;
                    break;
            }

            obj.FlatLocalPosition = obj.FlatLocalPosition + bumpDir;
        }

        private static void describeChildren(GameObject obj, StringBuilder builder) {
            var children = obj.GetChildren();
            if (children == null || children.Count == 0) {
                Pigeon.Console.Log("object has no children");
                return;
            }

            Pigeon.Console.Log("children:");
            var treeChildren = from child in children
                               where child.GetChildren() != null && child.GetChildren().Count > 0
                               orderby child.Name ascending
                               select child.Name;

            foreach (var child in treeChildren) {
                builder.Append('+');
                builder.Append(child);
                builder.Append(' ');
            }

            var leafChildren = from child in children
                               where child.GetChildren() == null || child.GetChildren().Count == 0
                               orderby child.Name ascending
                               select child.Name;

            foreach (var child in leafChildren) {
                builder.Append('-');
                builder.Append(child);
                builder.Append(' ');
            }

            Pigeon.Console.Log(builder.ToString());
        }

        private static void inspect(string args) {
            var squabject = Pigeon.World.FindObj(args);
            if (squabject != null) {
                Pigeon.Console.Log(squabject.ToString());
                return;
            }

            Pigeon.Console.LogError("object does not exist");
        }

        private static void hideDrawable(string args) {
            var obj = Pigeon.World.FindObj(args);

            if (obj == null) {
                return;
            }

            Component drawable = obj.GetComponent<SpriteRenderer>();

            if (drawable == null) {
                drawable = obj.GetComponent<ImageRenderer>();
            }

            if (drawable == null) {
                drawable = obj.GetComponent<TextRenderer>();
            }

            if (drawable != null) {
                drawable.Enabled = !drawable.Enabled;
                ConsoleUtilities.LogVariableChange(args + " visible", !drawable.Enabled, drawable.Enabled);
            } else {
                Pigeon.Console.Log(args + " no drawable components");
            }
        }

        private static void listComponents(string args) {
            var obj = Pigeon.World.FindObj(args);

            if (obj.components != null && obj.components.Count > 0) {
                var componentNames = new List<string>();
                foreach (var component in obj.components) {
                    componentNames.Add(component.GetType().Name);
                }

                Pigeon.Console.Log(ConsoleUtilities.BracketedList(componentNames));
            } else {
                Pigeon.Console.Log("object has no components");
            }
        }
        #endregion

        #region world
        private static void createSandboxWorld(string args) {
            var splitArgs = args.SplitArgs();

            Color background = Pigeon.EngineBkgdColor;

            if (splitArgs.Length == 3) {
                background.R = splitArgs[0].ToByte();
                background.G = splitArgs[1].ToByte();
                background.B = splitArgs[2].ToByte();
            }

            Pigeon.SetWorld(new EmptyWorld { BackgroundColor = background });
        }

        private static void pauseWorld(string args) {
            bool before = Pigeon.Instance.PauseWorld;
            bool after;
            if (string.IsNullOrEmpty(args)) {
                after = !Pigeon.Instance.PauseWorld;
            } else {
                after = args.ToBool();
            }

            Pigeon.Instance.PauseWorld = after;
            ConsoleUtilities.LogVariableChange("pause world", before, after);
        }
        #endregion

        #region audio
        private static void bgmEqualizer(string args) {
            if (string.IsNullOrWhiteSpace(args)) {
                Pigeon.Console.Log("bgmequalizer <treble> <bass>");
                Pigeon.Console.Log("   treble: -50 to 5 (def 0)");
                Pigeon.Console.Log("   bass: 1 to 16000 (def 90)");
            } else {
                var splitArgs = args.SplitArgs();

                double treble = splitArgs[0].ToDouble();
                double bass = splitArgs[1].ToFloat();

                Music.SetEqualizer(treble, bass);
            }
        }

        private static void bgmMuteVoice(string args) {
            if (string.IsNullOrWhiteSpace(args)) {
                Pigeon.Console.Log("bgmmutevoice <index> <mute>");
                Pigeon.Console.Log("   index: 0 to 7 (channel to mute)");
                Pigeon.Console.Log("   mute: 0 or 1 (1 to mute)");
            } else {
                var splitArgs = args.SplitArgs();
                Music.SetVoiceMute(splitArgs[0].ToInt(), splitArgs[1].ToInt());
            }
        }

        private static void bgmMuteVoices(string args) {
            //			var mutingMask = 31;
            var mutingMask = Convert.ToInt32(args, 2);
            Music.MuteVoices(mutingMask);
        }

        private static void bgmPlayTrack(string args) {
            if (string.IsNullOrWhiteSpace(args)) {
                Pigeon.Console.Log("unknown track");
            } else {
                if (!args.StartsWith("music/")) {
                    args = "music/" + args;
                }

                if (!args.EndsWith(".nsf")) {
                    args += ".nsf";
                }
                Music.Stop();
                Music.Load(args);
                Music.Play();
            }
        }

        private static void bgmPause(string args) {
            Music.Pause();
        }

        private static void bgmResume(string args) {
            Music.Play();
        }

        private static void bgmStop(string args) {
            Music.Stop();
        }

        private static void bgmStereoDepth(string args) {
            if (string.IsNullOrWhiteSpace(args)) {
                Pigeon.Console.Log("bgmstereodepth <depth>");
                Pigeon.Console.Log("	depth: 0.0 to 1.0 (def 0)");
            } else {
                Music.SetStereoDepth(args.ToDouble());
            }
        }

        private static void bgmFade(string args) {
            if (string.IsNullOrWhiteSpace(args)) {
                Pigeon.Console.Log("bgmfade <msLength>");
            } else {
                Music.SetFade(args.ToInt());
            }
        }

        private static void bgmTempo(string args) {
            if (string.IsNullOrWhiteSpace(args)) {
                Pigeon.Console.Log("bgmtempo <tempo>");
                Pigeon.Console.Log("	tempo: 0.5 to 2.0 (def 1)");
            } else {
                Music.SetTempo(args.ToDouble());
            }
        }

        private static void setVolumeBgm(string args) {
            double? value = args.ToUnitInterval();

            if (value != null) {
                //				var before = Audio.BgmVolume;
                //				var after = (float) value;
                //				Audio.BgmVolume = after;
                //				ConsoleUtilities.LogVariableChange("bgm vol", before, after);
            } else {
                Pigeon.Console.LogError("Invalid volume; enter a decimal value from 0.0 to 1.0");
            }
        }

        private static void setVolumeSfx(string args) {
            double? value = args.ToUnitInterval();

            if (value != null) {
                var before = Audio.SfxVolume;
                var after = (float) value;
                Audio.SfxVolume = after;
                ConsoleUtilities.LogVariableChange("sfx vol", before, after);
            } else {
                Pigeon.Console.LogError("Invalid volume; enter a decimal value from 0.0 to 1.0");
            }
        }
        #endregion

        #region vars
        private static void setVar(string args) {
            if (args == string.Empty) {
                Pigeon.Console.Log("select category:");
                var allCategories = Const.Categories;
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var category in allCategories) {
                    stringBuilder.Append("+");
                    stringBuilder.Append(category);
                    stringBuilder.Append(" ");
                }
                Pigeon.Console.Log(stringBuilder.ToString());
                return;
            }

            var splitArgs = args.SplitArgs();
            switch (splitArgs.Length) {
                case 1:
                    Pigeon.Console.Log("select variable:");
                    var vars = Const.GetVarsForCategory(splitArgs[0]);
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var variable in vars) {
                        stringBuilder.Append("+");
                        stringBuilder.Append(variable);
                        stringBuilder.Append(" ");
                    }
                    Pigeon.Console.Log(stringBuilder.ToString());
                    break;
                case 2:
                    Pigeon.Console.Log("current value: " + Const.GetFloat(splitArgs[0], splitArgs[1]));
                    break;
                case 3:
                    var before = Const.GetFloat(splitArgs[0], splitArgs[1]);
                    Const.SetVar(splitArgs[0], splitArgs[1], splitArgs[2].ToFloat());
                    var after = Const.GetFloat(splitArgs[0], splitArgs[1]);
                    ConsoleUtilities.LogVariableChange(splitArgs[0] + "." + splitArgs[1], before, after);
                    break;
                default:
                    Pigeon.Console.LogError("too many arguments provided.");
                    break;
            }
        }

        private static void resetVar(string args) {
            Const.ResetToDefaults();
            Pigeon.Console.Log("all variables restored to defaults");
        }

        private static void saveVarPreset(string args) {
            PlayerData.CreateDirectory(@"constants");
            Const.SavePreset(@"constants\" + args + ".cfg");
            Pigeon.Console.Log("created preset '" + args + "' from current variables");
        }

        private static void loadVarPreset(string args) {
            if (args == string.Empty) {
                var presets = PlayerData.GetFileList(@"constants\*.cfg", false);

                StringBuilder stringBuilder = new StringBuilder();
                foreach (var preset in presets) {
                    stringBuilder.Append("*");
                    stringBuilder.Append(preset);
                    stringBuilder.Append(" ");
                }

                Pigeon.Console.Log("available presets:");
                Pigeon.Console.Log(stringBuilder.ToString());

            } else {
                Const.LoadPreset(@"constants\" + args + ".cfg");
                Pigeon.Console.Log("loaded preset '" + args + "'");
            }
        }
        #endregion

        #region binds/aliases
        private static void bind(string args) {
            string[] splitArgs = args.Split(new[] { ' ' }, 2);

            Keys key = KeyBinds.ParseToKey(splitArgs[0]);

            // TODO: if no args passed in, then list all current binds
            if (splitArgs.Length == 1) {    // if no command is given, just display current bind for that key
                displayKeybind(key);
            } else {    // create a new bind
                KeyBinds.BindKey(key, splitArgs[1]);
                displayKeybind(key);
            }
        }

        private static void unbind(string args) {
            if (args == "all") {
                KeyBinds.Reset();
                Pigeon.Console.Log("all custom binds reset");
            } else {
                var key = KeyBinds.ParseToKey(args);
                KeyBinds.UnbindKey(key);
                displayKeybind(key);
            }
        }

        private static void displayKeybind(Keys key) {
            Pigeon.Console.Log(string.Format("{0}: {1}", key, KeyBinds.GetKeyBind(key) ?? "<unbound>"));
        }

        private static void findKeyboardKeyName(string args) {
            var matches = EnumUtil.GetValues<Keys>().Select(k => k.ToString()).FindStringMatches(args);

            if (matches.Count == 0) {
                Pigeon.Console.Log(string.Format("no keys match for '{0}'", args));
            } else {
                Pigeon.Console.Log(string.Format("keys matching '{0}':", args));
                Pigeon.Console.Log(ConsoleUtilities.BracketedList(matches));
            }
        }

        private static void alias(string args) {
            var manager = Pigeon.Console.AliasManager;

            if (string.IsNullOrEmpty(args)) {
                var names = manager.GetNames();
                Pigeon.Console.Log(names.Count > 0 ? ConsoleUtilities.BracketedList(names) : "no aliases currently defined");
                return;
            }

            var splitArgs = args.Split(new[] { ' ' }, 2);
            var aliasName = splitArgs[0];

            if (splitArgs.Length == 1) {
                if (manager.Exists(aliasName)) {
                    var commands = manager.Get(aliasName);

                    Pigeon.Console.Log(string.Format("{0}:", aliasName));
                    foreach (var command in commands) {
                        Pigeon.Console.Log(string.Format("  -{0}", command));
                    }
                } else {
                    Pigeon.Console.Log(string.Format("no alias with name '{0}'", splitArgs[0]));
                }
                return;
            }

            string aliasValue = splitArgs[1];
            manager.Set(aliasName, aliasValue);
        }
        #endregion
    }
}
