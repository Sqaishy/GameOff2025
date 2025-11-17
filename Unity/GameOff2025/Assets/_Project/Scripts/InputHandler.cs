using System;
using SubHorror.Interaction;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SubHorror
{
	[RequireComponent(typeof(PlayerInput))]
	[DisallowMultipleComponent]
	public class InputHandler : MonoBehaviour
	{
		private PlayerContext playerContext;
		private bool sprintToggle;

		private void Awake()
		{
			playerContext = GetComponent<PlayerStateMachineController>().PlayerContext;
		}

		public void MovementInput(InputAction.CallbackContext context)
		{
			playerContext.movement = context.ReadValue<Vector2>();
		}

		public void SprintInput(InputAction.CallbackContext context)
		{
			if (!context.performed)
				return;

			sprintToggle = !sprintToggle;

			playerContext.sprintPressed = sprintToggle;
		}

		public void JumpInput(InputAction.CallbackContext context)
		{
			playerContext.jumpPressed = context.performed;
		}

		public void InteractInput(InputAction.CallbackContext context)
		{
			GetComponentInChildren<Interactor>().Interact();
		}
	}
}