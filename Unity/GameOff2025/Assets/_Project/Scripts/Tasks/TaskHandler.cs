using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubHorror.Tasks
{
	public class TaskHandler : MonoBehaviour
	{
		public event Action OnObjectiveChanged;

		private List<Task> activeTasks = new();

		private void Update()
		{
			if (activeTasks.Count == 0)
				return;

			for (int i = activeTasks.Count - 1; i >= 0; i--)
			{
				Task task = activeTasks[i];

				Task.Status status = task.Process();

				Debug.Log($"{task.TaskName}: {status}");

				if (status is Task.Status.Success or Task.Status.Failure)
				{
					RemoveTask(task);
				}
			}
		}

		public void AddTask(Task newTask)
		{
			activeTasks.Add(newTask);
			newTask.Enter(gameObject);
			newTask.OnTaskUpdated += OnTaskUpdated;
		}

		private void RemoveTask(Task task)
		{
			activeTasks.Remove(task);
			task.Exit();
			task.OnTaskUpdated -= OnTaskUpdated;
		}

		private void OnTaskUpdated()
		{
			OnObjectiveChanged?.Invoke();
		}
	}
}