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

		public float NoiseLevel => noiseLevel;
		public float Delay => delay;
		public float Duration => duration;
		public AnimationCurve NoiseFalloff => noiseFalloff;

		public static NoiseSettings Create(float noiseLevel, float delay, float duration,
			AnimationCurve noiseFalloff)
		{
			NoiseSettings createSettings = CreateInstance<NoiseSettings>();
			createSettings.noiseLevel = noiseLevel;
			createSettings.delay = delay;
			createSettings.duration = duration;
			createSettings.noiseFalloff = noiseFalloff;

			return createSettings;
		}

		public static NoiseSettings Create(float noiseLevel, float delay, float duration)
		{
			return Create(noiseLevel, delay, duration,
				AnimationCurve.Linear(0, 0, 1, 1));
		}

		public NoiseLevelModifier NoiseModifier => new NoiseLevelModifier(this);

		public class NoiseLevelModifier
		{
			private float originalValue;
			private float modifiedValue;
			private NoiseSettings noiseSettings;

			public NoiseLevelModifier(NoiseSettings noiseSettings)
			{
				this.noiseSettings = noiseSettings;
				originalValue = noiseSettings.NoiseLevel;
				modifiedValue = noiseSettings.NoiseLevel;
			}

			public NoiseSettings GetModifiedValue(FloatModifier modifier)
			{
				noiseSettings.noiseLevel = modifier.Modify(modifiedValue);
				return noiseSettings;
			}
		}
	}

	public class NoiseModifier
	{
		public NoiseSettings NoiseSettings { get; private set; }
		public float SoundLevel { get; set; }
		public float Delay { get; set; }
		public float Duration { get; set; }

		public NoiseModifier(NoiseSettings noiseSettings)
		{
			NoiseSettings = noiseSettings;
		}
	}

	public abstract class FloatModifier
	{
		public abstract float Modify(float input);
	}

	public class MovementNoiseModifier : FloatModifier
	{
		private bool isSprinting;

		public MovementNoiseModifier(bool isSprinting)
		{
			this.isSprinting = isSprinting;
		}

		public override float Modify(float input)
		{
			return isSprinting ? input * 2f : input;
		}
	}
}