using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pigeon.legacy.entities;
using Pigeon.legacy.graphics.text;
using Pigeon.utilities.extensions;

namespace Pigeon.Console {
	public class MessageLog {
		private readonly int limit;
		private readonly SpriteFont font;
		private readonly Vector2 bottomMessagePosition;
		private readonly int wrapWidth;
		private readonly Dictionary<LogMessageTypes, Color> typeColors;
		private readonly EntityRegistry registry;

		private readonly List<LogMessage> allMessages;
		private readonly List<TextEntity> messageEntities;

		public MessageLog(ConsoleOptions options, EntityRegistry registry) {
			limit = options.LogHistoryLimit;
			font = options.Font;
			bottomMessagePosition = options.BottomMessagePosition;
			wrapWidth = options.LineWrapWidth;
			typeColors = new Dictionary<LogMessageTypes, Color> { { LogMessageTypes.Command, options.HistoryColor }, { LogMessageTypes.Info, options.InfoColor }, { LogMessageTypes.Error, options.ErrorColor } };
			this.registry = registry;

			allMessages = new List<LogMessage>();
			messageEntities = new List<TextEntity>();
		}

		public void AddMessage(LogMessage message) {
			var splitLines = message.Text.SplitWrap(font, wrapWidth);

			allMessages.Add(message);

			foreach (string line in splitLines) {
				logOne(line, message.Type);
			}

			Pigeon.EngineEventRegistry.RaiseEvent(this, new ConsoleLogChangedEvent { Message = message });
		}

		public List<LogMessage> GetAllMessages() {
			return allMessages;
		}

		private void logOne(string text, LogMessageTypes type) {
            if (messageEntities.Count == limit) {
                // TODO: for scrollable log history, this part will need to change
			    messageEntities[limit - 1].MarkedForRemoval = true;
				messageEntities.RemoveAt(limit - 1);
			}

			messageEntities.Insert(0, TextEntity.RegisterStatic(registry, text, bottomMessagePosition, font, 1f, typeColors[type], Justification.TopLeft));

			for (int index = 1; index < messageEntities.Count; index++) {
				var line = messageEntities[index];
				var previousLine = messageEntities[index - 1];
				line.Position.Y = previousLine.Position.Y - font.LineSpacing;
			}
		}
	}

	public class ConsoleLogChangedEvent : EventArgs {
		public LogMessage Message { get; set; }
	}
	public enum LogMessageTypes {
		Command, Info, Error
	}
}
