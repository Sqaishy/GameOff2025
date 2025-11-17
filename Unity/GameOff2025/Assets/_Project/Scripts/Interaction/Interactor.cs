using System;
using UnityEngine;

namespace SubHorror.Interaction
{
	public class Interactor : MonoBehaviour
	{
		[SerializeField] private InteractorContext context;

		public event Action OnEnterInteractable;
		public event Action OnExitInteractable;

		private IInteractable currentInteractable;
		private IInteractable previousInteractable;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.TryGetComponent(out IInteractable interactable))
				return;

			currentInteractable = interactable;
			OnEnterInteractable?.Invoke();
		}

		private void OnTriggerExit(Collider other)
		{
			if (!other.TryGetComponent(out IInteractable interactable))
				return;

			currentInteractable = null;
			OnExitInteractable?.Invoke();
		}

		public void Interact()
		{
			if (currentInteractable == null)
				return;

			if (previousInteractable == currentInteractable)
				return;

			previousInteractable?.ResetInteraction();
			currentInteractable.Interact(gameObject, context);
			previousInteractable = currentInteractable;
		}
	}
}