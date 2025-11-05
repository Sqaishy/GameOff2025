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
			//This is where you want to go over the list of all active noises and run them
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
			//Check if noise trying to play already exists as an active noise
			//If not create it

			//If cant get noise emitter from dictionary create the element
			//If it doesn't exist it means this noise emitter is not making any noise
			if (!activeNoises.TryGetValue(noiseEmitter, out Dictionary<NoiseSettings, ActiveNoise> noise))
			{
				noise = new Dictionary<NoiseSettings, ActiveNoise>();
				activeNoises.Add(noiseEmitter, noise);
			}

			//If you cant get the active noise then create it
			if (!noise.TryGetValue(noiseSettings, out ActiveNoise activeNoise))
			{
				activeNoise = new ActiveNoise(noiseSettings);
				noise[noiseSettings] = activeNoise;
			}

			if (!activeNoiseSet.Contains(activeNoise))
				activeNoiseSet.Add(activeNoise);

			activeNoise.Reset();
		}

		public float GetNoiseLevel(NoiseEmitter noiseEmitter)
		{
			float sum = 0;

			foreach (KeyValuePair<NoiseSettings, ActiveNoise> kvp in activeNoises[noiseEmitter])
			{
				if (!kvp.Value.Active)
					continue;

				sum += kvp.Value.CurrentSoundLevel;
			}

			return sum;
		}

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