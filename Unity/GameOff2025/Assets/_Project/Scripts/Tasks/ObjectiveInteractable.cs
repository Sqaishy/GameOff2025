using SubHorror.Interaction;
using UnityEngine;

namespace SubHorror.Tasks
{
	public class ObjectiveInteractable : MonoBehaviour, IInteractable
	{
		[SerializeField] private Objective objective;

		public bool CanInteract()
		{
			return true;
		}

		public void Interact(GameObject interactor, InteractorContext context)
		{

		}

		public void ResetInteraction()
		{

		}
	}
}