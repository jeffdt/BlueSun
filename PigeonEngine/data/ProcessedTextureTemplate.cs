using System;
using Microsoft.Xna.Framework.Graphics;

namespace pigeon.data {
	public delegate ProcessedTextureTemplate[] TextureTemplateProcessor(string texturePath);

	public class ProcessedTextureTemplate {
		public String Alias;
		public Texture2D Texture;
	}
}