using Microsoft.Xna.Framework;
using pigeon.legacy.graphics;

namespace pigeon.legacy.entities {
	public class ImageEntity : Entity {
		public ImageEntity(string image) {
			Graphic = Image.Create(image);
		}

		public ImageEntity(string image, Color color) {
			Graphic = Image.Create(image, color);
		}
	}
}
