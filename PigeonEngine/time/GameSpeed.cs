namespace Pigeon.Time {
	public static class GameSpeed {
		public static float Multiplier { get; set; }

		public static void Reinitialize() {
			Multiplier = 1f;
		}
	}
}