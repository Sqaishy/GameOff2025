using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace SubHorror.Noise
{
	/// <summary>
	/// Toggles a noise on or off
	/// </summary>
	public class ToggleNoise : INoise
	{
		public NoiseEmitter NoiseEmitter { get; }
		public bool NoiseActive => isPlaying || remainingDuration > 0;

		private NoiseSettings NoiseSettings { get; }
		private EventInstance audioInstance;
		private bool isPlaying;
		private float remainingDelay;
		private float remainingDuration;

		public ToggleNoise(NoiseEmitter noiseEmitter, NoiseSettings noiseSettings)
		{
			NoiseEmitter = noiseEmitter;
			NoiseSettings = noiseSettings;
			audioInstance = RuntimeManager.CreateInstance(NoiseSettings.AudioEvent);
		}

		public void Play()
		{
			ResetNoise();
			NoisePlaying(true);
			NoiseEmitter.PlayNoise(this);
		}

		public void NoisePlaying(bool toggle)
		{
			isPlaying = toggle;

			if (isPlaying)
				audioInstance.start();
			else
				AudioManager.Instance.StopLoop(audioInstance);
		}

		public void ResetNoise()
		{
			isPlaying = false;
			remainingDelay = NoiseSettings.Delay;
			remainingDuration = NoiseSettings.Duration;
		}

		public void ReduceNoise()
		{
			if (isPlaying)
				return;

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