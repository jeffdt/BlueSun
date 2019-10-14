using Microsoft.Xna.Framework;
using pigeon;
using pigeon.collision;
using pigeon.gameobject;
using pigeon.gfx.drawable.shape;
using pigeon.utilities;

namespace BlueSun.worlds.collision {
    public class EnemyController : Component {
        public GameObject[,] GameGrid;

        private int tileSize;

        private Point _coords;
        private Point coords {
            get { return _coords; }
            set {
                _coords = value;

                if (Object != null) {
                    Object.LocalPosition = _coords.Mult(tileSize);
                }
            }
        }

        public static GameObject BuildObject(Point initialCoords, int tileSize) {
            EnemyController enemyController = new EnemyController();
            enemyController.tileSize = tileSize;
            enemyController.coords = initialCoords;

            GameObject enemyObj = new GameObject("enemy") { LocalPosition = enemyController.coords.Mult(tileSize), Layer = .2f, LayerSortingVarianceEnabled = false };
            enemyObj.AddComponent(enemyController);
            enemyObj.AddComponent(new RectRenderer() { Rect = new Rectangle(2, 2, tileSize - 4, tileSize - 4), DrawStyle = ShapeDrawStyles.FilledBordered, FillColor = Color.DarkMagenta });
            enemyObj.AddComponent(new SimpleBoxCollider() { Passive = true, Hitbox = new Rectangle(2, 2, tileSize - 4, tileSize - 4), CollisionHandler = enemyController.OnCollision, Tags = { "enemy" } });
            return enemyObj;
        }

        private void OnCollision(ColliderComponent thisHitbox, ColliderComponent otherHitbox, Point penetration) {
            if (otherHitbox.Tags.Contains("projectile") && otherHitbox.Tags.Contains("firedByPlayer")) {
                Object.Deleted = true;
                GameGrid[_coords.X, _coords.Y] = null;
            }
        }

        protected override void Initialize() {
            GameGrid = ((CollisionWorld) Pigeon.World).GameGrid;
        }

        protected override void Update() {
            
        }
    }
}
