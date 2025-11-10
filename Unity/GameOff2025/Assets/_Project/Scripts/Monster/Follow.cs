using SubHorror.Noise;
using SubHorror.States;

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

			if (previousEmitter == context.loudestEmitter)
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
		}
	}
}