using System.Collections.Generic;
using System.Linq;

namespace pigeon.legacy.entities {
    public class EntityRegistry {
        private readonly List<Entity> toAdd = new List<Entity>();
        private readonly List<Entity> toRemove = new List<Entity>();

        private readonly List<Entity> activeEntities = new List<Entity>();

        public void Register(Entity entity) {
            toAdd.Add(entity);
        }

        public void RegisterAll(params Entity[] entities) {
            foreach (var entity in entities) {
                Register(entity);
            }
        }

        public void AddInstant(Entity entity) {
            addEntityToActive(entity);
        }

        public void Clear() {
            foreach (var entity in toAdd) {
                entity.UnloadContent();
            }

            foreach (var entity in activeEntities) {
                entity.UnloadContent();
            }

            foreach (var entity in toRemove) {
                entity.UnloadContent();
            }

            toAdd.Clear();
            toRemove.Clear();
            activeEntities.Clear();
        }

        public List<Entity> GetEntities() {
            return activeEntities;
        }

        public Entity GetNamedEntity(string name) {
            foreach (var entity in activeEntities) {
                if (entity.Name == name) {
                    return entity;
                }
            }

            foreach (var entity in toAdd) {
                if (entity.Name == name) {
                    return entity;
                }
            }

            return null;
        }

        public void Update() {
            addNew();
            removeOld();

            updateCurrent();
        }

        public void Draw() {
            for (int index = 0; index < activeEntities.Count; index++) {
                Entity entity = activeEntities[index];
                entity.Draw();
            }
        }

        private void updateCurrent() {
            for (int index = 0; index < activeEntities.Count; index++) {
                var entity = activeEntities[index];

                if (!entity.MarkedForRemoval) {
                    entity.UpdateComponents();
                    entity.Update();
                }

                if (entity.MarkedForRemoval) {
                    toRemove.Add(entity);
                    entity.MarkedForRemoval = false;
                }
            }
        }

        private void addNew() {
            if (toAdd.Count <= 0)
                return;

            foreach (var entity in toAdd) {
                addEntityToActive(entity);
            }

            toAdd.Clear();
        }

        private void addEntityToActive(Entity entity) {
            activeEntities.Add(entity);
        }

        private void removeOld() {
            if (toRemove.Count <= 0)
                return;

            foreach (var entity in toRemove) {
                activeEntities.Remove(entity);

                if (entity.UnloadOnUnregister) {
                    entity.UnloadContent();
                }
            }

            toRemove.Clear();
        }

        public List<string> GetAllNames() {
            return activeEntities.Where(e => !string.IsNullOrEmpty(e.Name)).Select(e => e.Name).ToList();
        }
    }
}