using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SubHorror.Noise
{
	public class NoiseEmitter : MonoBehaviour
	{
		[SerializeField] private AnimationCurve falloffRange = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		private List<NoiseEmitter> nearbyEmitters = new();

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

		/// <summary>
		/// Play a noise through this emitter
		/// </summary>
		/// <param name="noiseSettings">Controls how the noise is handled</param>
		public void PlayNoise(NoiseSettings noiseSettings)
		{
			NoiseManager.Instance.PlayNoise(this, noiseSettings);

			//Maybe also play some sound particles at the noise location
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