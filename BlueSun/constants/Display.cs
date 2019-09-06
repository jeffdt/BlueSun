﻿using Microsoft.Xna.Framework;


namespace BlueSun.src.parameters {
    static class Display {
        public const int InitialScale = 4;
        public const int FrameRate = 60;
        public const int ScreenWidth = 320;
        public const int ScreenHeight = 180;

        public static readonly Point ScreenCenter = new Point(ScreenWidth / 2, ScreenHeight / 2);
        public static readonly Point ScreenBoundary = new Point(ScreenWidth, ScreenHeight);
        public static readonly Rectangle Screen = new Rectangle(0, 0, ScreenBoundary.X, ScreenBoundary.Y);
    }
}