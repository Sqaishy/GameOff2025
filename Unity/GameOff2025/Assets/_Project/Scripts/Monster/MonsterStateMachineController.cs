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
		[Header("Rage Settings")]
		public float rageMovementSpeed;
		[Tooltip("How far object the monster is chasing has to be before the monster can exit rage")]
		public float exitRageDistance;
		[Tooltip("When exiting rage start a cooldown to prevent the monster from re-entering rage " +
		         "straight away")]
		public float rageCooldown;
		[Tooltip("The minimum time the monster has to be in rage before it can exit")]
		public float minRageTime;
		[Tooltip("How long the noise emitter has to be outside of the rage distance to escape")]
		public float rageEscapeTime;
		[Header("Extras")]
		public GameDifficulty gameDifficulty;
		[Tooltip("How often the monster checks for the loudest noise in seconds")]
		public float checkNoiseInterval;
		public float loudestNoiseLevel;
		public float forceIdleTime;
		[Header("Values")]
		public bool enraged;
		public bool hasLOS;
	}
}