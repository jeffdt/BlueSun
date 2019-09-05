﻿namespace pigeon.utilities.extensions {
    public static class DoubleExtensions {
        public static double Clamp(this double value, double min, double max) {
            if (value < min) {
                return min;
            }

            if (value > max) {
                return max;
            }

            return value;
        }
    }
}
