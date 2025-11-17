using FMOD.Studio;
using FMODUnity;
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
        [SerializeField] private EventReference audioEvent;

		public float NoiseLevel => noiseLevel;
		public float Delay => delay;
		public float Duration => duration;
		public AnimationCurve NoiseFalloff => noiseFalloff;
		public EventReference AudioEvent => audioEvent;

		public static NoiseSettings Create(float noiseLevel, float delay, float duration,
			AnimationCurve noiseFalloff, EventReference audioEvent = default)
		{
			NoiseSettings createSettings = CreateInstance<NoiseSettings>();
			createSettings.noiseLevel = noiseLevel;
			createSettings.delay = delay;
			createSettings.duration = duration;
			createSettings.noiseFalloff = noiseFalloff;
			createSettings.audioEvent = audioEvent;

			return createSettings;
		}

		public static NoiseSettings Create(float noiseLevel, float delay, float duration)
		{
			return Create(noiseLevel, delay, duration,
				AnimationCurve.Linear(0, 0, 1, 1));
		}
	}
}