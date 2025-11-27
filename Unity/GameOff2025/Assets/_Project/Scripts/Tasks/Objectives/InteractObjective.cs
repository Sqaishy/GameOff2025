using System;
using System.Collections.Generic;
using SubHorror.Interaction;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Objectives/Interact Objective")]
	public class InteractObjective : Objective<InteractData>
	{
		[SerializeField] private bool destroyChildren;

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

		public override void ResetObjective()
		{
			base.ResetObjective();

			if (destroyChildren)
				objectiveData.interactable.transform.DestroyChildren();
		}

		private void Interact()
		{
			interacted = true;

			foreach (ContextAction action in objectiveData.actions)
				action.Execute(Owner, objectiveData.interactable.gameObject);
		}
	}

	[Serializable]
	public class InteractData : ObjectiveData
	{
		public ObjectiveInteractable interactable;
		public List<ContextAction> actions;

		public override ObjectiveData Clone()
		{
			return new InteractData()
			{
				interactable = interactable,
				actions = actions
			};
		}
	}
}