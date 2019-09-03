using System;
using pigeon.time;

namespace pigeon.core.tasks {
	namespace xnapunk.task {
		public class TimedTask {
			private readonly Action callback;
			private float time;

			public TimedTask(Action callback, float time) {
				this.callback = callback;
				this.time = time;
			}

			public bool Update() {
				time -= Time.SecScaled;

				if (time <= 0) {
					callback.Invoke();
					return true;
				}

				return false;
			}
		}
	}
}
