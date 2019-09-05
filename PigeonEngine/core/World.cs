using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using pigeon.collision;
using pigeon.data;
using pigeon.legacy.entities;
using pigeon.core.tasks;
using pigeon.gameobject;
using pigeon.particle;

namespace pigeon.core {
    public abstract class World {
        protected bool AddDebugger = true;

        public GameObject ObjRoot = new GameObject { Name = "Root", DisableSortVariance = true, DisableLayerInheritance = true };
        internal List<ColliderComponent> Hitboxes = new List<ColliderComponent>();

        public readonly EntityRegistry EntityRegistry = new EntityRegistry();
        public readonly ObjectRegistry<Particle> ParticleRegistry = new ObjectRegistry<Particle>();
        private readonly TaskRegistry taskRegistry = new TaskRegistry();
        public Color BackgroundColor = Pigeon.EngineBkgdColor;

        public ICollider Collider;
        public static bool DrawColliderDebugInfo;

        public void AddObj(GameObject obj) {
            ObjRoot.AddChild(obj);
        }

        public void AddNestedObj(string path, GameObject obj) {
            FindObj(path).AddChild(obj);
        }

        public GameObject FindObj(string name) {
            return ObjRoot.FindChildRecursive(name);
        }

        public void AddEmptyObj(params string[] names) {
            foreach (var name in names) {
                ObjRoot.AddChild(new GameObject(name));
            }
        }

        public bool DeleteObjSafe(string name) {
            GameObject obj = ObjRoot.FindChildRecursive(name);
            if (obj != null) {
                obj.Deleted = true;
                return true;
            }

            return false;
        }

        public void LoadContent() {
            Particle.Initialize();
            Load();
        }

        protected abstract void Load();
        protected abstract void Unload();

        public void UnloadContent() {
            EntityRegistry.Clear();
            ParticleRegistry.Clear();

            Unload();
        }

        // useful if you only want to do something any time you enter a world, but not in LoadContent
        public virtual void Enter() { }

        public List<ObjectSeed> LoadSeeds(string seedLocation) {
            string path = Path.Combine("data", "seeds", seedLocation);
            return GameData.Deserialize<List<ObjectSeed>>(path);
        }

        public void LoadAndAddSeeds(string seedLocation) {
            var objectSeeds = LoadSeeds(seedLocation);

            foreach (var seed in objectSeeds) {
                GameObject obj = seed.Build();

                if (seed.ParentName == null) {
                    Pigeon.World.AddObj(obj);
                } else {
                    Pigeon.World.FindObj(seed.ParentName).AddChild(obj);
                }
            }
        }

        public virtual void Update() {
            taskRegistry.Update();
            EntityRegistry.Update();
            ObjRoot.Update();
            ParticleRegistry.Update();

            if (Collider != null) {
                Collider.Collide(Hitboxes);
            }

            ObjRoot.FinalUpdate();
        }

        public virtual void Draw() {
            Pigeon.Renderer.CustomRender(renderAll, BackgroundColor);
        }

        private void renderAll() {
            ObjRoot.Draw();
            EntityRegistry.Draw();

            foreach (Particle particle in ParticleRegistry.Objects) {
                particle.Draw();
            }

            if (Collider != null && DrawColliderDebugInfo) {
                Collider.Draw();
            }
        }

        public void AddTask(float time, Action action) {
            taskRegistry.Add(action, time);
        }
    }
}