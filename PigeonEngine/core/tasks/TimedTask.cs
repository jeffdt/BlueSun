using System;
using Pigeon.Time;

namespace Pigeon.Core.tasks {
	namespace xnapunk.task {
		public class TimedTask {
			private readonly Action callback;
			private float time;

			public TimedTask(Action callback, float time) {
				this.callback = callback;
				this.time = time;
			}

			public bool Update() {
                time -= Time.Time.SecScaled;

				if (time <= 0) {
					callback.Invoke();
					return true;
				}

				return false;
			}
		}
	}
}
