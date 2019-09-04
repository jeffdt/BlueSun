using System;
using Microsoft.Xna.Framework;

namespace pigeon.legacy.graphics.text {
	public struct ColoredString {
		public readonly String str;
		public readonly Color color;

		public ColoredString(string str, Color color) {
			this.str = str;
			this.color = color;
		}
	}
}
