using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pigeon.gfx.drawable.text;
using pigeon.legacy.graphics.text;

namespace pigeon.legacy.entities {
    public sealed class TextEntity : Entity {
        private readonly TextGraphic textGraphic;

        public string Text {
            get { return textGraphic.Text; }
            set { textGraphic.Text = value; }
        }

        private TextEntity(Vector2 position, TextGraphic graphic, float layer)
            : base(position) {
            Layer = layer;
            textGraphic = graphic;
            Graphic = graphic;
        }

        public static TextEntity RegisterStatic(EntityRegistry registry, string text, Vector2 position, SpriteFont font, float layer, Color tint, Justifications justification = Justifications.Center) {
            TextEntity entity = new TextEntity(position, new TextGraphic(text, font, tint, justification), layer);
            entity.textGraphic.Color = tint;
            registry.Register(entity);
            return entity;
        }

        public static TextEntity CreateStatic(Vector2 position, TextGraphic textGraphic, float layer) {
            return new TextEntity(position, textGraphic, layer);
        }
    }
}
