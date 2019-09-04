using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace pigeon.collision {
	public interface ICollider {
		bool Enabled { get; set; }
		void Collide(List<ColliderComponent> allBoxes);
		void Draw();
	}
}