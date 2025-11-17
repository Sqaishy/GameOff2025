using System;
using TMPro;
using UnityEngine;

namespace SubHorror.Tasks
{
	public class TaskElementUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text taskName;
		[SerializeField] private TMP_Text objectiveText;

		private Task activeTask;

		private void Update()
		{
			objectiveText.text = activeTask.CurrentObjective.DisplayObjectiveText();
		}

		public void Initialize(Task task)
		{
			activeTask = task;
			taskName.text = activeTask.TaskName;
		}
	}
}