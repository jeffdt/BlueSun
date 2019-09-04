using System;
using Microsoft.Xna.Framework;
using pigeon.squab;
using pigeon.utilities.extensions;

namespace pigeon.gfx {
	public class RectRenderer : Component, Drawable {
		public enum DrawModes { Filled, Bordered, FilledBordered }

		// TODO: !!!!!THIS CLASS ISN'T FINISHED!!!!!
		public Rectangle Rect;	// x/y represent offsets from Object.Position, width/height represent width height.
		// so to use, place object at desired position, and x/y of Rect can be left 0 if no offset is needed

		public Color FillColor;
		public Color BorderColor;
		public int BorderThickness = 1;

		public DrawModes DrawMode = DrawModes.Filled;

		protected override void Initialize() { }

		protected override void Update() { }

		public void Draw() {
			switch(DrawMode) {
				case DrawModes.Filled:
					RectangleExtensions.DrawFilled(Object.WorldPosition.X + Rect.X, Object.WorldPosition.Y + Rect.Y, Rect.Width, Rect.Height, FillColor, Object.DrawLayer);
					break;
				case DrawModes.Bordered:
					RectangleExtensions.DrawBordered(Object.WorldPosition.X + Rect.X, Object.WorldPosition.Y + Rect.Y, Rect.Width, Rect.Height, BorderColor, BorderThickness, Object.DrawLayer);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
