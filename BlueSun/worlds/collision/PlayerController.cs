using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using pigeon;
using pigeon.collision;
using pigeon.gameobject;
using pigeon.gfx.drawable.shape;
using pigeon.input;
using pigeon.sound;
using pigeon.utilities;
using pigeon.utilities.helpers;
using PigeonEngine.utilities.helpers;

namespace BlueSun.worlds.collision {
    internal class PlayerController : Component {
        private const float PROJ_SPEED = 500f;
        private const int PROJ_SIZE = 1;
        private const int PROJ_BOUNCES = 6;

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
            PlayerController playerController = new PlayerController();
            playerController.tileSize = tileSize;
            playerController.coords = initialCoords;

            GameObject playerObj = new GameObject("player") { LocalPosition = playerController.coords.Mult(tileSize), Layer = .2f, LayerSortingVarianceEnabled = false };
            playerObj.AddComponent(playerController);
            playerObj.AddComponent(new RectRenderer() { Rect = new Rectangle(2, 2, tileSize - 4, tileSize - 4), DrawStyle = ShapeDrawStyles.FilledBordered, FillColor = Color.DodgerBlue });
            playerObj.AddComponent(new SimpleBoxCollider() { Passive = true, Hitbox = new Rectangle(2, 2, tileSize - 4, tileSize - 4), CollisionHandler = playerController.OnCollision });
            return playerObj;
        }

        protected override void Initialize() {
            GameGrid = ((CollisionWorld) Pigeon.World).GameGrid;
        }

        protected override void Update() {
            AxisDirections? moveDirection = null;

            if (RawKeyboardInput.IsPressed(Keys.W) || RawGamepadInput.IsDpadPressed(AxisDirections.Up) || RawGamepadInput.IsLeftJoystickPressed(AxisDirections.Up)) {
                moveDirection = AxisDirections.Up;
            } else if (RawKeyboardInput.IsPressed(Keys.D) || RawGamepadInput.IsDpadPressed(AxisDirections.Right) || RawGamepadInput.IsLeftJoystickPressed(AxisDirections.Right)) {
                moveDirection = AxisDirections.Right;
            } else if (RawKeyboardInput.IsPressed(Keys.S) || RawGamepadInput.IsDpadPressed(AxisDirections.Down) || RawGamepadInput.IsLeftJoystickPressed(AxisDirections.Down)) {
                moveDirection = AxisDirections.Down;
            } else if (RawKeyboardInput.IsPressed(Keys.A) || RawGamepadInput.IsDpadPressed(AxisDirections.Left) || RawGamepadInput.IsLeftJoystickPressed(AxisDirections.Left)) {
                moveDirection = AxisDirections.Left;
            }

            if (moveDirection != null) {
                Point destination = coords + ((AxisDirections) moveDirection).ToPoint();

                if (checkCanMove(destination)) {
                    GameGrid[coords.X, coords.Y] = null;
                    GameGrid[destination.X, destination.Y] = Object;
                    coords = destination;
                }
            }


            Diagonals? diagonalDir = null;

            if (RawKeyboardInput.IsPressed(Keys.Q)) {
                diagonalDir = Diagonals.UpLeft;
            } else if (RawKeyboardInput.IsPressed(Keys.E)) {
                diagonalDir = Diagonals.UpRight;
            } else if (RawKeyboardInput.IsPressed(Keys.Z)) {
                diagonalDir = Diagonals.DownLeft;
            } else if (RawKeyboardInput.IsPressed(Keys.C)) {
                diagonalDir = Diagonals.DownRight;
            }

            if (diagonalDir != null) {
                Sfx.PlaySfx("sfx1");
                GameObject projectileObj = ProjectileController.BuildObject(PROJ_SPEED, PROJ_SIZE, PROJ_BOUNCES, ((Diagonals) diagonalDir).ToVector2());
                projectileObj.LocalPosition = Object.LocalPosition.Plus(tileSize / 2 - 1);
                Pigeon.World.AddObj(projectileObj);
            }
        }

        private bool checkCanMove(Point destination) {
            return GameGrid[destination.X, destination.Y] == null;
        }


        internal void OnCollision(ColliderComponent thisHitbox, ColliderComponent otherHitbox, Point penetration) {
            
        }
    }
}