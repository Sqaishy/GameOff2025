using System;
using UnityEngine;

namespace SubHorror.Tasks
{
	public abstract class Objective : ScriptableObject
	{
		[Header("Base Settings")]
		[SerializeField] protected bool useTimer;
		[SerializeField] protected float timerDuration;
		[SerializeField] private string objectiveText;

		public virtual object ObjectiveDataType => null;
		/// <summary>
		/// The GameObject that currently owns this objective
		/// </summary>
		public GameObject Owner { get; set; }
		protected float currentTime;

		public abstract Task.Status Enter();
		public abstract Task.Status Process();
		public abstract void Exit();

		public virtual void ResetObjective()
		{
			currentTime = 0;
		}

		public virtual string DisplayObjectiveText() => objectiveText;
	}

	public abstract class Objective<T> : Objective where T : ObjectiveData
	{
		[Header("Custom Data")]
		[SerializeField] protected T objectiveData;

		public override object ObjectiveDataType => objectiveData;
	}

	[Serializable]
	public class ObjectiveData { }
}