using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pigeon.data;
using pigeon.input;
using pigeon.core;
using pigeon.legacy.entities;
using pigeon.legacy.graphics.anim;
using pigeon.legacy.graphics.text;
using pigeon.utilities.extensions;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using pigeon.gfx;
using PigeonEngine.utilities.extensions;
using pigeon.legacy.graphics;

namespace pigeon.pgnconsole {
    public class PGNConsole : World {
        private const int bufferMaxLength = 300;

        private int _commandCursorIndex = 0;

        private int commandCursorIndex {
            get { return _commandCursorIndex; }
            set {
                _commandCursorIndex = value.Clamp(0, commandBuffer.Length);

                cursor.Position.X = bufferHomePosition.X + font.MeasureWidth(buffer.Text.Substring(0, _commandCursorIndex) + " ") + font.Spacing - 1;
            }
        }

        private string _commandBuffer;

        private string commandBuffer {
            get { return _commandBuffer; }
            set {
                if (value.Length > bufferMaxLength) {
                    return;
                }

                _commandBuffer = value;
                var displayPortion = _commandBuffer.LastByPixels(lineOverflowWidth, font);
                buffer.Text = string.Format(">{0}", displayPortion);

                if (commandCursorIndex >= value.Length) {
                    commandCursorIndex = value.Length;
                }
            }
        }

        internal readonly PGNConsoleOptions options;
        private readonly SpriteFont font;
        private readonly Vector2 bufferHomePosition;
        private readonly CommandHistory history;
        internal MessageLog messageLog;
        internal readonly AliasManager AliasManager = new AliasManager();

        private Entity panel;
        private Entity cursor;
        private TextEntity buffer;

        private int lineOverflowWidth;

        internal List<string> AllCommandNames {
            get {
                var all = new List<string>();
                all.AddRange(EngineCommandNames);
                all.AddRange(GameCommandNames);
                return all;
            }
        }

        internal List<string> EngineCommandNames { get { return engineCommands.Keys.ToList(); } }
        internal List<string> GameCommandNames { get { return gameCommands.Keys.ToList(); } }
        private readonly Dictionary<string, ConsoleCommand> engineCommands = new Dictionary<string, ConsoleCommand>();
        private readonly Dictionary<string, ConsoleCommand> gameCommands = new Dictionary<string, ConsoleCommand>();

        private string previousCommand = "";

        public bool IsDisplaying { get; private set; }

        public PGNConsole(PGNConsoleOptions options) {
            this.options = options;

            font = ResourceCache.Font("console");
            bufferHomePosition = new Vector2(this.options.TextInset, this.options.PanelHeight - (this.options.TextInset * 2));

            history = new CommandHistory(options.CommandHistory);
        }

        protected override void Load() {
            AliasManager.Load();

            var panelTexture = new Texture2D(Renderer.GraphicsDeviceMgr.GraphicsDevice, Pigeon.Renderer.BaseResolutionX, this.options.PanelHeight);

            Color[] panelPixels = new Color[panelTexture.Width * panelTexture.Height];
            for (int i = 0; i < panelPixels.Length; i++) {
                panelPixels[i] = options.PanelColor;
            }

            panelTexture.SetData(panelPixels);
            panel = new Entity(Vector2.Zero, Image.Create(panelTexture)) { Layer = 0f };
            EntityRegistry.Register(panel);

            Sprite cursorSprite = Sprite.Clone("consoleCursor", @"console\cursor");
            cursorSprite.Loop("flash");
            cursorSprite.Color = options.CursorColor;
            cursor = new Entity(bufferHomePosition - new Vector2(0, 1), cursorSprite) { Layer = .5f };
            EntityRegistry.Register(cursor);

            lineOverflowWidth = Pigeon.Renderer.BaseResolutionX - options.TextInset - (font.MeasureWidth(">") * 3);
            buffer = TextEntity.RegisterStatic(EntityRegistry, "", bufferHomePosition, font, 1f, options.BufferColor, Justification.TopLeft);
            commandBuffer = "";

            int lineSpacing = font.MeasureHeight(">");
            Vector2 bottomMessagePosition = new Vector2(bufferHomePosition.X, bufferHomePosition.Y - lineSpacing);
            messageLog = new MessageLog(font, lineOverflowWidth, bottomMessagePosition, lineSpacing, options, EntityRegistry);

            AddDebugger = false;

            Log("Console loaded...");
        }

