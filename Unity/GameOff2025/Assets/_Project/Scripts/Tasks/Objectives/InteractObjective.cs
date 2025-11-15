using System;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Interact Objective")]
	public class InteractObjective : Objective<InteractData>
	{
		private bool interacted;

		public override Task.Status Enter()
		{
			objectiveData.interactable.OnInteracted += Interact;

			return Task.Status.Running;
		}

		public override Task.Status Process()
		{
			if (useTimer && currentTime >= timerDuration)
				return Task.Status.Failure;

			currentTime += Time.deltaTime;

			return interacted ? Task.Status.Success : Task.Status.Running;
		}

		public override void Exit()
		{
			objectiveData.interactable.OnInteracted -= Interact;

			interacted = false;
		}

		public override string DisplayObjectiveText() =>
			string.Format(objectiveText, objectiveData.interactable.name);

		private void Interact()
		{
			interacted = true;
		}
	}

	[Serializable]
	public class InteractData : ObjectiveData
	{
		public ObjectiveInteractable interactable;

		public override ObjectiveData Clone()
		{
			return new InteractData()
			{
				interactable = interactable
			};
		}
	}
}