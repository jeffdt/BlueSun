﻿using Microsoft.Xna.Framework;

namespace PigeonEngine.utilities.extensions {
	public static class IntExtensions {
		public static int Clamp(this int me, int max) {
			return MathHelper.Clamp(me, 0, max);
		}

		public static int Clamp(this int me, int min, int max) {
			return MathHelper.Clamp(me, min, max);
		}

		public static int ClampWrap(this int me, int min, int max) {
			if (me < min) {
				return max;
			}
			
			if (me > max) {
				return min;
			}

			return me;
		}
	}
}
