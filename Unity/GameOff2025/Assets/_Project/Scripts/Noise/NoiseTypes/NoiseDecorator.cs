namespace SubHorror.Noise
{
	public class NoiseDecorator<T> : INoise where T : INoise
	{
		public float NoiseLevel { get; set; }
		public float Delay { get; set; }
		public float Duration { get; set; }
		public NoiseEmitter NoiseEmitter { get; }
		public bool NoiseActive => Noise.NoiseActive;
		public T Noise { get; }

		public NoiseDecorator(T noise)
		{
			Noise = noise;
			NoiseEmitter = noise.NoiseEmitter;
		}

		public void ReduceNoise()
		{
			Noise.ReduceNoise();
		}

		public float CurrentNoiseLevel()
		{
			return Noise.CurrentNoiseLevel() + NoiseLevel;
		}
	}
}