using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubHorror.Tasks
{
	public class TaskUI : MonoBehaviour
	{
		[SerializeField] private TaskHandler taskHandler;
		[SerializeField] private Transform tasksParent;
		[SerializeField] private TaskElementUI taskElementPrefab;

		private Dictionary<Task, TaskElementUI> taskUIElements = new();

		private void OnEnable()
		{
			taskHandler.OnObjectiveChanged += ObjectiveChanged;
			taskHandler.OnTaskCompleted += TaskComplete;
		}

		private void OnDisable()
		{
			taskHandler.OnObjectiveChanged -= ObjectiveChanged;
			taskHandler.OnTaskCompleted -= TaskComplete;
		}

		private void ObjectiveChanged(Task task)
		{
			if (!taskUIElements.TryGetValue(task, out TaskElementUI element))
			{
				element = Instantiate(taskElementPrefab, tasksParent);
				taskUIElements.Add(task, element);
			}

			element.Initialize(task);
		}

		private void TaskComplete(Task task)
		{
			if (!taskUIElements.Remove(task, out TaskElementUI element))
				return;

			Destroy(element.gameObject);
		}
	}
}