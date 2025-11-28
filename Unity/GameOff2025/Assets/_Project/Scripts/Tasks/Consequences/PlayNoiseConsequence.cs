using SubHorror.Noise;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Noise/Play noise Consequence")]
	public class PlayNoiseConsequence : Consequence
	{
		[SerializeField] private NoiseSettings noiseSettings;

		public override void Apply(Task failedTask)
		{
			NoiseEmitter noiseEmitter = failedTask.TaskOwner.GetComponentInChildren<NoiseEmitter>();

			if (!noiseEmitter)
				return;

			noiseEmitter.PlayNoise(new SingleNoise(noiseEmitter, noiseSettings));
		}
	}
}