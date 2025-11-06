using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SubHorror.Noise
{
	public class NoiseManager : MonoBehaviour
	{
		public static NoiseManager Instance { get; private set; }

		private Dictionary<NoiseEmitter, Dictionary<NoiseSettings, ActiveNoise>> activeNoises = new();
		private List<ActiveNoise> activeNoiseSet = new();

		private void Awake()
		{
			if (!Instance)
				Instance = this;
			else
				Destroy(gameObject);
		}

		private void Update()
		{
			if (activeNoiseSet.Count == 0)
				return;

			for (int i = activeNoiseSet.Count - 1; i >= 0; i--)
			{
				if (activeNoiseSet[i].RemainingDelay > 0f)
					activeNoiseSet[i].RemainingDelay -= Time.deltaTime;
				else
					activeNoiseSet[i].RemainingDuration -= Time.deltaTime;

				if (activeNoiseSet[i].RemainingDuration <= 0)
				{
					activeNoiseSet[i].Active = false;
					activeNoiseSet.RemoveAt(i);
				}
			}
		}

		public void PlayNoise(NoiseEmitter noiseEmitter, NoiseSettings noiseSettings)
		{
			if (!activeNoises.TryGetValue(noiseEmitter, out Dictionary<NoiseSettings, ActiveNoise> noise))
			{
				noise = new Dictionary<NoiseSettings, ActiveNoise>();
				activeNoises.Add(noiseEmitter, noise);
			}

			if (!noise.TryGetValue(noiseSettings, out ActiveNoise activeNoise))
			{
				activeNoise = new ActiveNoise(noiseSettings);
				noise[noiseSettings] = activeNoise;
			}

			if (!activeNoiseSet.Contains(activeNoise))
				activeNoiseSet.Add(activeNoise);

			activeNoise.Reset();
		}

		/// <summary>
		/// Gets all the noises that the noises emitter is making and adds them together
		/// </summary>
		/// <param name="noiseEmitter">The emitter making the noises</param>
		/// <returns>All noises summed together</returns>
		public float GetNoiseLevel(NoiseEmitter noiseEmitter)
		{
			if (!activeNoises.ContainsKey(noiseEmitter))
				return 0f;

			float sum = 0;

			foreach (KeyValuePair<NoiseSettings, ActiveNoise> kvp in activeNoises[noiseEmitter])
			{
				if (!kvp.Value.Active)
					continue;

				sum += kvp.Value.CurrentSoundLevel;
			}

			return sum;
		}

		/// <summary>
		/// Stop all noises coming from the noise emitter
		/// </summary>
		///  <remarks>Useful when destroying objects</remarks>
		/// <param name="noiseEmitter">The noise emitter making the noises</param>
		public void StopNoises(NoiseEmitter noiseEmitter)
		{
			//If the noise emitter is destroyed you want to stop playing any noises
			if (!activeNoises.TryGetValue(noiseEmitter, out Dictionary<NoiseSettings, ActiveNoise> noise))
				return;

			foreach (KeyValuePair<NoiseSettings,ActiveNoise> kvp in noise)
				activeNoiseSet.Remove(kvp.Value);

			activeNoises.Remove(noiseEmitter);
		}
	}

	public class ActiveNoise
	{
		public NoiseSettings NoiseSettings { get; private set; }
		public float RemainingDelay { get; set; }
		public float RemainingDuration { get; set; }
		public bool Active { get; set; }
		public float CurrentSoundLevel => CalculateSoundLevel();

		public ActiveNoise(NoiseSettings noiseSettings)
		{
			NoiseSettings = noiseSettings;
		}

		public void Reset()
		{
			RemainingDelay = NoiseSettings.Delay;
			RemainingDuration = NoiseSettings.Duration;
			Active = true;
		}

		private float CalculateSoundLevel()
		{
			float noiseInterpolation = NoiseSettings.NoiseFalloff.Evaluate(
				RemainingDuration / NoiseSettings.Duration);

			return NoiseSettings.NoiseLevel * noiseInterpolation;
		}
	}
}