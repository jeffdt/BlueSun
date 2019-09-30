using System;
using BlueSun.src.parameters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using pigeon;
using pigeon.collision;
using pigeon.component;
using pigeon.core;
using pigeon.gameobject;
using pigeon.gfx;
using pigeon.input;
using pigeon.rand;
using pigeon.sound;
using PigeonEngine.utilities.extensions;

namespace BlueSun.worlds.collision {
    class CollisionWorld : World {
        protected override void Load() {
            ColliderStrategy = new DumbSatColliderStrategy();

            BackgroundColor = Color.SteelBlue;

            CollisionRectTester rectTester = new CollisionRectTester();

            const int projectileSize = 4;
            const int tileSize = 16;

            int startX = Rand.Int(tileSize, Display.ScreenWidth - tileSize - 1);
            int startY = Rand.Int(tileSize, Display.ScreenHeight - tileSize - 1);

            AddObj(
                new GameObject("Projectile", 0f) { FlatLocalPosition = new Point(startX, startY), LocalLayer = 1f }
                .AddComponent(new RectRenderer() {
                    Rect = new Rectangle(0, 0, projectileSize, projectileSize),
                    DrawMode = RectRenderer.DrawModes.FilledBordered,
                    FillColor = Color.White,
                    BorderColor = Color.Black,
                    BorderThickness = 1,
                })
                .AddComponent(rectTester)
                .AddComponent(new SimpleBoxCollider() { Passive = false, Hitbox = new Rectangle(0, 0, projectileSize, projectileSize), CollisionHandler = rectTester.OnCollision })
            );

            for (int row = 0; row < Display.ScreenWidth / tileSize; row++) {
                for (int col = 0; col < Display.ScreenHeight / tileSize; col++) {
                    if (row == 0 || col == 0 || row == (Display.ScreenWidth / tileSize) - 1 || col == (Display.ScreenHeight / tileSize) - 1) {
                        rectTester = new CollisionRectTester();

                        Color fillColor = Color.White;
                        Color borderColor = Color.Black;
                        RectRenderer wallRenderer = new RectRenderer() {
                            Rect = new Rectangle(0, 0, tileSize, tileSize),
                            DrawMode = RectRenderer.DrawModes.FilledBordered,
                            FillColor = fillColor,
                            BorderColor = borderColor,
                            BorderThickness = 1
                        };
                        SimpleBoxCollider wallCollider = new SimpleBoxCollider() { Passive = true, Hitbox = new Rectangle(0, 0, tileSize, tileSize), CollisionHandler = rectTester.OnCollision };

                        GameObject wallObj = new GameObject("Wall", 0f) { FlatLocalPosition = new Point(row * tileSize, col * tileSize), LocalLayer = 0f };
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
        RectRenderer rectRenderer;

        protected override void Initialize() {
            rectRenderer = GetComponent<RectRenderer>();
        }

        protected override void Update() {
            if (rectRenderer.Image.Color != Color.White) {
                rectRenderer.Image.Color = Color.White;
            }
        }

        internal void OnCollision(ColliderComponent thisHitbox, ColliderComponent otherHitbox, Point penetration) {
            if (!thisHitbox.LastFrameCollisions.Contains(otherHitbox)) {
                Pigeon.Console.Log(string.Format("penetration: {0}", penetration.ToVector2().ToString()));
                Sfx.PlaySfx("sfx6");
                rectRenderer.Image.Color = Color.HotPink;
                
                if (penetration.X != 0) {
                    Object.Velocity = Object.Velocity.MultiplyX(-1);
                }

                if (penetration.Y != 0) {
                    Object.Velocity = Object.Velocity.MultiplyY(-1);
                }

            } else {
                rectRenderer.Image.Color = Color.DarkRed;
            }
        }
    }
}
