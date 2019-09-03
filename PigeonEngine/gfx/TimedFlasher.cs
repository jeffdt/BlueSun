using Microsoft.Xna.Framework;
using pigeon.squab;
using pigeon.time;

namespace pigeon.gfx {
	public class TimedFlasher : Component {
		// parameters
		public float OnTime;
		public float OffTime;
		public Component CmptToFlash;

		// state
		private float timer;

		protected override void Initialize() { }

		protected override void Update() {
			timer -= Time.SecScaled;

			if (timer <= 0) {
				CmptToFlash.Enabled = !CmptToFlash.Enabled;
				timer = CmptToFlash.Enabled ? OnTime : OffTime;
			}
		}
	}
}