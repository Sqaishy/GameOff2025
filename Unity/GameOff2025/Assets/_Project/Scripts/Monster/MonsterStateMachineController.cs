using System;
using SubHorror.Noise;
using SubHorror.States;
using UnityEngine;
using UnityEngine.AI;

namespace SubHorror.Monster
{
	public class MonsterStateMachineController : MonoBehaviour
	{
		[SerializeField] private MonsterContext monsterContext;
		private StateMachine machine;

		private void Awake()
		{
			monsterContext.agent = GetComponent<NavMeshAgent>();

			machine = new StateMachineBuilder<MonsterRootState>(monsterContext).Build();
		}

		private void Update()
		{
			machine.Update();

			Debug.Log(string.Join(" > ", machine.Root.PathToActiveChild()));
		}
	}

	[Serializable]
	public class MonsterContext : IStateContext
	{
		[Header("References")]
		public NavMeshAgent agent;
		public NoiseEmitter loudestEmitter;
		[Header("Movement")]
		public float movementSpeed;
		[Tooltip("The minimum movement speed multiplier of this monster based on the noise level" +
		         "\nMovement Speed * Multiplier")]
		public float minSpeedMultiplier = 1f;
		[Tooltip("The maximum movement speed multiplier of this monster based on the noise level" +
		         "\nMovement Speed * Multiplier")]
		public float maxSpeedMultiplier;
		[Tooltip("Controls how quickly the movement speed ramps between min and max multiplier")]
		public AnimationCurve movementSpeedRamp;
		[Header("Extras")]
		[Tooltip("How often the monster checks for the loudest noise in seconds")]
		public float checkNoiseInterval;
		public float forceIdleTime;
		[Header("Values")]
		public bool enraged;
		public bool hasLOS;
	}
}