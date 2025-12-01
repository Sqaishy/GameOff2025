using SubHorror.Depth;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Depth/Start Infinite Depth Consequence")]
	public class StartInfiniteDepthConsequence : Consequence
	{
		[Tooltip("As this is a consequence you most likely want the depth to reduce. Don't need to put " +
		         "a '-' as that is done under the hood")]
		[SerializeField] private float depthPerSecond;

		public InfiniteDepth InfiniteDepth { get; private set; }

		public override void Apply(Task failedTask)
		{
			InfiniteDepth ??= new InfiniteDepth(-depthPerSecond);
			InfiniteDepth.Start();

			Depth.Depth.AddDepth(InfiniteDepth);
		}
	}
}