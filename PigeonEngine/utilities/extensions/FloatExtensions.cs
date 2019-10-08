using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigeonEngine.utilities.extensions {
    public static class FloatExtensions {
        public static float Clamp(this float value, float min, float max) {
            if (value < min) {
                return min;
            }

            return value > max ? max : value;
        }
    }
}
