using UnityEngine;

namespace SubHorror.Noise
{
	public class NoisePlayer : MonoBehaviour
	{
		[SerializeField] private NoiseSettings noiseSettings;

		private NoiseEmitter noiseEmitter;
		private ToggleNoise toggleNoise;

		private void Awake()
		{
			noiseEmitter = GetComponent<NoiseEmitter>();
			toggleNoise = new ToggleNoise(noiseEmitter, noiseSettings);
		}

		private void OnEnable()
		{
			noiseEmitter.OnNoiseEntered += PlayNoise;
			noiseEmitter.OnNoiseExited += StopNoise;
		}

		private void OnDisable()
		{
			noiseEmitter.OnNoiseEntered -= PlayNoise;
			noiseEmitter.OnNoiseExited -= StopNoise;
		}

		private void OnDestroy()
		{
			toggleNoise.Dispose();
		}

		private void PlayNoise()
		{
			toggleNoise.Play();
		}

		private void StopNoise()
		{
			toggleNoise.Pause();
		}
	}
}