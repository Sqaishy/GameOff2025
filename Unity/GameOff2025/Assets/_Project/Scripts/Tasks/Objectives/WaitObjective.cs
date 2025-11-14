using System;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Wait Objective")]
	public class WaitObjective : Objective
	{
		public override Task.Status Enter()
		{
			Debug.Log($"Wait time {timerDuration} seconds");
			ResetObjective();

			return Task.Status.Running;
		}

		public override Task.Status Process()
		{
			currentTime += Time.deltaTime;

			if (currentTime >= timerDuration)
				return Task.Status.Success;

			return Task.Status.Running;
		}

		public override void Exit()
		{

		}

		private void OnValidate()
		{
			useTimer = true;
		}
	}
}