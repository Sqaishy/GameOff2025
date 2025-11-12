using System.Collections;
using SubHorror.Noise;
using SubHorror.States;
using UnityEngine;

namespace SubHorror.Monster
{
	public class Rage : State
	{
		private MonsterContext context;
		private NoiseEmitter previousEmitter;
		private bool isFarEnough;
		private bool canLeaveRage;
		private float minRageTime;
		private float currentRageTime;
		private float exceedDistanceTime;

		public Rage(StateMachine machine, State parent, MonsterContext context) : base(machine, parent)
		{
			this.context = context;
		}

		protected override bool GetTransition(out State transitionState)
		{
			if (canLeaveRage)
			{
				transitionState = Machine.Factory.GetState<MonsterIdle>();
				return true;
			}

			transitionState = null;
			return false;
		}

		protected override void OnEnter()
		{
			context.enraged = true;
			previousEmitter = context.loudestEmitter;
			isFarEnough = false;
			context.agent.isStopped = false;

			float distanceToEmitter = Vector3.Distance(context.loudestEmitter.transform.position,
				context.agent.transform.position);

			minRageTime = Mathf.Log(distanceToEmitter) + context.minRageTime;
		}

		protected override void OnTick()
		{
			RecalculatePath();
			SwitchEmitter();

			currentRageTime += Time.deltaTime;

			if (currentRageTime < minRageTime)
				return;

			CheckExitConditions();
		}

		private void RecalculatePath()
		{
			if (!previousEmitter)
				return;

			context.agent.SetDestination(previousEmitter.transform.position);
			context.agent.speed = context.rageMovementSpeed;
		}

		private void SwitchEmitter()
		{
			if (previousEmitter == context.loudestEmitter)
				return;

			previousEmitter = context.loudestEmitter;
		}

		private void CheckExitConditions()
		{
			float distanceToEmitter = Vector3.Distance(context.loudestEmitter.transform.position,
				context.agent.transform.position);

			isFarEnough = distanceToEmitter >= context.exitRageDistance;

			if (!isFarEnough)
				return;

			exceedDistanceTime += Time.deltaTime;

			if (exceedDistanceTime >= context.rageEscapeTime)
				canLeaveRage = true;
		}

		protected override void OnExit()
		{
			canLeaveRage = false;
			currentRageTime = 0;
			context.agent.GetComponent<MonoBehaviour>().StartCoroutine(RageCooldown());
		}

		private IEnumerator RageCooldown()
		{
			yield return new WaitForSeconds(context.rageCooldown);

			context.enraged = false;
		}
	}
}