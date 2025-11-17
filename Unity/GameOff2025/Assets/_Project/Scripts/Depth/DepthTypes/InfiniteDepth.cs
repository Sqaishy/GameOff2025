using UnityEngine;

namespace SubHorror.Depth
{
	public class InfiniteDepth : IDepth
	{
		public bool Active => true;
		public float DepthPerSecond { get; }

		public InfiniteDepth(float depthPerSecond)
		{
			DepthPerSecond = depthPerSecond;
		}

		public float DepthContribution()
		{
			return DepthPerSecond * Time.deltaTime;
		}
	}
}