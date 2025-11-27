using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubHorror.Noise
{
	public class NoiseEmitter : MonoBehaviour
	{
		[SerializeField] private AnimationCurve falloffRange = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		private List<NoiseEmitter> nearbyEmitters = new();
		private Dictionary<NoiseSettings, ToggleNoise> noiseMap = new();

		private void OnDestroy()
		{
			NoiseManager.Instance.StopNoises(this);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out NoiseEmitter noiseEmitter))
				nearbyEmitters.Add(noiseEmitter);
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out NoiseEmitter noiseEmitter))
				nearbyEmitters.Remove(noiseEmitter);
		}

		public static NoiseEmitter GetLoudestNoiseEmitter() => NoiseManager.Instance.GetLoudestNoiseEmitter();

		/// <summary>
		/// Play a noise through this emitter
		/// </summary>
		/// <param name="noise">The noise to be played</param>
		public void PlayNoise(INoise noise)
		{
			NoiseManager.Instance.PlayNoise(this, noise);
		}

		public void AddNoise(NoiseSettings noiseSettings)
		{
			if (noiseMap.ContainsKey(noiseSettings))
				StopNoise(noiseSettings);

			noiseMap[noiseSettings] = new ToggleNoise(this, noiseSettings);
			noiseMap[noiseSettings].Play();
		}

		public void StopNoise(NoiseSettings noiseSettings)
		{
			noiseMap[noiseSettings].NoisePlaying(false);

			StartCoroutine(ReleaseNoise(noiseMap[noiseSettings]));
		}

		private IEnumerator ReleaseNoise(ToggleNoise noise)
		{
			while (noise.NoiseActive)
				yield return null;

			noise.Dispose();
		}

		/// <returns>The total noise level this emitter is making</returns>
		/// <remarks>
		/// To get the noise level including nearby emitters use <see cref="TotalNoiseLevelCombined"/>
		/// </remarks>
		public float TotalNoiseLevel() => NoiseManager.Instance.GetNoiseLevel(this);

		/// <returns>The total noise level of this and any nearby noise emitters</returns>
		public float TotalNoiseLevelCombined()
		{
			if (nearbyEmitters.Count == 0)
				return TotalNoiseLevel();

			float otherNoiseLevel = 0;

			foreach (NoiseEmitter emitter in nearbyEmitters)
			{
				float distance = Vector3.Distance(transform.position, emitter.transform.position);
				float range = emitter.falloffRange.Evaluate(1 / distance);
				float noiseLevel = emitter.TotalNoiseLevel() * range;

				otherNoiseLevel += noiseLevel;
			}

			return TotalNoiseLevel() + otherNoiseLevel;
		}
	}
}