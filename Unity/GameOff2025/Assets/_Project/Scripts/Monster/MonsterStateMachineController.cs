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
		public NavMeshAgent agent;
		public NoiseEmitter loudestEmitter;
		public float movementSpeed;
		public float checkNoiseInterval;
		public float forceIdleTime;
		public bool enraged;
		public bool hasLOS;
	}
}