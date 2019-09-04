using Microsoft.Xna.Framework;

namespace Pigeon.utilities.extensions {
	public static class ColorExtensions {
		public static Vector3 ToVector3(this Color color) {
			return new Vector3((float) color.R / 255, (float) color.G / 255, (float) color.B / 255);
		}
	}
}
