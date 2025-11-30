using System;
using FMODUnity;
using SubHorror.Core;
using SubHorror.Interaction;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SubHorror
{
	[RequireComponent(typeof(PlayerInput))]
	[DisallowMultipleComponent]
	public class InputHandler : MonoBehaviour, IGameEnd
	{
		[SerializeField] private EventReference interactPressAudio;

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
			RuntimeManager.PlayOneShot(interactPressAudio);
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

		public void GameEnd()
		{
			enabled = false;

			GetComponent<PlayerInput>().enabled = false;
		}
	}
}