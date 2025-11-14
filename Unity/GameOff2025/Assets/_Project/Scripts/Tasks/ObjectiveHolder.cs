using UnityEngine;

namespace SubHorror.Tasks
{
	/// <summary>
	/// For when the objective just needs to exist in the scene to access references but not be interactable
	/// </summary>
	public class ObjectiveHolder : MonoBehaviour
	{
		[SerializeReference] private Objective objective;
		[SerializeReference] private ObjectiveData objectiveData;

#if UNITY_EDITOR

		[SerializeField, HideInInspector] private Objective previousObjective;

#endif

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