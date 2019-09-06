using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using pigeon.legacy.graphics;

namespace pigeon.legacy.entities.primitives {
    public class Line : Entity {
        private readonly Vector2[] points;
        private readonly Image[] pixels;

        private Color tint;

        public Color Tint {
            get {
                return tint;
            }
            set {
                tint = value;
            }
        }

        public Line(Vector2 start, Vector2 end, Color color, float layer = 0f) {
            Position = start;
            Layer = layer;

            points = FindLine((int) start.X, (int) start.Y, (int) end.X, (int) end.Y);

            pixels = new Image[points.Length];
            for (int i = 0; i < points.Length; i++) {
                Image pixel = Image.Create("pixel");
                pixel.Color = color;
                pixels[i] = pixel;
            }
        }

        public override void Draw() {
            for (int i = 0; i < points.Length; i++) {
                pixels[i].Draw(points[i], Layer);
            }
        }

        private Vector2[] FindLine(int x, int y, int x2, int y2) {
            List<Vector2> coords = new List<Vector2>();

            int dx1 = 0;
            int dy1 = 0;
            int dx2 = 0;
            int dy2 = 0;

            int w = x2 - x;
            int h = y2 - y;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);

            if (w < 0)
                dx1 = -1;
            else if (w > 0)
                dx1 = 1;

            if (h < 0)
                dy1 = -1;
            else if (h > 0)
                dy1 = 1;

            if (w < 0)
                dx2 = -1;
            else if (w > 0)
                dx2 = 1;

            if (!(longest > shortest)) {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0)
                    dy2 = -1;
                else if (h > 0)
                    dy2 = 1;
                dx2 = 0;
            }

            int numerator = longest >> 1;

            for (int i = 0; i <= longest; i++) {
                coords.Add(new Vector2(x, y));
                numerator += shortest;
                if (!(numerator < longest)) {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                } else {
                    x += dx2;
                    y += dy2;
                }
            }

            return coords.ToArray();
        }
    }
}
