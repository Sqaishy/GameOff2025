using System;
using FMOD.Studio;
using FMODUnity;
using SubHorror.Noise;
using SubHorror.States;
using UnityEngine;
using UnityEngine.AI;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace SubHorror.Monster
{
	public class MonsterStateMachineController : MonoBehaviour
	{
		[SerializeField] private MonsterContext monsterContext;
		private StateMachine machine;
		private EventInstance monsterSound;

		private void Awake()
		{
			monsterContext.agent = GetComponent<NavMeshAgent>();

			machine = new StateMachineBuilder<MonsterRootState>(monsterContext).Build();
			monsterSound = RuntimeManager.CreateInstance(monsterContext.monsterAudio);

			monsterSound.start();
			RuntimeManager.AttachInstanceToGameObject(monsterSound, gameObject);
		}

		private void OnDisable()
		{
			machine.Exit();
		}

		private void OnDestroy()
		{
			monsterSound.stop(STOP_MODE.IMMEDIATE);
			monsterSound.release();
			monsterContext.movementSoundInstance.stop(STOP_MODE.IMMEDIATE);
			monsterContext.movementSoundInstance.release();
		}

		private void Update()
		{
			machine.Update();
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
		[Tooltip("If the monster has 'line of sight' with the noise emitter its chasing multiply " +
		         "the movement speed" +
		         "\nThis is multiplicative with the other movement speed modifiers")]
		public float losSpeedMultiplier;
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
		public EventReference monsterAudio;
		[Tooltip("How often the monster checks for the loudest noise in seconds")]
		public float checkNoiseInterval;
		public float loudestNoiseLevel;
		public float forceIdleTime;
		[Header("Audio")]
		public EventReference movementSound;
		public EventReference rageSound;
		[Header("Values")]
		public bool enraged;
		public bool hasLOS;

		[HideInInspector]
		public EventInstance movementSoundInstance;
	}
}