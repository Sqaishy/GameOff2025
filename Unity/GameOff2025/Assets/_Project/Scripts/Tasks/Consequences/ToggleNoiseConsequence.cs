using SubHorror.Noise;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Noise/Toggle noise Consequence")]
	public class ToggleNoiseConsequence : Consequence
	{
		[SerializeField] private NoiseSettings noiseSettings;
		[SerializeField] private ToggleNoisePlayback noisePlayback;

		public override void Apply(Task failedTask)
		{
			NoiseEmitter noiseEmitter = failedTask.TaskOwner.GetComponentInChildren<NoiseEmitter>();

			if (!noiseEmitter)
				return;

			switch (noisePlayback)
			{
				case ToggleNoisePlayback.Start:
					noiseEmitter.AddNoise(noiseSettings);
					break;
				case ToggleNoisePlayback.Stop:
					noiseEmitter.StopNoise(noiseSettings);
					break;
			}
		}

		private enum ToggleNoisePlayback
		{
			Start,
			Stop,
		}
	}
}