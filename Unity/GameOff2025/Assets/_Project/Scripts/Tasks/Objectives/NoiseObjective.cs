using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Noise Objective")]
	public class NoiseObjective : Objective
	{
		[SerializeField] private ToggleNoiseConsequence noiseConsequence;

		public override Task.Status Enter()
		{
			return Task.Status.Running;
		}

		public override Task.Status Process()
		{
			noiseConsequence.Apply(Task);

			return Task.Status.Success;
		}

		public override void Exit()
		{

		}
	}
}