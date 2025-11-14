using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Location Objective")]
	public class LocationObjective : Objective<Transform>
	{
		public override Task.Status Start()
		{
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