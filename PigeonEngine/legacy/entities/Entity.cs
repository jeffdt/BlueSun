using System.Collections.Generic;
using Microsoft.Xna.Framework;
using pigeon.legacy.graphics;
using pigeon.debug;
using pigeon.gameobject;

namespace pigeon.legacy.entities {
    public class Entity {
        public bool UnloadOnUnregister { get; protected set; }

        public string Id;
        public string Name;
        public Vector2 Position;
        public Graphic Graphic;
        public float Layer = 0;

        public bool Visible = true;

        #region components
        private List<Component> components;

        public void AddComponent(Component component) {
            (components ?? (components = new List<Component>())).Add(component);
            // component.Parent = this;
        }

        public T GetComponent<T>() where T : Component {
            for (int index = 0; index < components.Count; index++) {
                var component = components[index];
                if (component is T) {
                    return component as T;
                }
            }

            return null;
        }
        #endregion

        public bool MarkedForRemoval = false;

        public void Reinitialize() {
            MarkedForRemoval = false;
        }

        public Entity() {
            Position = Vector2.Zero;
        }

        public Entity(Vector2 position) {
            Position = position;
        }

        public Entity(Vector2 position, Graphic graphic) {
            Graphic = graphic;
            Position = position;
        }

        public virtual void UnloadContent() { }

        public void UpdateComponents() {
            // not including in Update because then it could be overridden
            if (components == null) {
                return;
            }

            foreach (var component in components) {
                if (component.Enabled) {
                    component.UpdateComponent();
                }
            }
        }

        public virtual void Update() {
            Graphic?.Update();
        }

        public virtual void Draw() {
            if (Graphic != null && Visible) {
                Graphic.Draw(Position, Layer);
            }
        }

        protected virtual void DrawSecondaryHitboxes() { }

        public override string ToString() {
            var inspector = new ObjectInspector();
            inspector.AppendField("Name", Name);
            inspector.AppendField("Position", Position);

            return inspector.ToString();
        }
    }
}