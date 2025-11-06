using System.Collections.Generic;
using SubHorror.Noise;
using UnityEngine;

namespace SubHorror.States
{
	public abstract class State
	{
		public StateMachine Machine { get; private set; }
		public State Parent { get; set; }
		public State ActiveChild { get; set; }

		public State(StateMachine machine, State parent = null)
		{
			Machine = machine;
			Parent = parent;
		}

		public void Enter()
		{
			if (Parent is not null)
				Parent.ActiveChild = this;

			OnEnter();

			State child = GetInitialState();
			child?.Enter();
		}

		public void Tick()
		{
			if (GetTransition(out State transitionState))
			{
				Machine.RequestTransition(this, transitionState);
				return;
			}

			ActiveChild?.Tick();
			OnTick();
		}

		public void Exit()
		{
			ActiveChild?.Exit();
			ActiveChild = null;

			OnExit();
		}

		public State Leaf()
		{
			State current = this;

			while (current.ActiveChild is not null)
				current = current.ActiveChild;

			return current;
		}

		public State GetLowestActiveParent()
		{
			return Leaf().Parent;
		}

		public IEnumerable<State> PathToRoot()
		{
			for (State current = this; current is not null; current = current.Parent)
				yield return current;
		}

		public IEnumerable<State> PathToActiveChild()
		{
			for (State current = this; current is not null; current = current.ActiveChild)
				yield return current;
		}

		public IEnumerable<State> PathTo(State target)
		{
			for (State current = this; current != target; current = current.Parent)
				yield return current;
		}

		protected virtual State GetInitialState() => null;
		protected virtual bool GetTransition(out State transitionState)
		{
			transitionState = null;
			return false;
		}
		protected virtual void OnEnter() { }
		protected virtual void OnTick() { }
		protected virtual void OnExit() { }

		public override string ToString() => GetType().Name;
	}

	public class PlayerRoot : State
	{
		public Grounded Grounded { get; private set; }
		public Airborne Airborne { get; private set; }
		public PlayerContext Context { get; private set; }

		public PlayerRoot(StateMachine machine, PlayerContext context) : base(machine)
		{
			Context = context;
			Grounded = Machine.Factory.GetOrAdd(new Grounded(machine, this, context));
			Airborne = Machine.Factory.GetOrAdd(new Airborne(machine, this, context));
		}

		protected override State GetInitialState() => Grounded;
		protected override bool GetTransition(out State transitionState)
		{
			if (Context.isGrounded)
			{
				transitionState = null;
				return false;
			}

			if (Context.isAirborne)
			{
				transitionState = null;
				return false;
			}

			transitionState = Airborne;
			return true;
		}
	}

	public class Idle : State
	{
		private PlayerContext context;

		public Idle(StateMachine machine, State parent, PlayerContext context) : base(machine, parent)
		{
			this.context = context;
		}

		protected override bool GetTransition(out State transitionState)
		{
			if (Mathf.Abs(context.movement.x) < .01f && Mathf.Abs(context.movement.y) < .01f)
			{
				transitionState = null;
				return false;
			}

			transitionState = Machine.Factory.GetState<Movement>();
			return true;
		}
	}

	public class Movement : State
	{
		private PlayerContext context;
		private Vector3 movementDirection;

		public Movement(StateMachine machine, State parent, PlayerContext context) : base(machine, parent)
		{
			this.context = context;
		}

		protected override bool GetTransition(out State transitionState)
		{
			if (Mathf.Abs(context.movement.x) > .01f || Mathf.Abs(context.movement.y) > .01f)
			{
				transitionState = null;
				return false;
			}

			transitionState = Machine.Factory.GetState<Idle>();
			return true;
		}

		protected override void OnEnter()
		{
			/*NoiseSettings modifiedNoise = context.movementNoiseSettings.NoiseModifier.GetModifiedValue(
				new MovementNoiseModifier(context.sprintPressed));*/

			context.noiseEmitter.PlayNoise(context.movementNoiseSettings);
			context.animator.SetBool("IsMoving", true);
		}

		protected override void OnTick()
		{
			//TODO Movement speed is a fixed value, change this to a variable at some point
			movementDirection = MovementDirection();
			movementDirection *= context.sprintPressed ? 8f : 5f;
			movementDirection.y = context.rigidbody.linearVelocity.y;

			context.rigidbody.linearVelocity = movementDirection;
		}

		protected override void OnExit()
		{
			context.animator.SetBool("IsMoving", false);
		}

		private Vector3 MovementDirection()
		{
			Vector3 cameraForward = (context.mainCamera.transform.forward).normalized;
			Vector3 cameraRight = (context.mainCamera.transform.right).normalized;

			return cameraForward * context.movement.y + cameraRight * context.movement.x;
		}
	}

	public class Grounded : State
	{
		public Idle Idle { get; private set; }
		public Movement Movement { get; private set; }

		private PlayerContext context;

		public Grounded(StateMachine machine, State parent, PlayerContext context) : base(machine, parent)
		{
			this.context = context;

			Idle = machine.Factory.GetOrAdd(new Idle(machine, this, context));
			Movement = machine.Factory.GetOrAdd(new Movement(machine, this, context));
		}

		protected override State GetInitialState()
		{
			Idle.Parent = this;
			return Idle;
		}

		protected override void OnTick()
		{
			if (context.jumpPressed)
			{
				context.jumpPressed = false;
				Vector3 velocity = context.rigidbody.linearVelocity;
				velocity.y = 12f;
				context.rigidbody.linearVelocity = velocity;
			}
		}
	}

	public class Airborne : State
	{
		public Idle Idle { get; private set; }
		public Movement Movement { get; private set; }

		private PlayerContext context;

		public Airborne(StateMachine machine, State parent, PlayerContext context) : base(machine, parent)
		{
			this.context = context;

			Idle = machine.Factory.GetOrAdd(new Idle(machine, this, context));
			Movement = machine.Factory.GetOrAdd(new Movement(machine, this, context));
		}

		protected override State GetInitialState()
		{
			Idle.Parent = this;
			return Idle;
		}

		protected override bool GetTransition(out State transitionState)
		{
			if (!context.isGrounded)
			{
				transitionState = null;
				return false;
			}

			transitionState = Machine.Factory.GetState<Grounded>();
			return true;
		}

		protected override void OnEnter()
		{
			context.isAirborne = true;
		}

		protected override void OnExit()
		{
			context.isAirborne = false;
		}
	}
}