using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Log Consequence")]
	public class LogConsequence : Consequence
	{
		public override void Apply(Task failedTask)
		{
			Debug.Log($"Applied consequence from {failedTask.TaskName}");
		}
	}
}