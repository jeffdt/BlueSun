using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pigeon.Collision {
	public interface ICollider {
		bool Enabled { get; set; }
		void Collide(List<ColliderComponent> allBoxes);
		void Draw();
	}
}