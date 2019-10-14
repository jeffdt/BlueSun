using BlueSun.src.parameters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using pigeon.collision;
using pigeon.core;
using pigeon.gameobject;
using pigeon.input;
using pigeon.rand;
using pigeon.utilities;
using pigeon.utilities.extensions;
using pigeon.gfx.drawable.shape;
using pigeon.utilities.helpers;
using PigeonEngine.utilities.helpers;
using pigeon.component;

namespace BlueSun.worlds.collision {
    public class ProjectileController : Component {
        public int MaxBounces = 0;
        public float ProjectileSpeed;
        public Vector2 Direction;

        private int bounceCount = 0;

        public static GameObject BuildObject(float projectileSpeed, int projectileSize, int maxBounces, Vector2 direction) {
            GameObject obj = new GameObject("projectile");
            ProjectileController projectileController = new ProjectileController() { ProjectileSpeed = projectileSpeed, Direction = direction, MaxBounces = maxBounces };
            obj.AddComponent(projectileController);
            obj.AddComponent(new SimpleBoxCollider() { Passive = false, Hitbox = new Rectangle(0, 0, projectileSize, projectileSize), CollisionHandler = projectileController.OnCollision, Tags = { "projectile", "firedByPlayer" } });
            obj.AddComponent(new RectRenderer() {
                Rect = new Rectangle(0, 0, projectileSize, projectileSize),
                DrawStyle = ShapeDrawStyles.Filled,
                FillColor = Color.White,
            });
            obj.AddComponent(new BoundaryExpirator() { Boundaries = Display.Screen });
            
            return obj;
        }

        protected override void Initialize() {
            Object.Velocity = Direction.Scale(ProjectileSpeed);
        }

        protected override void Update() {

        }


        internal void OnCollision(ColliderComponent thisHitbox, ColliderComponent otherHitbox, Point penetration) {
            if (otherHitbox.Tags.Contains("reflective")
                && !thisHitbox.LastFrameCollisions.Contains(otherHitbox) // TODO: can this be deleted?
                ) {
                bounceCount += 1;

                if (bounceCount >= MaxBounces) {
                    Object.Deleted = true;
                } else {
                    if (penetration.X != 0) {
                        Object.Velocity = Object.Velocity.MultiplyX(-1);
                        Object.LocalPosition = Object.LocalPosition.PlusX(penetration.X);
                        Object.UpdateSpeculativePosition();
                    }

                    if (penetration.Y != 0) {
                        Object.Velocity = Object.Velocity.MultiplyY(-1);
                        Object.LocalPosition = Object.LocalPosition.PlusY(penetration.Y);
                        Object.UpdateSpeculativePosition();
                    }
                }
            } else if (otherHitbox.Tags.Contains("enemy")) {
                Object.Deleted = true;
            }
        }
    }
}
