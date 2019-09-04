using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Pigeon.Time;
using PigeonEngine.utilities;

namespace Pigeon.Collision {
	public class DumbSatCollider : ICollider {
		private const int ITERATIONS = 3;

		public bool Enabled { get; set; }

		public DumbSatCollider() {
			Enabled = true;
		}

		public void Collide(List<ColliderComponent> allBoxes) {
			if (!Enabled) {
				return;
			}

			for (int i = 0; i < allBoxes.Count; i++) {
				var box1 = allBoxes[i];
				if (!box1.Enabled || box1.IsPassive()) {
					continue;
				}

				var rect1 = box1.GetRectangle();

				for (int j = 0; j < allBoxes.Count; j++) {
					var box2 = allBoxes[j];
					if (i == j || !box2.Enabled || box1.FrameCollisions.Contains(box2) || (box1.IgnoredColliders != null && box1.IgnoredColliders.Contains(box2))) {
						continue;
					}

					bool collided;
					
					if (box1.GetShape() == HitboxShapes.Box && box2.GetShape() == HitboxShapes.Box) {
						var rect2 = box2.GetRectangle();

						int iterationCount = 1;
						if (box1.Object.Velocity.LengthSquared() + box2.Object.Velocity.LengthSquared() > 3000) {
							iterationCount = 2;
						}

						var box1WorldPosition = box1.Object.WorldPosition;
						var box2WorldPosition = box2.Object.WorldPosition;

						for (int iteration = 0; iteration < iterationCount; iteration++) {
							var deltaTime = (iteration + 1) * Time.Time.SecScaled / iterationCount;
							var box1SpeculativePosition = box1.Object.SpeculativeWorldPositionAt(deltaTime);
							var box2SpeculativePosition = box2.Object.SpeculativeWorldPositionAt(deltaTime);

							var specRectX1 = getSpeculativeRect(rect1, box1SpeculativePosition.X, box1WorldPosition.Y);
							var specRectX2 = getSpeculativeRect(rect2, box2SpeculativePosition.X, box2WorldPosition.Y);
							var penX = checkBox(specRectX1, specRectX2, box1, box2, true);

							var specRectY1 = getSpeculativeRect(rect1, box1WorldPosition.X, box1SpeculativePosition.Y);
							var specRectY2 = getSpeculativeRect(rect2, box2WorldPosition.X, box2SpeculativePosition.Y);
							var penY = checkBox(specRectY1, specRectY2, box1, box2, false);

							if (penX != 0 || penY != 0) {
								var penetration = new Point(penX, penY);
								box1.Collided(box2, penetration);
								box2.Collided(box1, penetration.Mult(-1));

								if (box1.Enabled == false || box2.Enabled == false) {
									break;
								}
							}
						}
					} else if (box1.GetShape() == HitboxShapes.Box && box2.GetShape() == HitboxShapes.Point) {
						collided = SatCollider.CollideBoxPoint(box1.GetRectangle(), box2.Object.WorldPosition);
						if (collided) {
							box1.Collided(box2, Point.Zero);
							box2.Collided(box1, Point.Zero);
						}
					} else if (box1.GetShape() == HitboxShapes.Point && box2.GetShape() == HitboxShapes.Box) {
						collided = SatCollider.CollideBoxPoint(box2.GetWorldRectangle(), box1.Object.WorldPosition);

						if (collided) {
							box1.Collided(box2, Point.Zero);
							box2.Collided(box1, Point.Zero);
						}
					} else {
						throw new NotImplementedException(String.Format("no collision detection algorithm for shapes {0} and {1}", box1.GetShape(), box2.GetShape()));
					}
				}
			}
		}

		private static Rectangle getSpeculativeRect(Rectangle hbRect, int xPosition, int yPosition) {
			return new Rectangle(hbRect.X + xPosition, hbRect.Y + yPosition, hbRect.Width, hbRect.Height);
		}

		private static int checkBox(Rectangle specRect1, Rectangle specRect2, ColliderComponent box1, ColliderComponent box2, bool isX) {
			bool collided = SatCollider.CollideBoxes(specRect1, specRect2);
			if (collided) {
				return isX ? SatCollider.GetMinTranslationX(specRect1, specRect2) : SatCollider.GetMinTranslationY(specRect1, specRect2);
			}

			return 0;
		}

		public void Draw() {
			
		}
	}
} 