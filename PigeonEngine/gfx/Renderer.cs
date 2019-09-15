﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pigeon.data;
using pigeon.utilities;
using PigeonEngine.utilities.extensions;

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

        private bool isBorderless;
        public bool IsBorderless {
            get { return isBorderless; }
            set {
                isBorderless = value;
                GraphicsDeviceMgr.HardwareModeSwitch = !isBorderless;
                IsFullScreen = isBorderless;
            }
        }

        private bool isFullScreen;
        public bool IsFullScreen {
            get { return isFullScreen; }
            set {
                isFullScreen = value;

                if (isFullScreen) {
                    int scaleWidth = monitorWidth / BaseResolutionX;
                    int scaleHeight = monitorHeight / BaseResolutionY;
                    
                    DrawScale = Math.Min(scaleWidth, scaleHeight);
                } else {
                    DrawScale = Pigeon.Instance.DisplayParams.InitialScale;
                }
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

        private static void setRenderTarget(RenderTarget2D renderTarget2D, Color clearColor) {
            setRenderTarget(renderTarget2D);
            GraphicsDeviceMgr.GraphicsDevice.Clear(clearColor);
        }

        private static void setRenderTarget(RenderTarget2D renderTarget2D) {
            GraphicsDeviceMgr.GraphicsDevice.SetRenderTarget(renderTarget2D);
        }

        public void CustomRender(RenderFunction renderFunction, Color clearColor) {
            if (deviceNeedsRefresh) {
                applyDeviceChanges();
            }

            // draw true image
            SpriteBatch.BeginPixelPerfect(trueScaleMatrix, SpriteSortMode.FrontToBack);
            setRenderTarget(TrueGameScreen, clearColor);
            renderFunction();
            SpriteBatch.End();

            // apply the shader
            setRenderTarget(ShadedGameScreen, clearColor);
            // TODO: try SpriteSortMode.Deferred instead of Immediate and see if it makes any difference
            SpriteBatch.BeginPixelPerfect(trueScaleMatrix);
            PostProcessors?.Invoke();
            SpriteBatch.Draw(TrueGameScreen, Vector2.Zero, Color.White);
            SpriteBatch.End();

            // if requested, save shaded screenshot (before scaling)
            if (takeScreenshot) {
                takeScreenshot = false;
                ShadedGameScreen.SaveAsTimestampedPng();
            }
        }

        internal void RenderOverlay(RenderFunction renderFunction, RenderTarget2D overlayRenderTarget, Vector2 drawPosition) {
            //setRenderTarget(TrueGameScreen);
            SpriteBatch.BeginPixelPerfect(trueScaleMatrix, SpriteSortMode.FrontToBack);
            renderFunction();
            SpriteBatch.End();

            //setRenderTarget(TrueGameScreen);
            //SpriteBatch.BeginPixelPerfect(trueScaleMatrix, SpriteSortMode.Immediate);
            //SpriteBatch.Draw(overlayRenderTarget, drawPosition, Color.White);
            //SpriteBatch.End();
        }

        public void FinalDraw() {
            if (LcdDisplay) {
                SpriteBatch.BeginPixelPerfect(trueScaleMatrix);
                SpriteBatch.Draw(lcdGridTex, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 1f);
                SpriteBatch.End();
            }

            // scale up
            if (IsFullScreen) {
                setRenderTarget(PaddedGameScreen, Color.Black);
                SpriteBatch.BeginPixelPerfect(DrawScaleMatrix);
                SpriteBatch.Draw(ShadedGameScreen, fullscreenInset, Color.White);
                SpriteBatch.End();

                setRenderTarget(null, Color.Black);
                SpriteBatch.BeginPixelPerfect(trueScaleMatrix);
                SpriteBatch.Draw(PaddedGameScreen, Vector2.Zero, Color.White);
                SpriteBatch.End();
            } else {
                setRenderTarget(null, Color.Black);
                SpriteBatch.BeginPixelPerfect(DrawScaleMatrix);
                SpriteBatch.Draw(ShadedGameScreen, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                SpriteBatch.End();
            }
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


        private void updateGraphicsDeviceSettings() {
            GraphicsDeviceMgr.IsFullScreen = IsFullScreen;

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
