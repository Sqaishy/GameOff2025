using System;
using System.Collections.Generic;

namespace SubHorror
{
	public class StateFactory
	{
		private Dictionary<Type, State> states = new();

		public T GetOrAdd<T>(T state) where T : State
		{
			if (states.ContainsKey(typeof(T)))
				return GetState<T>();

			states[typeof(T)] = state;

			return GetState<T>();
		}

		public T GetState<T>() where T : State
		{
			return GetState(typeof(T)) as T;
		}

		public State GetState(Type type)
		{
			return states[type];
		}
	}
}