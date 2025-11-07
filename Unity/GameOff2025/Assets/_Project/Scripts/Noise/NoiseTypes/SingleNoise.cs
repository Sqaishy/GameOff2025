using UnityEngine;

namespace SubHorror.Noise
{
	/// <summary>
	/// Plays a noise once
	/// </summary>
	public class SingleNoise : INoise
	{
		public NoiseEmitter NoiseEmitter { get; }
		public bool NoiseActive => remainingDuration > 0;

		private NoiseSettings NoiseSettings { get; }
		private float remainingDelay;
		private float remainingDuration;

		public SingleNoise(NoiseEmitter noiseEmitter, NoiseSettings noiseSettings)
		{
			NoiseEmitter = noiseEmitter;
			NoiseSettings = noiseSettings;
		}

		public void ReduceNoise()
		{
			if (remainingDelay > 0f)
				remainingDelay -= Time.deltaTime;
			else
				remainingDuration -= Time.deltaTime;
		}

		public float CurrentNoiseLevel()
		{
			float noiseInterpolation = NoiseSettings.NoiseFalloff.Evaluate(
				remainingDuration / NoiseSettings.Duration);

			return NoiseSettings.NoiseLevel * noiseInterpolation;
		}
	}
}