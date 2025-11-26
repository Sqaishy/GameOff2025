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
		private Interactor interactor;
		private bool sprintToggle;

		private void Awake()
		{
			playerContext = GetComponent<PlayerStateMachineController>().PlayerContext;
			interactor = GetComponentInChildren<Interactor>();

			DisableCursor();
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
			if (!context.performed)
				return;

			interactor.Interact();
		}

		public static void EnableCursor()
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		public static void DisableCursor()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}