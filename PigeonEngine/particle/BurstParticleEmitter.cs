using Microsoft.Xna.Framework;
using pigeon;
using pigeon.rand;
using pigeon.squab;
using pigeon.utilities;

namespace PigeonEngine.particle {
	public class BurstParticleEmitter : Component {
		public float MinLifespan;
		public float MaxLifespan;
		public float MinSpeed;
		public float MaxSpeed;
		public float DrawLayer;
		public int SprayCount;
		public Vector2 SprayDir;
		public Color Color;
		public float SprayAngleVarianceDegrees;

		protected override void Initialize() { }
		protected override void Update() { }

		public void Burst(Point position) {
			for (int i = 0; i < SprayCount; i++) {
				var particle = Particle.Get();

				particle.Position = position;

				var adjSprayAngle = VectorOps.AddAngleDegrees(SprayDir, Rand.Float(-SprayAngleVarianceDegrees, SprayAngleVarianceDegrees));
				particle.Velocity = VectorOps.Scale(adjSprayAngle, Rand.Float(MinSpeed, MaxSpeed));
				
				particle.Color = Color;
				particle.Lifespan = Rand.Float(MinLifespan, MaxLifespan);
				particle.DrawLayer = DrawLayer;

				Pigeon.World.ParticleRegistry.Register(particle);
			}
		}

		public void Burst() {
			Burst(Object.WorldPosition);
		}
	}
}