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
			currentTime = timerDuration;

			return Task.Status.Running;
		}

		public override Task.Status Process()
		{
			currentTime -= Time.deltaTime;

			Debug.Log(DisplayObjectiveText());

			if (currentTime <= 0)
				return Task.Status.Success;

			return Task.Status.Running;
		}

		public override void Exit()
		{

		}

		public override string DisplayObjectiveText() =>
			string.Format(objectiveText, currentTime.ToString("F1"));

		private void OnValidate()
		{
			useTimer = true;
		}
	}
}