        protected override void Unload() { }

        public override void Update() {
            base.Update();

            if (RawKeyboardInput.IsPressed(Keys.OemTilde)) {
                toggleDisplay();
            }

            if (!IsDisplaying) {
                return;
            }

            handleKeyboardInput();
        }

        public override void Draw() {
            if (IsDisplaying) {
                Pigeon.Renderer.RenderOverlay(EntityRegistry.Draw);
            }
        }

        internal void AddGlobalCommands(Dictionary<string, ConsoleCommand> newCommands) {
            foreach (var command in newCommands) {
                engineCommands.Add(command.Key, command.Value);
            }
        }

        public void AddWorldCommands(Dictionary<string, ConsoleCommand> newCommands) {
            foreach (var command in newCommands) {
                gameCommands.Add(command.Key, command.Value);
            }
        }

        public void ResetWorldCommands() {
            gameCommands.Clear();
        }

        private void handleKeyboardInput() {
            Keys key = RawKeyboardInput.GetSingleJustPressedKey();

            if (key == Keys.None) {
                return;
            }

            if (key == Keys.Up) {
                var command = history.Next();
                if (command != null) {
                    commandBuffer = command;
                    commandCursorIndex = commandBuffer.Length;
                }
            } else if (key == Keys.Down) {
                commandBuffer = history.Previous();
                commandCursorIndex = commandBuffer.Length;
            } else if (key == Keys.Left) {
                commandCursorIndex--;
            } else if (key == Keys.Right) {
                commandCursorIndex++;
            } else if (key == Keys.Back) {
                history.Reset();
                handleBackspace();
            } else if (key == Keys.Delete) {
                history.Reset();
                handleDelete();
            } else if (key == Keys.Enter) {
                history.Reset();
                commitCommand();
            } else if (key == Keys.Tab) {
                history.Reset();
                handleAutocomplete();
            } else if (key == Keys.Home) {
                commandCursorIndex = 0;
            } else if (key == Keys.End) {
                commandCursorIndex = commandBuffer.Length;
            } else if (key.IsPrintable()) {
                history.Reset();
                handleCharacters(key.ToChar());
            }
        }

        private void handleAutocomplete() {
            if (commandBuffer.Length == 0) {
                return;
            }

            List<string> possibleCommands = new List<string>();

            int minMatchingChars = 0;

            foreach (var commandPair in engineCommands) {
                var commandName = commandPair.Key;
                if (commandName.StartsWith(commandBuffer)) {
                    minMatchingChars = Math.Min(minMatchingChars, commandName.Length - commandBuffer.Length);
                    possibleCommands.Add(commandName);
                }
            }

            foreach (var commandPair in gameCommands) {
                var commandName = commandPair.Key;
                if (commandName.StartsWith(commandBuffer)) {
                    minMatchingChars = Math.Min(minMatchingChars, commandName.Length - commandBuffer.Length);
                    possibleCommands.Add(commandName);
                }
            }

            switch (possibleCommands.Count) {
                case 0:
                    Log(string.Format("no commands starting with '{0}'...", commandBuffer));
                    break;
                case 1:
                    commandBuffer = possibleCommands[0];
                    break;
                default:
                    var referenceCommand = possibleCommands[0];

                    bool breakLoop = false;
                    int charInd = 0;
                    while (charInd < referenceCommand.Length) {
                        // check every other command to see how many of their letters match
                        var character = referenceCommand[charInd];
                        for (int commandInd = 1; commandInd < possibleCommands.Count && !breakLoop; commandInd++) {
                            string command = possibleCommands[commandInd];

                            if (charInd >= command.Length || command[charInd] != character) {
                                charInd--;
                                breakLoop = true;
                            }
                        }

                        if (!breakLoop && charInd < referenceCommand.Length - 1) {
                            charInd++;
                        } else {
                            break;
                        }
                    }

                    if (charInd + 1 > commandBuffer.Length) {
                        commandBuffer = referenceCommand.Substring(0, charInd + 1);
                    } else {
                        Log(string.Format("commands starting with '{0}': ", commandBuffer));
                        Log(ConsoleUtilities.BracketedList(possibleCommands));
                    }

                    break;
            }
        }

