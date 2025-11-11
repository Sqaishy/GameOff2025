using SubHorror.Noise;
using SubHorror.States;
using UnityEngine;

namespace SubHorror.Monster
{
	public class Follow : State
	{
		private MonsterContext context;
		private NoiseEmitter previousEmitter;

		public Follow(StateMachine machine, State parent, MonsterContext context) : base(machine, parent)
		{
			this.context = context;
		}

		protected override void OnEnter()
		{
			previousEmitter = context.loudestEmitter;
			context.agent.isStopped = false;
		}

		protected override void OnTick()
		{
			RecalculatePath();

			if (previousEmitter && previousEmitter == context.loudestEmitter)
				return;

			previousEmitter = context.loudestEmitter;
		}

		protected override void OnExit()
		{
			context.agent.isStopped = true;
		}

		private void RecalculatePath()
		{
			if (!previousEmitter)
				return;

			context.agent.SetDestination(previousEmitter.transform.position);

			float noiseLevel = previousEmitter.TotalNoiseLevelCombined();
			//TODO Replace the 'noiseLevel / 100f' by a difficulty value at some point for more control
			float movementRamp = context.movementSpeedRamp.Evaluate(noiseLevel / 100f);

			float speedMultiplier = Mathf.Lerp(context.minSpeedMultiplier, context.maxSpeedMultiplier,
				movementRamp);
			context.agent.speed = context.movementSpeed * speedMultiplier;
		}
	}
}