using System;
using UnityEngine;

namespace SubHorror.Interaction
{
	public class Interactor : MonoBehaviour
	{
		[SerializeField] private InteractorContext context;

		/// <summary>
		/// Called when the interactor trigger enters an IInteractable
		/// </summary>
		public event Action OnEnterInteractable;
		/// <summary>
		/// Called when the interactor trigger exits an IInteractable
		/// </summary>
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

		/// <summary>
		/// Interact with the current IInteractable that you have triggered with
		/// </summary>
		/// <remarks>
		/// If no there is no interactable or the interactable has not changed, no interaction happens
		/// </remarks>
		public void Interact()
		{
			if (currentInteractable == null)
				return;

			if (previousInteractable == currentInteractable)
				return;

			if (!currentInteractable.CanInteract())
				return;

			previousInteractable?.ResetInteraction();
			currentInteractable.Interact(gameObject, context);
			previousInteractable = currentInteractable;
		}
	}
}