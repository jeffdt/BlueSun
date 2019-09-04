using Microsoft.Xna.Framework;
using Pigeon.Collision;

namespace PigeonEngine.collision {
	public delegate void CollisionHandler(ColliderComponent thisHitbox, ColliderComponent otherHitbox, Point penetration);
}