using System;
using SubHorror.Interaction;
using UnityEngine;

namespace SubHorror.Tasks
{
	public class ObjectiveInteractable : MonoBehaviour, IInteractable
	{
		[SerializeReference] private Objective objective;
		[SerializeReference] private ObjectiveData objectiveData;

		#if UNITY_EDITOR

		[SerializeField, HideInInspector] private Objective previousObjective;

		#endif

		public bool CanInteract()
		{
			return true;
		}

		public void Interact(GameObject interactor, InteractorContext context)
		{
			objective.Enter();
		}

		public void ResetInteraction()
		{

		}

		#region Editor Validation

		#if UNITY_EDITOR

		private void OnValidate()
		{
			ValidateObjective();
		}

		private void ValidateObjective()
		{
			if (!objective)
			{
				previousObjective = null;
				objectiveData = null;
				return;
			}

			if (previousObjective != objective)
			{
				previousObjective = objective;
				objectiveData = objective.ObjectiveDataType as ObjectiveData;
			}
		}

		[ContextMenu("Reset Objective Data")]
		private void ResetObjectiveData()
		{
			objectiveData = objective.ObjectiveDataType as ObjectiveData;

			Debug.Log($"Resetting Objective Data to {objectiveData}");
		}

#endif

		#endregion
	}
}