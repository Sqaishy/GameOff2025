using System;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Location Objective")]
	public class LocationObjective : Objective<LocationData>
	{
		public override Task.Status Enter()
		{
			Debug.Log($"Location null {objectiveData.location == null}");
			Debug.Log($"Location {objectiveData.location.name}: {objectiveData.location.position}");

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

	[Serializable]
	public class LocationData : ObjectiveData
	{
		public Transform location;
	}
}