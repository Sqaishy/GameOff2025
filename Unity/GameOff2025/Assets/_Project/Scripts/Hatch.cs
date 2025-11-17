using System;
using SubHorror.Depth;
using SubHorror.Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace SubHorror
{
	public class Hatch : MonoBehaviour, IInteractable
	{
		[SerializeField] private UnityEvent onHatchUnlocked;
		[SerializeField] private UnityEvent onHatchInteracted;

		private bool hatchUnlocked;

		private void OnEnable()
		{
			DepthController.OnReachedSurface += UnlockHatch;
		}

		private void OnDisable()
		{
			DepthController.OnReachedSurface -= UnlockHatch;
		}

		public bool CanInteract()
		{
			return hatchUnlocked;
		}

		public void Interact(GameObject interactor, InteractorContext context)
		{
			onHatchInteracted?.Invoke();
		}

		public void ResetInteraction()
		{

		}

		private void UnlockHatch()
		{
			hatchUnlocked = true;

			onHatchUnlocked?.Invoke();
		}
	}
}