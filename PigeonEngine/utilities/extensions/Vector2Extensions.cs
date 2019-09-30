using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigeonEngine.utilities.extensions {
    public static class Vector2Extensions {
        public static Vector2 Multiply(this Vector2 vector2, float x, float y) {
            return new Vector2(vector2.X * x, vector2.Y * y);
        }

        public static Vector2 MultiplyX(this Vector2 vector2, float x) {
            return new Vector2(vector2.X * x, vector2.Y);
        }

        public static Vector2 MultiplyY(this Vector2 vector2, float y) {
            return new Vector2(vector2.X, vector2.Y * y);
        }
    }
}
