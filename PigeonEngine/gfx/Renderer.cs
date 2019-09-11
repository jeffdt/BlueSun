using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pigeon.data;
using pigeon.utilities;

namespace pigeon.gfx {
    public class Renderer {
        public delegate void RenderFunction();
        public class RenderScaleChangedEvent : EventArgs { }

        public Action PostProcessors;

        private readonly Matrix trueScaleMatrix = Matrix.CreateScale(1, 1, 1);

        private static int monitorWidth { get { return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; } }
        private static int monitorHeight { get { return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; } }

        private bool deviceNeedsRefresh;
        private int drawScale = 1;

        public int DrawScale {
            get { return drawScale; }
            set {
                try {
                    drawScale = value;
                    DrawScaleMatrix = Matrix.CreateScale(drawScale, drawScale, 1);

                    updateGraphicsDeviceSettings();

                    Pigeon.GameEventRegistry.RaiseEmptyEvent<RenderScaleChangedEvent>();
                } catch (Exception) {
                    Pigeon.Console.LogError("Unable to set drawscale to " + value);
                }
            }
        }

        private bool isFullScreen;

        public bool IsFullScreen {
            get { return isFullScreen; }
            set {
                isFullScreen = value;
                GraphicsDeviceMgr.IsFullScreen = isFullScreen;
                updateGraphicsDeviceSettings();
            }
        }

        #region resolution
        public int BaseResolutionX { get; private set; }
        public int BaseResolutionY { get; private set; }
        public Point BaseScreenCenter { get { return new Point(BaseResolutionX, BaseResolutionY).Div(2); } }

        private int customResX = -1;
        private int customResY = -1;

        public void SetCustomResolution(int x, int y) {
            customResX = x;
            customResY = y;
            updateGraphicsDeviceSettings();
        }
        #endregion

        public Matrix DrawScaleMatrix;
        public RenderTarget2D TrueGameScreen;
        public RenderTarget2D ShadedGameScreen;
        public RenderTarget2D PaddedGameScreen;

        public static SpriteBatch SpriteBatch;
        public static GraphicsDeviceManager GraphicsDeviceMgr;

        public static bool LcdDisplay = false;
        private const int LcdStrength = 10;
        private Texture2D lcdGridTex;

        private bool takeScreenshot;

        private Vector2 fullscreenInset;

        public void Screenshot() {
            takeScreenshot = true;
        }

        public Renderer(int baseResolutionX, int baseResolutionY, int drawScale = 1) {
            DrawScale = drawScale;
            SetBaseResolution(baseResolutionX, baseResolutionY);
        }

        public void SetBaseResolution(int x, int y) {
            BaseResolutionX = x;
            BaseResolutionY = y;
            TrueGameScreen = new RenderTarget2D(GraphicsDeviceMgr.GraphicsDevice, x, y);
            ShadedGameScreen = new RenderTarget2D(GraphicsDeviceMgr.GraphicsDevice, x, y);
            PaddedGameScreen = new RenderTarget2D(GraphicsDeviceMgr.GraphicsDevice, monitorWidth, monitorHeight);
            updateGraphicsDeviceSettings();
        }

        public void CustomRender(RenderFunction renderFunction, Color clearColor) {
            // draw true image
            Begin(trueScaleMatrix);
            setRenderTarget(TrueGameScreen, clearColor);
            renderFunction();
            SpriteBatch.End();

            // apply the shader
            setRenderTarget(ShadedGameScreen, clearColor);
            SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, trueScaleMatrix);
            PostProcessors?.Invoke();
            SpriteBatch.Draw(TrueGameScreen, Vector2.Zero, Color.White);
            SpriteBatch.End();

            // if requested, save shaded screenshot (before scaling)
            if (takeScreenshot) {
                takeScreenshot = false;
                ShadedGameScreen.SaveAsTimestampedPng();
            }
        }

