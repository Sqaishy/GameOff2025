using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Task")]
	public class Task : ScriptableObject, ICloneable
	{
		[SerializeField] private List<Objective> objectives = new();

		public event Action OnTaskUpdated;

		private int currentObjective;

		public Status Start()
		{
			return StartObjective();
		}

		public Status Process()
		{
			Status childStatus = objectives[currentObjective].Process();

			if (childStatus != Status.Success)
				return childStatus;

			return IncrementObjective();
		}

		public void Exit()
		{
			objectives[currentObjective].Exit();
		}

		private Status StartObjective()
		{
			Status childStatus = objectives[currentObjective].Start();

			OnTaskUpdated?.Invoke();

			return childStatus switch
			{
				Status.Success => currentObjective >= objectives.Count - 1 ? Status.Success : IncrementObjective(),
				_ => childStatus
			};
		}

		private Status IncrementObjective()
		{
			objectives[currentObjective].Exit();
			currentObjective++;
			return StartObjective();
		}

		public object Clone() => Instantiate(this);

		public enum Status
		{
			Running,
			Success,
			Failure,
		}
	}
}