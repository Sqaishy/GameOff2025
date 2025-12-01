using SubHorror.Depth;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Depth/Infinite Depth Consequence")]
	public class InfiniteDepthConsequence : Consequence
	{
		[Tooltip("As this is a consequence you most likely want the depth to reduce. Don't need to put " +
		         "a '-' as that is done under the hood")]
		[SerializeField] private float depthPerSecond;
		[SerializeField] private ToggleDepth depthToggle;

		private InfiniteDepth infiniteDepth;

		public override void Apply(Task failedTask)
		{
			infiniteDepth ??= new InfiniteDepth(depthPerSecond);

			switch (depthToggle)
			{
				case ToggleDepth.Start:
					Depth.Depth.AddDepth(infiniteDepth);
					break;
				case ToggleDepth.Stop:
					infiniteDepth.Stop();
					break;
			}
		}

		private enum ToggleDepth
		{
			Start,
			Stop,
		}
	}
}