        public void FinalDraw() {
            // scale up
            if (IsFullScreen) {
                setRenderTarget(PaddedGameScreen, Color.Black);
                SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, DrawScaleMatrix);
                SpriteBatch.Draw(ShadedGameScreen, fullscreenInset, Color.White);
                if (LcdDisplay) {
                    SpriteBatch.Draw(lcdGridTex, Vector2.Zero, Color.White);
                }
                SpriteBatch.End();

                setRenderTarget(null, Color.Black);
                SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, trueScaleMatrix);
                SpriteBatch.Draw(PaddedGameScreen, Vector2.Zero, Color.White);
                SpriteBatch.End();
            } else {
                setRenderTarget(null, Color.Black);
                SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, DrawScaleMatrix);
                SpriteBatch.Draw(ShadedGameScreen, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                SpriteBatch.End();

                if (LcdDisplay) {
                    SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, trueScaleMatrix);
                    SpriteBatch.Draw(lcdGridTex, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 1f);
                    SpriteBatch.End();
                }
            }
        }

        protected void Begin(Matrix scaleMatrix) {
            if (deviceNeedsRefresh) {
                applyDeviceChanges();
            }

            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, scaleMatrix);
        }

        private void applyDeviceChanges() {
            var scaledResX = DrawScale * BaseResolutionX;
            var scaledResY = DrawScale * BaseResolutionY;

            lcdGridTex = new RenderTarget2D(GraphicsDeviceMgr.GraphicsDevice, scaledResX, scaledResY);
            Color[] lcdGridPixels = new Color[scaledResX * scaledResY];

            var emptyPixel = new Color(0, 0, 0, 0);
            var gridPixel = new Color(0, 0, 0, LcdStrength);

            for (int row = 0; row < scaledResY; row++) {
                for (int col = 0; col < scaledResX; col++) {
                    var rowPix = row % DrawScale;
                    var colPix = col % DrawScale;
                    if (rowPix == 0 || colPix == 0) {
                        lcdGridPixels[(row * scaledResX) + col] = gridPixel;
                    } else {
                        lcdGridPixels[(row * scaledResX) + col] = emptyPixel;
                    }
                }
            }

            lcdGridTex.SetData(lcdGridPixels);
            GraphicsDeviceMgr.ApplyChanges();
            deviceNeedsRefresh = false;
        }

        private static void setRenderTarget(RenderTarget2D renderTarget2D, Color clearColor) {
            GraphicsDeviceMgr.GraphicsDevice.SetRenderTarget(renderTarget2D);
            GraphicsDeviceMgr.GraphicsDevice.Clear(clearColor);
        }

        internal void RenderOverlay(RenderFunction renderFunction) {
            Begin(trueScaleMatrix);
            renderFunction();
            SpriteBatch.End();
        }

        private void updateGraphicsDeviceSettings() {
            int scaledGameWidth = BaseResolutionX * drawScale;
            int scaledGameHeight = BaseResolutionY * drawScale;

            int fullDisplayWidth;
            int fullDisplayHeight;

            if (IsFullScreen) {
                fullDisplayWidth = monitorWidth;
                fullDisplayHeight = monitorHeight;
                fullscreenInset = new Vector2((fullDisplayWidth - scaledGameWidth) / 2, (fullDisplayHeight - scaledGameHeight) / 2) / drawScale;
            } else if (customResX != -1 && customResY != -1) {
                fullDisplayWidth = customResX;
                fullDisplayHeight = customResY;

                customResX = -1;
                customResY = -1;
            } else {
                fullDisplayWidth = scaledGameWidth;
                fullDisplayHeight = scaledGameHeight;
            }

            GraphicsDeviceMgr.PreferredBackBufferWidth = fullDisplayWidth;
            GraphicsDeviceMgr.PreferredBackBufferHeight = fullDisplayHeight;

            deviceNeedsRefresh = true;
        }
    }
}
