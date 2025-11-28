using System;
using System.Collections.Generic;
using SubHorror.Core;
using UnityEngine;

namespace SubHorror.Tasks
{
	public class TaskHandler : MonoBehaviour, IGameEnd
	{
		public event Action<Task> OnObjectiveChanged;
		public event Action<Task> OnTaskCompleted;
		public List<Task> ActiveTasks => activeTasks;

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
			newTask.OnTaskUpdated += OnTaskUpdated;
			newTask.Enter(gameObject);
		}

		/// <summary>
		/// Adds a task and clones it before adding to the task handler
		/// </summary>
		/// <param name="newTask"></param>
		public void AddTaskClone(Task newTask)
		{
			Task clone = (Task)newTask.Clone();
			AddTask(clone);
		}

		private void RemoveTask(Task task)
		{
			activeTasks.Remove(task);
			task.Exit();
			task.OnTaskUpdated -= OnTaskUpdated;

			OnTaskCompleted?.Invoke(task);
		}

		private void OnTaskUpdated(Task task)
		{
			OnObjectiveChanged?.Invoke(task);
		}

		public void GameEnd()
		{
			for (int i = activeTasks.Count - 1; i >= 0; i--)
				RemoveTask(activeTasks[i]);
		}
	}
}