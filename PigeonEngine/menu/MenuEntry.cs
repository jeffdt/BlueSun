using System;
using Microsoft.Xna.Framework;
using pigeon.legacy.entities;
using pigeon.legacy.graphics;

namespace pigeon.menu {
	public class MenuEntry : Entity {
		public object BaseItem;
		public MenuEntry Previous { get; set; }
		public MenuEntry Next { get; set; }
		public Action SelectionAction { get; set; }

		public MenuEntry(Vector2 position, Graphic graphic) : base(position, graphic) { }
	}
}