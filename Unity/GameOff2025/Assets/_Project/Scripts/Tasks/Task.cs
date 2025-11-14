using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Task")]
	public class Task : ScriptableObject, ICloneable
	{
		[SerializeField] private string taskName;
		[SerializeField] private List<Objective> objectives = new();

		public event Action OnTaskUpdated;
		public string TaskName => taskName;

		private int currentObjective;
		/// <summary>
		/// The GameObject that currently owns this task
		/// </summary>
		private GameObject taskOwner;

		public Status Enter(GameObject owner)
		{
			taskOwner = owner;
			return StartObjective();
		}

		public Status Process()
		{
			Status childStatus = objectives[currentObjective].Process();

			if (childStatus != Status.Success)
				return childStatus;

			if (currentObjective + 1 >= objectives.Count)
				return Status.Success;

			return IncrementObjective();
		}

		public void Exit()
		{
			objectives[currentObjective].Exit();
		}

		private Status StartObjective()
		{
			objectives[currentObjective].Owner = taskOwner;
			Status childStatus = objectives[currentObjective].Enter();

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