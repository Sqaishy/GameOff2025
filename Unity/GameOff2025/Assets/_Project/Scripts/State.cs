using System.Collections.Generic;
using UnityEngine;

namespace SubHorror
{
	public abstract class State
	{
		public StateMachine Machine { get; private set; }
		public State Parent { get; private set; }
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
			Grounded = new Grounded(machine, this, context);
			Airborne = new Airborne(machine, this, context);
		}

		protected override State GetInitialState() => Grounded;
		protected override bool GetTransition(out State transitionState)
		{
			if (Context.isGrounded)
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
			if (Mathf.Abs(context.movement.x) < .01f)
			{
				transitionState = null;
				return false;
			}

			transitionState = ((Grounded)Parent).Movement;
			return true;
		}
	}

	public class Movement : State
	{
		private PlayerContext context;

		public Movement(StateMachine machine, State parent, PlayerContext context) : base(machine, parent)
		{
			this.context = context;
		}

		protected override bool GetTransition(out State transitionState)
		{
			if (Mathf.Abs(context.movement.x) > .01f)
			{
				transitionState = null;
				return false;
			}

			transitionState = ((Grounded)Parent).Idle;
			return true;
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

			Idle = new Idle(machine, this, context);
			Movement = new Movement(machine, this, context);
		}

		protected override State GetInitialState() => Idle;
	}

	public class Airborne : State
	{
		private PlayerContext context;

		public Airborne(StateMachine machine, State parent, PlayerContext context) : base(machine, parent)
		{
			this.context = context;
		}

		protected override bool GetTransition(out State transitionState)
		{
			if (!context.isGrounded)
			{
				transitionState = null;
				return false;
			}

			transitionState = ((PlayerRoot)Parent).Grounded;
			return true;
		}
	}
}