using SubHorror.States;
using UnityEngine;

namespace SubHorror.Monster
{
	public class MonsterIdle : State
	{
		private MonsterContext context;
		private float idleTime = 5f;
		private float currentIdleTime;
		private bool hasTarget;

		public MonsterIdle(StateMachine machine, State parent, MonsterContext context) : base(machine, parent)
		{
			this.context = context;
		}

		protected override bool GetTransition(out State transitionState)
		{
			if (hasTarget)
			{
				transitionState = Machine.Factory.GetState<Follow>();
				return true;
			}

			transitionState = null;
			return false;
		}

		protected override void OnTick()
		{
			currentIdleTime += Time.deltaTime;

			if (currentIdleTime < idleTime)
				return;

			hasTarget = context.loudestEmitter;
		}

		protected override void OnExit()
		{
			currentIdleTime = 0f;
			hasTarget = false;
		}
	}
}