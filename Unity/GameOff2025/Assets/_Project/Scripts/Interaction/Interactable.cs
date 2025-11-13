using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SubHorror.Interaction
{
	public class Interactable : MonoBehaviour, IInteractable, ISerializationCallbackReceiver
	{
		[SerializeField] private List<ContextInteractable> contexts = new();

		private Dictionary<InteractorContext, List<ContextAction>> contextActions = new();

		[Serializable]
		public struct ContextInteractable
		{
			public InteractorContext interactorContext;
			public List<ContextAction> actions;
		}

		public bool CanInteract()
		{
			return true;
		}

		public void Interact(GameObject interactor, InteractorContext context)
		{
			foreach (ContextAction action in contextActions[context])
				action.Execute(interactor, gameObject);
		}

		public void ResetInteraction()
		{

		}

		public void OnBeforeSerialize()
		{
			contexts.Clear();

			foreach (KeyValuePair<InteractorContext,List<ContextAction>> kvp in contextActions)
			{
				contexts.Add(new ContextInteractable()
				{
					interactorContext = kvp.Key,
					actions = kvp.Value
				});
			}
		}

		public void OnAfterDeserialize()
		{
			contextActions.Clear();

			for (var index = 0; index < contexts.Count; index++)
			{
				ContextInteractable contextInteractable = contexts[index];

				if (!contextInteractable.interactorContext || contextInteractable.actions == null
				                                           || contextInteractable.actions.Count == 0)
				{
					Debug.LogWarning($"Context at index {index} has missing variables, it will be ignored", gameObject);
					continue;
				}

				contextActions.Add(contextInteractable.interactorContext, contextInteractable.actions);
			}
		}
	}
}