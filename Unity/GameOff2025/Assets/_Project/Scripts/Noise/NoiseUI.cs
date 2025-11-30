using System;
using UnityEngine;
using UnityEngine.UI;

namespace SubHorror.Noise.UI
{
	public class NoiseUI : MonoBehaviour
	{
		[SerializeField] private NoiseEmitter emitter;
		[SerializeField] private GameDifficulty difficulty;
		[Header("UI References")]
		[SerializeField] private Slider noiseSlider;
		[SerializeField] private Image warningImage;

		private void Update()
		{
			float noiseLevel = emitter.TotalNoiseLevelCombined();

			noiseSlider.value = noiseLevel;

			warningImage.enabled = noiseLevel >= difficulty.MaxNoiseThreshold;
		}
	}
}