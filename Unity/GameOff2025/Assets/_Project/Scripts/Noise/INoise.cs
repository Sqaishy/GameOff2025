namespace SubHorror.Noise
{
	public interface INoise
	{
		NoiseEmitter NoiseEmitter { get; }
		bool NoiseActive { get; }
		void ReduceNoise();
		float CurrentNoiseLevel();
	}
}