using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Depth/Stop Depth Consequence")]
	public class StopDepthConsequence : Consequence
	{
		[SerializeField] private bool stopClimb;

		public override void Apply(Task failedTask)
		{
			Depth.Depth.ToggleDepthClimb(!stopClimb);
		}
	}
}