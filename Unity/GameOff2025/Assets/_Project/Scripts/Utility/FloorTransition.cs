using SubHorror.Interaction;
using UnityEngine;

public class FloorTransition : MonoBehaviour, IInteractable
{
	[SerializeField] private InteractorContext interactorContext;
	[SerializeField] private Transform destination;

	public bool CanInteract()
	{
		return true;
	}

	public void Interact(GameObject interactor, InteractorContext context)
	{
		if (context != interactorContext)
			return;

		TeleportToFloor(interactor);
	}

	public void ResetInteraction()
	{

	}

	private void TeleportToFloor(GameObject interactor)
	{
		interactor.transform.root.position = destination.transform.position;
	}
}