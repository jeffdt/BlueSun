using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using pigeon.collision;
using pigeon.console;
using pigeon.core;
using pigeon.core.events;
using pigeon.data;
using pigeon.debug;
using pigeon.gfx;
using pigeon.input;
using pigeon.legacy.graphics.anim;
using pigeon.sound;
using pigeon.time;
using System;

namespace pigeon {
    public abstract class Pigeon : Game {
        // set these for each new game
        protected abstract string WindowTitle { get; }
        protected abstract Renderer GetRenderer { get; }
        protected abstract string Version { get; }
        protected abstract int FrameRate { get; }
        protected abstract ConsoleOptions ConsoleOpts { get; }
        protected abstract bool StartMouseVisible { get; }
        protected abstract Color DefaultBkgdColor { get; }
        protected abstract TextureTemplateProcessor TemplateProcessor { get; }
        protected abstract World InitialWorld { get; }
        protected abstract void Load();
        protected abstract void InitializeGame();

        public static Pigeon Instance;
        public static console.Console Console;
        public static Renderer Renderer;
        public static readonly EventRegistry GameEventRegistry = new EventRegistry();
        public static readonly EventRegistry EngineEventRegistry = new EventRegistry();
        public static readonly Camera Camera = new Camera();
        public static ContentManager ContentManager;
        public static bool IsInFocus;
        public static World World { get; private set; }

        public static InputManager InputManager = new InputManager();

        internal static Color EngineBkgdColor;

        public bool PauseWorld;
        internal static World nextWorld = null;
        private static bool isNextWorldAlreadyInitialized;
        private static World lastWorld;

        public static bool MouseVisible { set { Instance.IsMouseVisible = value; } }

        protected Pigeon() {
            ContentManager = Content;
            ContentManager.RootDirectory = @"Content";

            Renderer.GraphicsDeviceMgr = new GraphicsDeviceManager(this);

            Instance = this;
        }

        protected sealed override void LoadContent() {
            Renderer = GetRenderer;

            TargetElapsedTime = TimeSpan.FromSeconds(1 / (float) FrameRate);
            Instance.Window.Title = WindowTitle;

            ResourceCache.Initialize(TemplateProcessor);

            GameData.Initialize();
            PlayerData.Initialize();

            Renderer.spriteBatch = new SpriteBatch(GraphicsDevice);

            loadResources();

            Audio.Initialize();
            Music.Initialize();

            Load();

            Console = new console.Console(ConsoleOpts);
            Console.LoadContent();
            Console.AddGlobalCommands(PigeonCommands.Build());

            EngineBkgdColor = DefaultBkgdColor;

            World = InitialWorld;
            World.LoadContent();
        }

        private static void loadResources() {
            string[] texturePaths = GameData.GetContentFiles(@"textures");
            var templates = GameData.Read(@"textures\templates.cfg");
            ResourceCache.LoadTextures(texturePaths, templates);

            string[] sfxPaths = GameData.GetContentFiles(@"sfx");
            ResourceCache.LoadSounds(sfxPaths);

            Font[] fonts = GameData.Deserialize<Font[]>(@"fonts\fonts.xml");
            if (fonts != null) {
                ResourceCache.LoadFonts(fonts);
            }

            HitboxSetManager.Load(@"data\hitboxes.dat");
            Sprite.LoadCompact(@"textures\compactAnimations.cfg");
        }

        protected override void Initialize() {
            base.Initialize();

            MouseReader.Initialize();
            RawKeyboardInput.Initialize();
            Legacy_GamepadReader.Initialize();
            GameSpeed.Reinitialize();
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            DebugHelper.Initialize();

            MouseVisible = StartMouseVisible;

            InitializeGame();
        }

        protected override void UnloadContent() {
            World.UnloadContent();
        }

        protected override void Update(GameTime gameTime) {
            if (nextWorld != null && World != nextWorld) {
                swapWorld();
            }

            IsInFocus = IsActive;

            InputManager.Update();
            RawKeyboardInput.Update();
            Legacy_GamepadReader.Update();
            MouseReader.Update();

            Time.Set(gameTime);

            base.Update(gameTime);

            Console.Update();

            if (!Console.IsDisplaying && !PauseWorld) {
                UpdateGameplay();
            }
        }

        public void UpdateGameplay() {
            World.Update();
            Audio.Update();
        }

        public static void SetWorld(World world, bool isAlreadyInitialized = false) {
            if (nextWorld != null) {
                Console.LogError("NextWorld has already been set.");
                Console.LogError("Cannot override " + nextWorld + " with " + world);
            }

            nextWorld = world;
            isNextWorldAlreadyInitialized = isAlreadyInitialized;
        }

        private static void swapWorld() {
            if (!isNextWorldAlreadyInitialized) {
                Camera.Position = Vector2.Zero;
                World.UnloadContent();
                Console.ResetWorldCommands();
            }

            lastWorld = World;
            World = nextWorld;
            nextWorld = null;

            if (!isNextWorldAlreadyInitialized) {
                GameEventRegistry.Reset();
                World.LoadContent();
            }

            Audio.StopAllSfx();
            World.Enter();
        }

        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            if (lastWorld != null) {
                lastWorld.Draw();
                lastWorld = null;
            } else {
                World.Draw();
            }

            if (Console.IsDisplaying) { Console.Draw(); }
        }

        public static void Reset() {
            SetWorld(Instance.InitialWorld);
        }

        public static void ExitApp() {
            Instance.ExitGame();
        }

        public void ExitGame() {
            Exit();
        }

        protected override void OnExiting(object sender, EventArgs args) {
            base.OnExiting(sender, args);
        }
    }
}
