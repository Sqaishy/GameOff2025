using System;
using FMODUnity;
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
		[SerializeField] private EventReference escapeAudio;

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

			RuntimeManager.PlayOneShotAttached(escapeAudio, gameObject);

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