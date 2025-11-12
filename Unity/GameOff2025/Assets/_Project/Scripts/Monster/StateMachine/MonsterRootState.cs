using SubHorror.Noise;
using SubHorror.States;
using UnityEngine;

namespace SubHorror.Monster
{
	public class MonsterRootState : State
	{
		private MonsterContext context;
		private Follow follow;
		private float checkNoiseTime;
		private float idleTime;
		private bool canForceIdle;

		public MonsterRootState(StateMachine machine, MonsterContext context) : base(machine)
		{
			this.context = context;
			follow = machine.Factory.GetOrAdd(new Follow(machine, this, context));
			machine.Factory.GetOrAdd(new MonsterIdle(machine, this, context));
			machine.Factory.GetOrAdd(new Rage(machine, this, context));
			checkNoiseTime = context.checkNoiseInterval;
		}

		protected override State GetInitialState() => follow;

		protected override bool GetTransition(out State transitionState)
		{
			if (canForceIdle)
			{
				canForceIdle = false;
				context.agent.isStopped = true;

				transitionState = Machine.Factory.GetState<MonsterIdle>();
				return true;
			}

			if (context.loudestNoiseLevel > context.gameDifficulty.MaxNoiseThreshold && !context.enraged)
			{
				transitionState = Machine.Factory.GetState<Rage>();
				return true;
			}

			transitionState = null;
			return false;
		}

		protected override void OnTick()
		{
			CheckLoudestNoise();
			canForceIdle = CanForceIdle();
		}

		protected override void OnExit()
		{
			canForceIdle = false;
			idleTime = 0f;
		}

		private void CheckLoudestNoise()
		{
			checkNoiseTime += Time.deltaTime;

			if (checkNoiseTime < context.checkNoiseInterval)
				return;

			context.loudestEmitter = NoiseEmitter.GetLoudestNoiseEmitter();
			if (context.loudestEmitter)
				context.loudestNoiseLevel = context.loudestEmitter.TotalNoiseLevelCombined();
			checkNoiseTime = 0;
		}

		private bool CanForceIdle()
		{
			idleTime += Time.deltaTime;

			if (idleTime < context.forceIdleTime)
				return false;

			if (context.enraged || HasLineOfSight())
				return false;

			idleTime = 0;
			return true;
		}

		private bool HasLineOfSight()
		{
			if (!context.loudestEmitter)
				return false;

			Vector3 rayOrigin = context.agent.transform.position;
			Vector3 rayDirection = context.loudestEmitter.transform.position - rayOrigin;

			Ray ray = new Ray(rayOrigin, rayDirection);

			Debug.DrawRay(rayOrigin, rayDirection, Color.red);

			if (Physics.Raycast(ray, out RaycastHit hit))
				context.hasLOS = hit.collider.gameObject == context.loudestEmitter.gameObject;

			return context.hasLOS;
		}
	}
}