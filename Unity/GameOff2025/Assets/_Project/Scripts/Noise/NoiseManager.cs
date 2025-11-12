using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SubHorror.Noise
{
	public class NoiseManager : MonoBehaviour
	{
		public static NoiseManager Instance { get; private set; }

		private Dictionary<NoiseEmitter, List<INoise>> activeNoisesDic = new();
		private List<INoise> noises = new();

		private void Awake()
		{
			if (!Instance)
				Instance = this;
			else
				Destroy(gameObject);
		}

		private void Update()
		{
			if (noises.Count == 0)
				return;

			for (int i = noises.Count - 1; i >= 0; i--)
			{
				noises[i].ReduceNoise();

				if (!noises[i].NoiseActive)
					RemoveActiveNoise(noises[i]);
			}
		}

		public void PlayNoise(NoiseEmitter noiseEmitter, INoise noise)
		{
			if (!activeNoisesDic.TryGetValue(noiseEmitter, out List<INoise> noisesList))
			{
				noisesList = new List<INoise>();
				activeNoisesDic.Add(noiseEmitter, noisesList);
			}

			if (noisesList.Contains(noise))
				return;

			noisesList.Add(noise);
			noises.Add(noise);
		}

		/// <summary>
		/// Gets all the noises that the noise emitter is making and adds them together
		/// </summary>
		/// <param name="noiseEmitter">The emitter making the noises</param>
		/// <returns>All noises summed together</returns>
		public float GetNoiseLevel(NoiseEmitter noiseEmitter)
		{
			if (!activeNoisesDic.ContainsKey(noiseEmitter))
				return 0f;

			float sum = 0f;

			foreach (INoise noise in activeNoisesDic[noiseEmitter])
				sum += noise.CurrentNoiseLevel();

			return sum;
		}

		/// <summary>
		/// Gets the
		/// </summary>
		/// <returns></returns>
		public NoiseEmitter GetLoudestNoiseEmitter()
		{
			if (activeNoisesDic.Count == 0)
				return null;

			NoiseEmitter loudestEmitter = null;
			float noiseLevel = 0f;

			foreach (KeyValuePair<NoiseEmitter,List<INoise>> keyValuePair in activeNoisesDic)
			{
				float noiseToCompare = GetNoiseLevel(keyValuePair.Key);

				if (noiseToCompare <= noiseLevel)
					continue;

				loudestEmitter = keyValuePair.Key;
				noiseLevel = noiseToCompare;
			}

			return loudestEmitter;
		}

		/// <summary>
		/// Stop all noises coming from the noise emitter
		/// </summary>
		///  <remarks>Useful when destroying objects</remarks>
		/// <param name="noiseEmitter">The noise emitter making the noises</param>
		public void StopNoises(NoiseEmitter noiseEmitter)
		{
			if (!activeNoisesDic.TryGetValue(noiseEmitter, out List<INoise> noisesList))
				return;

			foreach (INoise noise in noisesList)
				noises.Remove(noise);

			activeNoisesDic.Remove(noiseEmitter);
		}

		private void RemoveActiveNoise(INoise noise)
		{
			NoiseEmitter emitter = noise.NoiseEmitter;

			activeNoisesDic[emitter].Remove(noise);

			if (activeNoisesDic[emitter].Count == 0)
				activeNoisesDic.Remove(emitter);

			noises.Remove(noise);
		}
	}
}