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

			machine = new StateMachineBuilder(new PlayerRoot(null, playerContext)).Build();
		}

		private void Update()
		{
			playerContext.isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

			machine.Update();

			Debug.Log(string.Join(" > ", machine.Root.PathToActiveChild()));
		}
	}

	[Serializable]
	public class PlayerContext
	{
		public Rigidbody rigidbody;
		public bool isGrounded;
		public bool jumpPressed;
		public Vector2 movement;
	}
}