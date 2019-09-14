using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pigeon.data;
using pigeon.input;
using pigeon.core;
using pigeon.legacy.entities;
using pigeon.legacy.graphics;
using pigeon.legacy.graphics.anim;
using pigeon.legacy.graphics.text;
using pigeon.utilities.extensions;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using pigeon.gfx;
using PigeonEngine.utilities.extensions;

namespace pigeon.pgnconsole {
    public class PGNConsole : World {
        private const int bufferMaxLength = 300;

        #region printCharacters
        private static readonly Dictionary<Keys, char> printCharacters = new Dictionary<Keys, char>{
            {Keys.Space, ' '},
            {Keys.A, 'a'},
            {Keys.B, 'b'},
            {Keys.C, 'c'},
            {Keys.D, 'd'},
            {Keys.E, 'e'},
            {Keys.F, 'f'},
            {Keys.G, 'g'},
            {Keys.H, 'h'},
            {Keys.I, 'i'},
            {Keys.J, 'j'},
            {Keys.K, 'k'},
            {Keys.L, 'l'},
            {Keys.M, 'm'},
            {Keys.N, 'n'},
            {Keys.O, 'o'},
            {Keys.P, 'p'},
            {Keys.Q, 'q'},
            {Keys.R, 'r'},
            {Keys.S, 's'},
            {Keys.T, 't'},
            {Keys.U, 'u'},
            {Keys.V, 'v'},
            {Keys.W, 'w'},
            {Keys.X, 'x'},
            {Keys.Y, 'y'},
            {Keys.Z, 'z'},
            {Keys.D0, '0'},
            {Keys.D1, '1'},
            {Keys.D2, '2'},
            {Keys.D3, '3'},
            {Keys.D4, '4'},
            {Keys.D5, '5'},
            {Keys.D6, '6'},
            {Keys.D7, '7'},
            {Keys.D8, '8'},
            {Keys.D9, '9'},
            {Keys.OemMinus, '-'},
            {Keys.OemPlus,'='},
            {Keys.OemOpenBrackets, '['},
            {Keys.OemCloseBrackets, ']'},
            {Keys.OemSemicolon, ';'},
            {Keys.OemQuotes, '\''},
            {Keys.OemPipe, '\\'},
            {Keys.OemComma, ','},
            {Keys.OemPeriod, '.'},
            {Keys.OemQuestion, '/'},
        };

        private static readonly Dictionary<char, char> shiftChars = new Dictionary<char, char>{
            {'0', ')'},
            {'1', '!'},
            {'2', '@'},
            {'3', '#'},
            {'4', '$'},
            {'5', '%'},
            {'6', '^'},
            {'7', '&'},
            {'8', '*'},
            {'9', '('},
            {'-', '_'},
            {'=', '+'},
            {';', ':'},
            {'\'', '\"'},
            {',', '<'},
            {'.', '>'},
            {'/', '?'}
        };
        #endregion

        private string _commandBuffer;

        private string commandBuffer {
            get { return _commandBuffer; }
            set {
                if (value.Length > bufferMaxLength) {
                    return;
                }

                _commandBuffer = value;
                var displayPortion = _commandBuffer.LastByPixels(bufferOverflowWidth, font);
                buffer.Text = string.Format(">{0}", displayPortion);

                updateCursorPosition();
            }
        }

        public readonly PGNConsoleOptions Options;
        private readonly Vector2 bufferPosition;
        private bool initialized;
        private readonly SpriteFont font;
        public readonly MessageLog MessageLog;
        private readonly CommandHistory history;
        private Entity panel;
        private Entity cursor;
        private TextEntity buffer;

        private const int INSET_X = 5;
        private int bufferOverflowWidth;

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
        internal readonly AliasManager AliasManager = new AliasManager();

        private string previousCommand = "";
        private Queue<string> messageQueue = new Queue<string>();

        public bool IsDisplaying { get; private set; }

        public PGNConsole(PGNConsoleOptions options) {
            Options = options;

            font = ResourceCache.Font("console");
            bufferPosition = new Vector2(INSET_X, Pigeon.Renderer.BaseResolutionY - 10);

            int lineWrapWidth = Pigeon.Renderer.BaseResolutionX - 15;
            Vector2 bottomMessagePosition = new Vector2(INSET_X, Pigeon.Renderer.BaseResolutionY - 20);
            MessageLog = new MessageLog(font, lineWrapWidth, bottomMessagePosition, options, EntityRegistry);

            history = new CommandHistory(options.CommandHistory);
        }

