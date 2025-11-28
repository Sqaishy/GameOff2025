using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SubHorror.States
{
	public class StateMachine
	{
		public State Root { get; private set; }
		public StateFactory Factory { get; private set; } = new();

		public void Start()
		{
			Root.Enter();
		}

		public void Update()
		{
			Root?.Tick();
		}

		public void Exit()
		{
			Root?.Exit();
		}

		public void RequestTransition(State from, State to)
		{
			if (from == to || from == null || to == null)
				return;

			if (from.Parent is not null && from.Parent != to.Parent)
				to.Parent = from.Parent;

			State commonAncestor = LowestCommonAncestor(from, to);

			foreach (State current in from.PathTo(commonAncestor))
				current.Exit();

			Stack<State> stateStack = new Stack<State>();
			foreach (State current in to.PathTo(commonAncestor))
				stateStack.Push(current);

			while (stateStack.Count > 0)
				stateStack.Pop().Enter();
		}

		public static State LowestCommonAncestor(State a, State b)
		{
			HashSet<State> aParents = new HashSet<State>();

			for (State current = a; current is not null; current = current.Parent)
				aParents.Add(current);

			for (State current = b; current is not null; current = current.Parent)
				if (aParents.Contains(current)) return current;

			return null;
		}

		public void SetRoot(State root)
		{
			Root = root;
		}
	}
}