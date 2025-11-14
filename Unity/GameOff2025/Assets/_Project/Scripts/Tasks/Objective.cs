using UnityEngine;

namespace SubHorror.Tasks
{
	public abstract class Objective : ScriptableObject
	{
		[Header("Base Settings")]
		[SerializeField] protected bool useTimer;
		[SerializeField] protected float timerDuration;
		[SerializeField] private string objectiveText;

		protected float currentTime;

		public abstract Task.Status Start();
		public abstract Task.Status Process();
		public abstract void Exit();

		public virtual void ResetObjective()
		{
			currentTime = 0;
		}

		public virtual string DisplayObjectiveText() => objectiveText;
	}

	public abstract class Objective<T> : Objective
	{
		[Header("Custom Data")]
		[SerializeField] protected T objectiveData;

		public void OverrideObjectiveData(T newObjectiveData) => objectiveData = newObjectiveData;
	}
}