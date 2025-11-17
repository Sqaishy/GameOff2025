using System.Collections.Generic;
using SubHorror.Depth;
using UnityEngine;

namespace SubHorror.Tasks
{
	public class TaskManager : MonoBehaviour
	{
		[SerializeField] private TaskHandler taskHandler;
		[Tooltip("A list of all possible tasks that the player can have")]
		[SerializeField] private List<Task> allTasks;

		private List<Task> possibleTasks;

		private void OnEnable()
		{
			DepthController.OnDepthMilestone += GiveTask;
		}

		private void OnDisable()
		{
			DepthController.OnDepthMilestone -= GiveTask;
		}

		private Task GetRandomTask()
		{
			possibleTasks = new List<Task>(allTasks);

			foreach (Task activeTask in taskHandler.ActiveTasks)
				possibleTasks.Remove(activeTask);

			return possibleTasks[Random.Range(0, possibleTasks.Count)];
		}

		private void GiveTask()
		{
			taskHandler.AddTask((Task)GetRandomTask().Clone());
		}
	}
}