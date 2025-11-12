using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubHorror.Interaction
{
	public class Interactable : MonoBehaviour, IInteractable, ISerializationCallbackReceiver
	{
		[SerializeField] private List<ContextInteractable> contexts = new();

		private Dictionary<InteractorContext, ContextAction> contextActions = new();

		[Serializable]
		public struct ContextInteractable
		{
			public InteractorContext interactorContext;
			public ContextAction action;
		}

		public bool CanInteract()
		{
			return true;
		}

		public void Interact(GameObject interactor, InteractorContext context)
		{
			Debug.Log($"{interactor.name}-{context.name} is interacting with {name}");
		}

		public void ResetInteraction()
		{

		}

		public void OnBeforeSerialize()
		{
			contextActions.Clear();

			foreach (ContextInteractable contextInteractable in contexts)
				contextActions.Add(contextInteractable.interactorContext, contextInteractable.action);
		}

		public void OnAfterDeserialize()
		{

		}
	}
}