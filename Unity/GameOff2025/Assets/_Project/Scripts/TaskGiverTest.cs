using System;
using SubHorror.Tasks;
using UnityEngine;

public class TaskGiverTest : MonoBehaviour
{
	[SerializeField] private Task taskToGive;
	[SerializeField] private TaskHandler taskHandler;

	private void Awake()
	{
		taskHandler.AddTask((Task)taskToGive.Clone());
	}
}
