using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using pigeon.legacy.graphics.text;

namespace pigeon.legacy.entities {
	public class TextEntity : Entity {
		public delegate string TimeBasedUpdater();
		public delegate string EventBasedUpdater(object sender, EventArgs evt);

		private TimeBasedUpdater timeUpdater;
		private EventBasedUpdater eventUpdater;

		private readonly TextGraphic textGraphic;

        public String Text {
            get { return textGraphic.Text; }
            set { textGraphic.Text = value; }
        }

        public Color Color {
            get { return textGraphic.Color; }
            set { textGraphic.Color = value; }
        }

		public Point Size {
			get { return textGraphic.Size; }
    	}

		public SpriteFont Font {
			get { return textGraphic.Font; }
		}

		private TextEntity(Vector2 position, TextGraphic graphic, float layer)
			: base(position) {
        	Layer = layer;
			textGraphic = graphic;
			Graphic = graphic;
        }

        public static TextEntity RegisterStatic(EntityRegistry registry, string text, Vector2 position, SpriteFont font, float layer, Color tint, Justification justification = Justification.Center) {
            TextEntity entity = new TextEntity(position, new TextGraphic(text, font, tint, justification), layer);
            entity.textGraphic.Color = tint;
            registry.Register(entity);
            return entity;
        }

		public static TextEntity CreateStatic(Vector2 position, TextGraphic textGraphic, float layer) {
			return new TextEntity(position, textGraphic, layer);
		}

		public static TextEntity RegisterTimeUpdated(EntityRegistry registry, TimeBasedUpdater timeUpdater, string text, Vector2 position, SpriteFont font, float layer, Color tint, Justification justification = Justification.Center) {
			TextEntity entity = new TextEntity(position, new TextGraphic(text, font, tint, justification), layer) {
				timeUpdater = timeUpdater
			};
			entity.textGraphic.Color = tint;
            registry.Register(entity);
            return entity;
        }

		public static TextEntity RegisterEventUpdated<T>(EntityRegistry registry, string text, Vector2 position, SpriteFont font, float layer, Color tint, Justification justification, params EventBasedUpdater[] updaters) where T: EventArgs {
			TextEntity entity = new TextEntity(position, new TextGraphic(text, font, tint, justification), layer);

			foreach(EventBasedUpdater updater in updaters) {
				entity.eventUpdater += updater;
			}
			
			entity.textGraphic.Color = tint;
			Pigeon.GameEventRegistry.RegisterEventHandler<T>(entity.onEvent);
			registry.Register(entity);
			return entity;
		}

        public override void Update()
        {
            base.Update();

			if (timeUpdater != null) {
				Text = timeUpdater.Invoke();
			}
		}

		private void onEvent(object sender, EventArgs args) {
			eventUpdater(sender, args);
		}
    }
}
