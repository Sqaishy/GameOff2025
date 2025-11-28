using System;
using SubHorror.Interaction;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SubHorror.Tasks
{
	public class ObjectiveInteractable : MonoBehaviour, IInteractable
	{
		[SerializeField] private InteractorContext interactorContext;
		[SerializeReference] private Objective objective;
		[SerializeReference] private ObjectiveData objectiveData;

		public event Action OnInteracted;

		#if UNITY_EDITOR

		[SerializeField, HideInInspector] private Objective previousObjective;

		#endif

		private void Awake()
		{
			objective.OverrideDataType(objectiveData);
		}

		public bool CanInteract()
		{
			return true;
		}

		public void Interact(GameObject interactor, InteractorContext context)
		{
			if (context != interactorContext)
				return;

			OnInteracted?.Invoke();
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
				objectiveData = (objective.ObjectiveDataType as ObjectiveData)?.Clone();
			}
		}

		[ContextMenu("Reset Objective Data")]
		private void ResetObjectiveData()
		{
			objectiveData = (objective.ObjectiveDataType as ObjectiveData)?.Clone();

			Debug.Log($"Resetting Objective Data to {objectiveData}");
		}

#endif

		#endregion
	}

	public static class TransformExtensions
	{
		/// <summary>
		/// Destroys all children under this transform
		/// </summary>
		/// <param name="transform"></param>
		public static void DestroyChildren(this Transform transform)
		{
			foreach (Transform child in transform)
				Object.Destroy(child.gameObject);
		}
	}
}