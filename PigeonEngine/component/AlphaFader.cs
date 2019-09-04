using Microsoft.Xna.Framework;
using Pigeon.Gfx;
using Pigeon.squab;
using Pigeon.Time;

namespace Pigeon.component {
	public class AlphaFader : Component {
		// parameters
		public float CyclePeriod;
		public float MaxAlpha;
		public float MinAlpha;

		// pipes
		private ImageRenderer image;

		// state
		private float cycleTimer;
		private bool increasing;

		protected override void Initialize() {
			image = Object.GetComponent<ImageRenderer>();
			cycleTimer = CyclePeriod;
		}

		protected override void Update() {
			if (!increasing) {
                cycleTimer -= Time.Time.SecScaled;
				if (cycleTimer < 0) {
					cycleTimer = 0;
					increasing = true;
				}
			} else {
                cycleTimer += Time.Time.SecScaled;
				if (cycleTimer > CyclePeriod) {
					cycleTimer = CyclePeriod;
					increasing = false;
				}
			}
			var alphaRange = MaxAlpha - MinAlpha;
			var cycle = MathHelper.Lerp(0, CyclePeriod, cycleTimer);

			image.SetAlpha(MinAlpha + alphaRange * (cycle / CyclePeriod));
		}
	}
}