using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SubHorror
{
	public class StateMachine
	{
		public State Root { get; private set; }

		public StateMachine(State root)
		{
			Root = root;
		}

		public void Start()
		{
			Root.Enter();
		}

		public void Update()
		{
			Root?.Tick();
		}

		public void RequestTransition(State from, State to)
		{
			if (from == to || from == null || to == null)
				return;

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
	}
}