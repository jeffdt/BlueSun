using Microsoft.Xna.Framework;
using Pigeon.legacy.entities;
using Pigeon.legacy.graphics;

namespace Pigeon.menu {
	public class DynamicPanel : Entity {
		private readonly Image topLeft;
		private readonly Image topEdge;
		private readonly Image topRight;

		private readonly Image left;
		private readonly Image center;
		private readonly Image right;

		private readonly Image bottomLeft;
		private readonly Image bottomEdge;
		private readonly Image bottomRight;

		// position refers to upper left corner of CENTER AREA
		public DynamicPanel(string spriteSheet, Point cornerSize, Point centerSize, float layer) {
			Layer = layer;

			topLeft = Image.Create(spriteSheet, new Rectangle(0, 0, cornerSize.X, cornerSize.Y), Point.Zero);
			topLeft.Offset = new Point(cornerSize.X, cornerSize.Y);
			topEdge = Image.Create(spriteSheet, new Rectangle(cornerSize.X, 0, 1, cornerSize.Y));
			topEdge.Scale = new Vector2(centerSize.X, 1);
			topEdge.Offset = new Point(0, cornerSize.Y);
			topRight = Image.Create(spriteSheet, new Rectangle(cornerSize.X + 1, 0, cornerSize.X, cornerSize.Y));
			topRight.Offset = new Point(-centerSize.X, cornerSize.Y);

			left = Image.Create(spriteSheet, new Rectangle(0, cornerSize.Y, cornerSize.X, 1), Point.Zero);
			left.Scale = new Vector2(1, centerSize.Y);
			left.Offset = new Point(cornerSize.X, 0);
			center = Image.Create(spriteSheet, new Rectangle(cornerSize.X, cornerSize.Y, 1, 1));
			center.Scale = new Vector2(centerSize.X, centerSize.Y);
			center.Offset = Point.Zero;
			right = Image.Create(spriteSheet, new Rectangle(cornerSize.X + 1, cornerSize.Y, cornerSize.X, 1));
			right.Scale = new Vector2(1, centerSize.Y);
			right.Offset = new Point(-centerSize.X, 0);

			bottomLeft = Image.Create(spriteSheet, new Rectangle(0, cornerSize.Y + 1, cornerSize.X, cornerSize.Y), Point.Zero);
			bottomLeft.Offset = new Point(cornerSize.X, -centerSize.Y);
			bottomEdge = Image.Create(spriteSheet, new Rectangle(cornerSize.X, cornerSize.Y + 1, 1, cornerSize.Y));
			bottomEdge.Scale = new Vector2(centerSize.X, 1);
			bottomEdge.Offset = new Point(0, -centerSize.Y);
			bottomRight = Image.Create(spriteSheet, new Rectangle(cornerSize.X + 1, cornerSize.Y + 1, cornerSize.X, cornerSize.Y));
			bottomRight.Offset = new Point(-centerSize.X, -centerSize.Y);
		}
		
		public override void Draw() {
			base.Draw();

			topLeft.Draw(Position, Layer);
			topEdge.Draw(Position, Layer);
			topRight.Draw(Position, Layer);

			left.Draw(Position, Layer);
			center.Draw(Position, Layer);
			right.Draw(Position, Layer);

			bottomLeft.Draw(Position, Layer);
			bottomEdge.Draw(Position, Layer);
			bottomRight.Draw(Position, Layer);
		}

		public Vector2 Size {
			get {
				return new Vector2(topLeft.Size.X + topEdge.Size.X * topEdge.Scale.X + topRight.Size.X, topLeft.Size.Y + left.Size.Y * left.Scale.Y + bottomLeft.Size.Y);
			}
		}
	}
}
