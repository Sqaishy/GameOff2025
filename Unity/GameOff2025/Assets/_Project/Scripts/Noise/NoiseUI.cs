using System;
using UnityEngine;
using UnityEngine.UI;

namespace SubHorror.Noise.UI
{
	public class NoiseUI : MonoBehaviour
	{
		[SerializeField] private NoiseEmitter emitter;
		[Header("UI References")]
		[SerializeField] private Slider noiseSlider;

		private void Update()
		{
			noiseSlider.value = emitter.TotalNoiseLevel();
		}
	}
}