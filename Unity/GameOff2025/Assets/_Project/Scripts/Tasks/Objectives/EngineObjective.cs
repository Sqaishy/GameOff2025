using System;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Engine Objective")]
	public class EngineObjective : Objective<EngineData>
	{
		public override Task.Status Enter()
		{
			objectiveData.engine.Initialize(objectiveData.repairTime,
				objectiveData.distanceToRepair);

			return Task.Status.Running;
		}

		public override Task.Status Process()
		{
			if (objectiveData.engine.EngineFixed())
				return Task.Status.Success;

			return Task.Status.Running;
		}

		public override void Exit()
		{

		}
	}

	[Serializable]
	public class EngineData : ObjectiveData
	{
		public float repairTime;
		public float distanceToRepair;
		public Engine engine;

		public override ObjectiveData Clone()
		{
			return new EngineData()
			{
				repairTime = repairTime,
				distanceToRepair = distanceToRepair,
				engine = engine
			};
		}
	}
}