using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Wait Objective")]
	public class WaitObjective : Objective
	{
		public override Task.Status Enter()
		{
			Debug.Log($"Wait time {timerDuration} seconds");

			return Task.Status.Running;
		}

		public override Task.Status Process()
		{
			return Task.Status.Running;
		}

		public override void Exit()
		{

		}
	}
}