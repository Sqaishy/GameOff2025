using System;
using System.Linq;
using UnityEngine;

namespace SubHorror.Noise
{
	public class NoiseEmitter : MonoBehaviour
	{
		//Component added to objects that need to emit a noise\

		private void OnDestroy()
		{
			NoiseManager.Instance.StopNoises(this);
		}

		public void PlayNoise(NoiseSettings noiseSettings)
		{
			NoiseManager.Instance.PlayNoise(this, noiseSettings);

			//Maybe also play some sound particles at the noise location
		}

		//Probably control combining sounds here instead of the manager
		//On trigger enter/exit with other noise emitters combine noise based on distance
		public float TotalNoiseLevel() => NoiseManager.Instance.GetNoiseLevel(this);
	}
}