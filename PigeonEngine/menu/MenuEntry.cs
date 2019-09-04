using System;
using Microsoft.Xna.Framework;
using Pigeon.legacy.entities;
using Pigeon.legacy.graphics;

namespace Pigeon.menu {
	public class MenuEntry : Entity {
		public object BaseItem;
		public MenuEntry Previous { get; set; }
		public MenuEntry Next { get; set; }
		public Action SelectionAction { get; set; }

		public MenuEntry(Vector2 position, Graphic graphic) : base(position, graphic) { }
	}
}