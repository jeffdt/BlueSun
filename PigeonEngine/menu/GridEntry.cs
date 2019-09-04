using System;
using Microsoft.Xna.Framework;
using Pigeon.legacy.entities;
using Pigeon.legacy.graphics;

namespace Pigeon.menu {
    public class GridEntry : Entity {	
        public object BaseItem;

        public GridEntry Left { get; set; }
        public GridEntry Right { get; set; }
        public GridEntry Up { get; set; }
        public GridEntry Down { get; set; }

		public Action SelectionAction { private get; set; }

		public GridEntry(Vector2 position, Graphic graphic) : base(position, graphic) { }

        public void Select() {
            if (SelectionAction != null) {
                SelectionAction.Invoke();
            }
        }
    }
}