        protected override void Load() {
            AliasManager.Load();

            initialized = true;

            var panelTexture = new Texture2D(Renderer.GraphicsDeviceMgr.GraphicsDevice, Pigeon.Renderer.BaseResolutionX, Pigeon.Renderer.BaseResolutionY);

            Color[] panelPixels = new Color[panelTexture.Width * panelTexture.Height];
            for (int i = 0; i < panelPixels.Length; i++) {
                panelPixels[i] = Options.PanelColor;
            }

            panelTexture.SetData(panelPixels);
            panel = new Entity(Vector2.Zero, Image.Create(panelTexture)) { Layer = 0f };
            panel.Graphic.Color.A = 200;
            EntityRegistry.Register(panel);

            Sprite animatedSprite = Sprite.Clone("consoleCursor", @"console\cursor");
            animatedSprite.Loop("flash");
            animatedSprite.Color = Options.BufferColor;
            cursor = new Entity(bufferPosition, animatedSprite) { Layer = .5f };
            EntityRegistry.Register(cursor);

            bufferOverflowWidth = Pigeon.Renderer.BaseResolutionX - INSET_X - (font.MeasureWidth(">") * 3);

            buffer = TextEntity.RegisterStatic(EntityRegistry, "", bufferPosition, font, 1f, Options.BufferColor, Justification.TopLeft);
            commandBuffer = "";

            Log("Console loaded...");
            messageQueue = null;

            AddDebugger = false;
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
                }
            } else if (key == Keys.Down) {
                commandBuffer = history.Previous();
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
            } else if (printCharacters.ContainsKey(key)) {
                history.Reset();
                handleCharacterKeys(key);
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
                        int commandInd = 1;
                        while (commandInd < possibleCommands.Count && !breakLoop) {
                            string command = possibleCommands[commandInd];

                            if (charInd >= command.Length || command[charInd] != character) {
                                charInd--;
                                breakLoop = true;
                            }
                            commandInd++;
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

        private void handleCharacterKeys(Keys key) {
            if (commandBuffer.Length == bufferMaxLength) {
                return;
            }

            char character = printCharacters[key];

            char value;
            if (isCapsShift() && shiftChars.TryGetValue(character, out value)) {
                character = value;
            } else if (isCapsLockXorShift()) {
                if (char.IsLetter(character) && char.IsLower(character)) {
                    character = char.ToUpper(character);
                }
            }

            commandBuffer = string.Concat(commandBuffer, character);
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

                ConsoleCommand command;
                if (engineCommands.TryGetValue(commandName, out command) || gameCommands.TryGetValue(commandName, out command)) {
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

        private void updateCursorPosition() {
            cursor.Position.X = bufferPosition.X + font.MeasureWidth(buffer.Text) + font.Spacing;
        }

        private void handleBackspace() {
            if (commandBuffer.Length > 0) {
                commandBuffer = commandBuffer.Substring(0, commandBuffer.Length - 1);
            }
        }

        private void handleDelete() {
            if (commandBuffer.Length > 0) {
                commandBuffer = commandBuffer.Substring(0, commandBuffer.Length - 1);
            }
        }

        private static bool isCapsShift() {
            bool leftShift = RawKeyboardInput.IsHeld(Keys.LeftShift);
            bool rightShift = RawKeyboardInput.IsHeld(Keys.RightShift);
            return (leftShift || rightShift);
        }

        private static bool isCapsLockXorShift() {
            return isCapsShift() ^ System.Console.CapsLock;
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

        public void Log(object message, bool condition = true) {
            Log(message.ToString(), condition);
        }

        public void Log(string message, bool condition = true) {
            if (!initialized) {
                messageQueue.Enqueue(message);
            } else if (condition) {
                MessageLog.AddMessage(new LogMessage(message, LogMessageTypes.Info));
            }
        }

        public void LogError(string message, bool condition = true) {
            if (!initialized) {
                messageQueue.Enqueue(message);
            } else if (condition) {
                MessageLog.AddMessage(new LogMessage(message, LogMessageTypes.Error));
            }
        }

        private void logCommand(string message) {
            MessageLog.AddMessage(new LogMessage(message, LogMessageTypes.Command));
        }

        public void RepeatPrevious() {
            ExecuteCommand(previousCommand);
        }
    }
}
