using Microsoft.Xna.Framework;
using Pigeon.legacy.graphics;

namespace Pigeon.legacy.entities {
	public class ImageEntity : Entity {
		public ImageEntity(string image) {
			Graphic = Image.Create(image);
		}

		public ImageEntity(string image, Color color) {
			Graphic = Image.Create(image, color);
		}
	}
}
