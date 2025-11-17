using System;
using SubHorror.Depth;
using SubHorror.Interaction;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace SubHorror
{
	public class Hatch : MonoBehaviour, IInteractable
	{
		[SerializeField] private InteractorContext interactorContext;
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
			if (context != interactorContext)
				return;

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