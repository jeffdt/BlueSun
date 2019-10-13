﻿using BlueSun.src.parameters;
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

namespace BlueSun.worlds.collision {
    class CollisionWorld : World {
        private const int tileSize = 16;
        private const int projectileSize = 2;
        private const int projectileCount = 8;
        private const float projectileSpeed = 300f;

        protected override void Load() {
            ColliderStrategy = new DumbSatColliderStrategy();

            BackgroundColor = Color.GhostWhite;

            for (int i = 0; i < projectileCount; i++) {
                CollisionRectTester rectTester = new CollisionRectTester() { NormalColor = Color.Black, IsProjectile = true, TileSize = tileSize, ProjectileSpeed = projectileSpeed };

                SimpleBoxCollider colliderCmpt = new SimpleBoxCollider() { Passive = false, Hitbox = new Rectangle(0, 0, projectileSize, projectileSize), CollisionHandler = rectTester.OnCollision };

                AddObj(
                    new GameObject("Projectile " + i) { Layer = 1f }
                    .AddComponent(new RectRenderer() {
                        Rect = new Rectangle(0, 0, projectileSize, projectileSize),
                        DrawStyle = ShapeDrawStyles.Filled,
                        FillColor = Color.White,
                    })
                    .AddComponent(rectTester)
                    .AddComponent(colliderCmpt)
                );
            }

            for (int row = 0; row < Display.ScreenHeight / tileSize; row++) {
                for (int col = 0; col < Display.ScreenWidth / tileSize; col++) {
                    if (row == 0 || col == 0 || row == (Display.ScreenHeight / tileSize) - 1 || col == (Display.ScreenWidth / tileSize) - 1) {
                        CollisionRectTester rectTester = new CollisionRectTester();

                        Color fillColor = Color.White;
                        Color borderColor = Color.Black;
                        RectRenderer wallRenderer = new RectRenderer() {
                            Rect = new Rectangle(0, 0, tileSize, tileSize),
                            DrawStyle = ShapeDrawStyles.FilledBordered,
                            FillColor = fillColor,
                            BorderColor = borderColor,
                            BorderThickness = 1
                        };
                        SimpleBoxCollider wallCollider = new SimpleBoxCollider() { Passive = true, Hitbox = new Rectangle(0, 0, tileSize, tileSize), CollisionHandler = rectTester.OnCollision };

                        if (row == 0 || row == (Display.ScreenHeight / tileSize) - 1) { // top or bottom row
                            wallCollider.IgnoredSides[1] = true; // right
                            wallCollider.IgnoredSides[3] = true; // left
                        }

                        if (col == 0 || col == (Display.ScreenWidth / tileSize) - 1) { // left or right col
                            wallCollider.IgnoredSides[0] = true; // top
                            wallCollider.IgnoredSides[2] = true; // bot
                        }

                        GameObject wallObj = new GameObject("Wall " + (row * col + col)) { LocalPosition = new Point(col * tileSize, row* tileSize), Layer = 0f };
                        wallObj.AddComponent(wallRenderer);
                        wallObj.AddComponent(wallCollider);
                        wallObj.AddComponent(rectTester);
                        AddObj(wallObj);
                    }
                }
            }
        }

        protected override void Unload() { }
    }

    class CollisionRectTester : Component {
        public Color NormalColor = Color.White;
        public float ProjectileSpeed;

        public bool IsProjectile = false;
        public int TileSize;

        private RectRenderer rectRenderer;

        protected override void Initialize() {
            rectRenderer = GetComponent<RectRenderer>();

            if (IsProjectile) {
                randomizePositionAndVelocity();
            }
        }

        private void randomizePositionAndVelocity() {
            int startX = Rand.Int(TileSize * 2, Display.ScreenWidth - (TileSize * 2));
            int startY = Rand.Int(TileSize * 2, Display.ScreenHeight - (TileSize * 2));
            Object.LocalPosition = new Point(startX, startY);
            Object.Velocity = new Vector2(Rand.SignFloat(), Rand.SignFloat()).Scale(ProjectileSpeed);
        }

        protected override void Update() {
            rectRenderer.Image.Color = NormalColor;

            if (IsProjectile && RawKeyboardInput.IsPressed(Keys.Space)) {
                randomizePositionAndVelocity();
            }
        }

        internal void OnCollision(ColliderComponent thisHitbox, ColliderComponent otherHitbox, Point penetration) {
            if (!thisHitbox.LastFrameCollisions.Contains(otherHitbox)) {
                rectRenderer.Image.Color = Color.LawnGreen;
                
                if (IsProjectile) {
                    // Pigeon.Console.Log(string.Format("penetration: {0}", penetration.ToVector2().ToString()));
                    // Sfx.PlaySfx("sfx6");

                    if (penetration.X != 0) {
                        Object.Velocity = Object.Velocity.MultiplyX(-1);
                        Object.LocalPosition = Object.LocalPosition.AddX(penetration.X);
                        Object.UpdateSpeculativePosition();
                    }

                    if (penetration.Y != 0) {
                        Object.Velocity = Object.Velocity.MultiplyY(-1);
                        Object.LocalPosition = Object.LocalPosition.AddY(penetration.Y);
                        Object.UpdateSpeculativePosition();
                    }
                }
            } else {
                rectRenderer.Image.Color = Color.OrangeRed;
            }
        }
    }
}
