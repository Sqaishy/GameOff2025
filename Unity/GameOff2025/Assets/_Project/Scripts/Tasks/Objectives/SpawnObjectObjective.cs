using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Spawn Object Objective")]
	public class SpawnObjectObjective : Objective<SpawnData>
	{
		public override Task.Status Enter()
		{
			Debug.Log($"Spawning {objectiveData.spawnObject.name} at {objectiveData.spawnLocation.name} {objectiveData.spawnLocation.position}");

			return Task.Status.Running;
		}

		public override Task.Status Process()
		{
			Instantiate(objectiveData.spawnObject, objectiveData.spawnLocation.position, Quaternion.identity);
			return Task.Status.Success;
		}

		public override void Exit()
		{

		}

		public override string DisplayObjectiveText() =>
			string.Format(objectiveText, objectiveData.spawnObject.name, objectiveData.spawnLocation.name);
	}

	[Serializable]
	public class SpawnData : ObjectiveData
	{
		public GameObject spawnObject;
		public Transform spawnLocation;

		public override ObjectiveData Clone()
		{
			return new SpawnData()
			{
				spawnObject = spawnObject,
				spawnLocation = spawnLocation,
			};
		}
	}
}