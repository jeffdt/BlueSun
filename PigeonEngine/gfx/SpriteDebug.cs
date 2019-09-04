using Pigeon;
using Pigeon.Gfx;
using Pigeon.squab;
using Pigeon.Time;

namespace PigeonEngine.gfx {
	public static class SpriteDebug {
		public static void CreateTestAnim(string sprite, string animation, bool isCentered, bool continuousLoop) {
            Pigeon.Pigeon.World.DeleteObjSafe("ANIM_TEST");
			Squabject animObj = new Squabject("ANIM_TEST", .99f);

			var spriteRenderer = new SpriteRenderer(sprite);

			if (continuousLoop) {
				spriteRenderer.Loop(animation);
			} else {
				animObj.AddComponent(new SpriteDelayedLooper { Animation = animation });
			}

			animObj.AddComponent(spriteRenderer);

			if (isCentered) {
				animObj.FlatLocalPosition = Pigeon.Pigeon.Renderer.BaseScreenCenter;
			}

            Pigeon.Pigeon.World.AddObj(animObj);
		}

		private class SpriteDelayedLooper : Component {
			private const float LOOP_LENGTH = 1f;

			public string Animation;

			private SpriteRenderer spriteRenderer;
			private float loopTimer;

			protected override void Initialize() {
				spriteRenderer = GetComponent<SpriteRenderer>();
				loopTimer = 0;
			}

			protected override void Update() {
				loopTimer -= Time.Seconds;
				if (loopTimer <= 0) {
					Enabled = false;
					spriteRenderer.Play(Animation, () => {
						Enabled = true;
						loopTimer = LOOP_LENGTH;
					});
				}
			}
		}
	}
}
