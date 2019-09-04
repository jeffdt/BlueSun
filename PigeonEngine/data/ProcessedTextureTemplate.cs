using System;
using Microsoft.Xna.Framework.Graphics;

namespace Pigeon.Data {
	public delegate ProcessedTextureTemplate[] TextureTemplateProcessor(string texturePath);

	public class ProcessedTextureTemplate {
		public String Alias;
		public Texture2D Texture;
	}
}