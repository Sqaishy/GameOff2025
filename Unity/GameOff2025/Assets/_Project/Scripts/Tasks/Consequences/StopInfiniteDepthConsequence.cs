using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Depth/Stop Infinite Depth Consequence")]
	public class StopInfiniteDepthConsequence : Consequence
	{
		[SerializeField] private StartInfiniteDepthConsequence startInfiniteConsequence;
		
		public override void Apply(Task failedTask)
		{
			startInfiniteConsequence.InfiniteDepth.Stop();
		}
	}
}