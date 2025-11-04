using System;
using UnityEngine;

namespace SubHorror
{
	public class PlayerStateMachineController : MonoBehaviour
	{
		[SerializeField] private Transform groundCheck;
		[SerializeField] private float groundCheckRadius;
		[SerializeField] private LayerMask groundMask;
		[SerializeField] private PlayerContext playerContext = new();
		private StateMachine machine;

		public PlayerContext PlayerContext => playerContext;

		private void Awake()
		{
			playerContext.rigidbody = GetComponent<Rigidbody>();
			playerContext.mainCamera = Camera.main;

			machine = new StateMachineBuilder<PlayerRoot>(playerContext).Build();
		}

		private void Update()
		{
			playerContext.isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

			machine.Update();

			//Debug.Log(string.Join(" > ", machine.Root.PathToActiveChild()));
		}
	}

	[Serializable]
	public class PlayerContext
	{
		public Camera mainCamera;
		public Rigidbody rigidbody;
		public bool isGrounded;
		public bool isAirborne;
		public bool jumpPressed;
        public bool sprintPressed;
		public Vector2 movement;
	}
}