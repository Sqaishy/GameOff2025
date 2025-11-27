using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
		[FormerlySerializedAs("successAction")]
		[Tooltip("On task success perform these actions if any are defined")]
		[SerializeField] private List<Consequence> successActions;

		public event Action<Task> OnTaskUpdated;
		public string TaskName => taskName;
		public Objective CurrentObjective => objectives[currentObjectiveIndex];
		public GameObject TaskOwner => taskOwner;

		private int currentObjectiveIndex;
		/// <summary>
		/// The GameObject that currently owns this task
		/// </summary>
		private GameObject taskOwner;
		private Status currentChildStatus;

		public Status Enter(GameObject owner)
		{
			taskOwner = owner;

			foreach (Objective objective in objectives)
				objective.ResetObjective();

			if (consequenceApplication == ConsequenceApplication.Start)
			{
				//Apply the consequence that is defined
				consequence.Apply(this);
			}

			return StartObjective();
		}

		public Status Process()
		{
			currentChildStatus = objectives[currentObjectiveIndex].Process();

			if (currentChildStatus != Status.Success)
				return currentChildStatus;

			if (currentObjectiveIndex + 1 >= objectives.Count)
				return Status.Success;

			return IncrementObjective();
		}

		public void Exit()
		{
			objectives[currentObjectiveIndex].Exit();

			if (consequenceApplication == ConsequenceApplication.End && currentChildStatus == Status.Failure)
			{
				//Apply the consequence that is defined
				consequence.Apply(this);
			}

			if (currentChildStatus != Status.Success)
				return;

			foreach (Consequence success in successActions)
				success.Apply(this);
		}

		private Status StartObjective()
		{
			objectives[currentObjectiveIndex].Owner = taskOwner;
			Status childStatus = objectives[currentObjectiveIndex].Enter();

			OnTaskUpdated?.Invoke(this);

			return childStatus switch
			{
				Status.Success => currentObjectiveIndex >= objectives.Count - 1 ? Status.Success : IncrementObjective(),
				_ => childStatus
			};
		}

		private Status IncrementObjective()
		{
			objectives[currentObjectiveIndex].Exit();
			currentObjectiveIndex++;
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