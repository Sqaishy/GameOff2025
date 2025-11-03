using System.Collections.Generic;
using System.Reflection;

namespace SubHorror
{
	public class StateMachineBuilder
	{
		private State root;

		private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
			| BindingFlags.FlattenHierarchy;

		public StateMachineBuilder(State root)
		{
			this.root = root;
		}

		public StateMachine Build()
		{
			StateMachine machine = new StateMachine(root);
			Wire(root, machine, new HashSet<State>());
			machine.Start();
			return machine;
		}

		private void Wire(State s, StateMachine m, HashSet<State> visited)
		{
			if (s == null)
				return;

			if (!visited.Add(s))
				return;

			PropertyInfo machineField = typeof(State).GetProperty("Machine", Flags);
			machineField?.SetValue(s, m);

			foreach (FieldInfo field in s.GetType().GetFields(Flags))
			{
				if (!typeof(State).IsAssignableFrom(field.FieldType))
					continue;

				if (field.Name == "Parent")
					continue;

				State child = (State)field.GetValue(s);

				if (child == null)
					continue;

				if (!ReferenceEquals(child.Parent, s))
					continue;

				Wire(child, m, visited);
			}
		}
	}
}