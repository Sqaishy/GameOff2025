using SubHorror.Depth;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Depth/Reduce Depth Consequence")]
	public class TimedDepthConsequence : Consequence
	{
		[Tooltip("As this is a consequence you most likely want the depth to reduce. Don't need to put " +
		         "a '-' as that is done under the hood")]
		[SerializeField] private float depthPerSecond;
		[SerializeField] private float depthDuration;

		public override void Apply(Task failedTask)
		{
			Depth.Depth.AddDepth(new TimedDepth(-depthPerSecond, depthDuration));
		}
	}
}