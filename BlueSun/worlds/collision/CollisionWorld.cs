﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using pigeon.collision;
using pigeon.component;
using pigeon.core;
using pigeon.gameobject;
using pigeon.gfx;
using pigeon.input;
using pigeon.sound;

namespace BlueSun.worlds.collision {
    class CollisionWorld : World {
        protected override void Load() {
            ColliderStrategy = new DumbSatColliderStrategy();

            BackgroundColor = Color.SteelBlue;

            CollisionRectTester rectTester = new CollisionRectTester();

            AddObj(
                new GameObject("Rect 1", 0f) { FlatLocalPosition = new Point(40, 100) }
                .AddComponent(new RectRenderer() {
                    Rect = new Rectangle(0, 0, 30, 30),
                    DrawMode = RectRenderer.DrawModes.FilledBordered,
                    FillColor = Color.PapayaWhip,
                    BorderThickness = 1,
                    BorderColor = Color.Black,
                })
                .AddComponent(new SimpleController())
                .AddComponent(rectTester)
                .AddComponent(new SimpleBoxCollider() { Passive = false, Hitbox = new Rectangle(0, 0, 30, 30), CollisionHandler = rectTester.OnCollision })
            );

            AddObj(
                new GameObject("Rect 2", 0f) { FlatLocalPosition = new Point(80, 100) }
                .AddComponent(new RectRenderer() {
                    Rect = new Rectangle(0, 0, 30, 30),
                    DrawMode = RectRenderer.DrawModes.FilledBordered,
                    FillColor = Color.ForestGreen,
                    BorderThickness = 1,
                    BorderColor = Color.Black
                })
                .AddComponent(new SimpleBoxCollider() { Passive = false, Hitbox = new Rectangle(0, 0, 30, 30) })
            );
        }

        protected override void Unload() { }
    }

    class CollisionRectTester : Component {
        RectRenderer rectRenderer;

        protected override void Initialize() {
            rectRenderer = GetComponent<RectRenderer>();
        }

        protected override void Update() {
            if (RawKeyboardInput.IsHeld(Keys.LeftShift, Keys.RightShift)) {
                rectRenderer.FillColor = Color.DodgerBlue;
            } else {
                rectRenderer.FillColor = Color.PapayaWhip;
            }
        }

        internal void OnCollision(ColliderComponent thisHitbox, ColliderComponent otherHitbox, Point penetration) {
            if (!thisHitbox.LastFrameCollisions.Contains(otherHitbox)) {
                Sfx.PlaySfx("sfx2");
            }
        }
    }
}
