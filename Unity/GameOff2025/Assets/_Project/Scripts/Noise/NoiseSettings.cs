using UnityEngine;

namespace SubHorror.Noise
{
	[CreateAssetMenu(menuName = "Sub Horror/Noise/Noise Settings")]
	public class NoiseSettings : ScriptableObject
	{
		[Tooltip("How much noise will be emitted when the noise is played")]
		[SerializeField] private float noiseLevel;
		[Tooltip("A delay before the sound duration begins to decrease")]
		[SerializeField] private float delay;
		[Tooltip("How long this noise lasts")]
		[SerializeField] private float duration;
		[Tooltip("Interpolates the sound level over the duration based on the noise falloff")]
		[SerializeField] private AnimationCurve noiseFalloff;
		[Tooltip("Controls how the noise level is interpolated over a distance")]
		[SerializeField] private AnimationCurve falloffRange;

		public float NoiseLevel => noiseLevel;
		public float Delay => delay;
		public float Duration => duration;
		public AnimationCurve NoiseFalloff => noiseFalloff;
		public AnimationCurve FalloffRange => falloffRange;
	}
}