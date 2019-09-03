using Microsoft.Xna.Framework;
using pigeon.collision;

namespace PigeonEngine.collision {
	public delegate void CollisionHandler(ColliderComponent thisHitbox, ColliderComponent otherHitbox, Point penetration);
}