using System;
using SubHorror.Tasks;
using UnityEngine;

public class TaskGiverTest : MonoBehaviour
{
	[SerializeField] private Task taskToGive;
	[SerializeField] private TaskHandler taskHandler;

	private void Start()
	{
		taskHandler.AddTask((Task)taskToGive.Clone());
	}

	[ContextMenu("Give Task")]
	private void GiveTask() => taskHandler.AddTask((Task)taskToGive.Clone());
}