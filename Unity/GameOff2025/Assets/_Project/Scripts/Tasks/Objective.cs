using System;
using System.Data;
using UnityEngine;

namespace SubHorror.Tasks
{
	public abstract class Objective : ScriptableObject
	{
		[Header("Base Settings")]
		[Tooltip("If use timer is true the objective will be timed")]
		[SerializeField] protected bool useTimer;
		[Tooltip("The timed duration of this objective in seconds")]
		[Min(0f)]
		[SerializeField] protected float timerDuration;
		[Tooltip("A description of the objective to show in UI")]
		[SerializeField] private string objectiveText;

		public virtual object ObjectiveDataType => objectiveDataType;
		/// <summary>
		/// The GameObject that currently owns this objective
		/// </summary>
		public GameObject Owner { get; set; }

		protected float currentTime;

		private object objectiveDataType = null;

		public abstract Task.Status Enter();
		public abstract Task.Status Process();
		public abstract void Exit();

		public virtual void ResetObjective()
		{
			currentTime = 0;
		}

		public virtual void OverrideDataType(object newDataType) => objectiveDataType = newDataType;

		/// <returns>
		/// The remaining time in seconds or -1 if this objective does not use a timer
		/// </returns>
		public float GetObjectiveRemainingTime() => useTimer ? timerDuration - currentTime : -1f;

		public virtual string DisplayObjectiveText() => objectiveText;

		private void OnValidate()
		{
			useTimer = timerDuration > 0;
		}
	}

	public abstract class Objective<T> : Objective where T : ObjectiveData
	{
		[Header("Custom Data")]
		[SerializeField] protected T objectiveData;

		public override object ObjectiveDataType => objectiveData;

		public override void OverrideDataType(object newDataType)
		{
			if (newDataType is not T castedType)
				throw new ConstraintException($"{nameof(newDataType)} {newDataType.GetType()} does not match the type of {typeof(T)}");

			objectiveData = castedType;
		}
	}

	[Serializable]
	public abstract class ObjectiveData
	{
		public abstract ObjectiveData Clone();
	}
}