        private void handleCharacters(char character) {
            if (commandBuffer.Length == bufferMaxLength) {
                return;
            }

            if (isShiftHeld() && character.TryGetShiftChar(out char value)) {
                character = value;
            } else if (isCapsLockXorShift()) {
                if (char.IsLetter(character) && char.IsLower(character)) {
                    character = char.ToUpper(character);
                }
            }

            commandBuffer = commandBuffer.Insert(commandCursorIndex, character.ToString());
            commandCursorIndex++;
        }

        private void commitCommand() {
            string command = commandBuffer;
            ExecuteCommand(command, true);
        }

        public void ExecuteCommand(string commandFull, bool addToHistory = false) {
            string lowercaseBuffer = commandFull;

            if (addToHistory) {
                logCommand(">" + commandFull);
                history.Commit(commandFull);
                commandBuffer = "";
            }

            try {
                string[] splitBuffer = lowercaseBuffer.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (splitBuffer.Length == 0) {
                    return;
                }

                string commandName = splitBuffer[0].ToLower();

                if (engineCommands.TryGetValue(commandName, out ConsoleCommand command) || gameCommands.TryGetValue(commandName, out command)) {
                    string args = splitBuffer.Length == 2 ? splitBuffer[1] : string.Empty;
                    command.Invoke(args);
                } else if (AliasManager.Exists(commandName)) {
                    List<string> aliasCommands = AliasManager.Get(commandName);
                    foreach (var aliasCommand in aliasCommands) {
                        ExecuteCommand(aliasCommand);
                    }
                } else {
                    LogError("Command name not recognized: " + commandName);
                }

                previousCommand = (commandName == "repeat") ? previousCommand : commandFull;
            } catch (Exception e) {
                LogError("command failed: " + commandFull);
                Log(e.Message);
            }
        }

        private void handleBackspace() {
            if (commandBuffer.Length > 0 && commandCursorIndex > 0) {
                bool isCursorAtEnd = commandCursorIndex == commandBuffer.Length;

                commandBuffer = commandBuffer.Remove(commandCursorIndex - 1, 1);

                if (!isCursorAtEnd) { 
                    commandCursorIndex--;
                }
            }
        }

        private void handleDelete() {
            if (commandBuffer.Length > 0 && commandCursorIndex < commandBuffer.Length) {
                commandBuffer = commandBuffer.Remove(commandCursorIndex, 1);
            }
        }

        private static bool isShiftHeld() {
            bool leftShift = RawKeyboardInput.IsHeld(Keys.LeftShift);
            bool rightShift = RawKeyboardInput.IsHeld(Keys.RightShift);
            return leftShift || rightShift;
        }

        private static bool isCapsLockXorShift() {
            return isShiftHeld() ^ Console.CapsLock;
        }

        private void toggleDisplay() {
            IsDisplaying = !IsDisplaying;
        }

        public void DebugLog(string message) {
            Log(message);
        }

        public void DebugLogToFile(string message, string logFilename = @"logs\debug.log") {
            PlayerData.AppendToFile(logFilename, message);
        }

        public void Log(object message) {
            Log(message.ToString());
        }

        public void Log(string message) {
            messageLog.AddMessage(new LogMessage(message, LogMessageTypes.Info));
        }

        public void LogError(string message) {
            messageLog.AddMessage(new LogMessage(message, LogMessageTypes.Error));
        }

        private void logCommand(string message) {
            messageLog.AddMessage(new LogMessage(message, LogMessageTypes.Command));
        }

        public void RepeatPrevious() {
            ExecuteCommand(previousCommand);
        }
    }
}
