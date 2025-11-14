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
		[Tooltip("When the task should apply the consequence" +
		         "\nStart -> When the task starts apply the consequence" +
		         "\nEnd -> Only applies the consequence if the task has failed")]
		[SerializeField] private ConsequenceApplication consequenceApplication;
		[SerializeField] private Consequence consequence;

		public event Action OnTaskUpdated;
		public string TaskName => taskName;

		private int currentObjective;
		/// <summary>
		/// The GameObject that currently owns this task
		/// </summary>
		private GameObject taskOwner;
		private Status currentChildStatus;

		public Status Enter(GameObject owner)
		{
			taskOwner = owner;

			if (consequenceApplication == ConsequenceApplication.Start)
			{
				//Apply the consequence that is defined
				consequence.Apply(this);
			}

			return StartObjective();
		}

		public Status Process()
		{
			currentChildStatus = objectives[currentObjective].Process();

			if (currentChildStatus != Status.Success)
				return currentChildStatus;

			if (currentObjective + 1 >= objectives.Count)
				return Status.Success;

			return IncrementObjective();
		}

		public void Exit()
		{
			objectives[currentObjective].Exit();

			if (consequenceApplication == ConsequenceApplication.End && currentChildStatus == Status.Failure)
			{
				//Apply the consequence that is defined
				consequence.Apply(this);
			}
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

		public enum ConsequenceApplication
		{
			/// <summary>
			/// Applies the consequence once the task has started
			/// </summary>
			Start,
			/// <summary>
			/// Only applies the consequence at the end if the task failed
			/// </summary>
			End
		}
	}
}