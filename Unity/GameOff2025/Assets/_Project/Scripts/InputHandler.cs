using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SubHorror
{
	[RequireComponent(typeof(PlayerInput))]
	[DisallowMultipleComponent]
	public class InputHandler : MonoBehaviour
	{
		private PlayerContext playerContext;

		private void Awake()
		{
			playerContext = GetComponent<PlayerStateMachineController>().PlayerContext;
		}

		public void MovementInput(InputAction.CallbackContext context)
		{
			playerContext.movement = context.ReadValue<Vector2>();
		}

		public void JumpInput(InputAction.CallbackContext context)
		{
			playerContext.jumpPressed = context.performed;
		}
	}
}