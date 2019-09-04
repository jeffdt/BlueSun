using Microsoft.Xna.Framework;
using Pigeon;
using Pigeon.rand;
using Pigeon.squab;
using Pigeon.Time;
using Pigeon.utilities;

namespace PigeonEngine.particle {
	public class ConstantParticleEmitter : Component {
		public float MinLifespan;
		public float MaxLifespan;
		public float MinSpeed;
		public float MaxSpeed;
		public float DrawLayer;
		public int SprayCountMin;
		public int SprayCountMax;
		public Vector2 EmitDirection;
		public Color Color;
		public float SprayAngleVarianceDegrees;

		public float EmitRateMin;
		public float EmitRateMax;

		private float emitTimer;

		protected override void Initialize() {
			emitTimer = Rand.Float(0, EmitRateMax);
		}

		protected override void Update() {
			emitTimer -= Time.SecScaled;
			if (emitTimer <= 0) {
				emitTimer = Rand.Float(EmitRateMin, EmitRateMax);

				int count = Rand.Int(SprayCountMin, SprayCountMax);
				for (int i = 0; i < count; i++) {
					var particle = Particle.Get();

					particle.Position = Object.WorldPosition;

					var adjSprayAngle = VectorOps.AddAngleDegrees(EmitDirection, Rand.Float(-SprayAngleVarianceDegrees / 2, SprayAngleVarianceDegrees / 2));
					particle.Velocity = VectorOps.Scale(adjSprayAngle, Rand.Float(MinSpeed, MaxSpeed));

					particle.Color = Color;
					particle.Lifespan = Rand.Float(MinLifespan, MaxLifespan);
					particle.DrawLayer = DrawLayer;

                    Pigeon.Pigeon.World.ParticleRegistry.Register(particle);
				}
			}
		}
	}
}
