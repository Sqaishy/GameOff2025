using System;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Location Objective")]
	public class LocationObjective : Objective<LocationData>
	{
		public override Task.Status Enter()
		{
			Debug.Log($"Location {objectiveData.location.name}: {objectiveData.location.position}");
			ResetObjective();

			return Task.Status.Running;
		}

		public override Task.Status Process()
		{
			if (useTimer && currentTime >= timerDuration)
				return Task.Status.Failure;

			currentTime += Time.deltaTime;
			float distance = Vector3.Distance(Owner.transform.position, objectiveData.location.position);

			if (distance < objectiveData.distanceToLocation)
				return Task.Status.Success;

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
		public float distanceToLocation;

		public override ObjectiveData Clone()
		{
			return new LocationData()
			{
				location = location,
				distanceToLocation = distanceToLocation
			};
		}
	}
}