using UnityEngine;

namespace SubHorror.Interaction
{
	public interface IInteractable
	{
		bool CanInteract();
		void Interact(GameObject interactor, InteractorContext context);
		void ResetInteraction();
	}
}