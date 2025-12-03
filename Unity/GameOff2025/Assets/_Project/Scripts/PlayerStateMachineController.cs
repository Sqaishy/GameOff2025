using System;
using SubHorror.Noise;
using SubHorror.States;
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
			playerContext.noiseEmitter = GetComponentInChildren<NoiseEmitter>();
			playerContext.animator = GetComponentInChildren<Animator>();
			playerContext.mainCamera = Camera.main;

			machine = new StateMachineBuilder<PlayerRoot>(playerContext).Build();
		}

		private void Update()
		{
			playerContext.isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

			machine.Update();
		}
	}

	[Serializable]
	public class PlayerContext : IStateContext
	{
		[Header("References")]
		public Camera mainCamera;
		public Rigidbody rigidbody;
		public Animator animator;
		public NoiseEmitter noiseEmitter;
		[Header("Movement")]
		public PlayerMovementSettings movementSettings;
		[Header("Values")]
		public bool isGrounded;
		public bool isAirborne;
        public bool sprintPressed;
		public Vector2 movement;
	}

	public interface IStateContext { }

	[Serializable]
	public struct PlayerMovementSettings
	{
		public float walkSpeed;
		public float sprintSpeed;
		public NoiseSettings movementNoiseSettings;
		public float sprintNoiseMultiplier;
		public float rotationSpeed;
		public float jumpSpeed;
		public NoiseSettings jumpNoiseSettings;
		public NoiseSettings landingNoiseSettings;
	}
}