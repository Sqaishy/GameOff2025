using UnityEngine;

namespace SubHorror.Depth
{
	public class InfiniteDepth : IDepth
	{
		public bool Active => active;
		public float DepthPerSecond { get; }

		private bool active = true;

		public InfiniteDepth(float depthPerSecond)
		{
			DepthPerSecond = depthPerSecond;
		}

		public float DepthContribution()
		{
			return DepthPerSecond * Time.deltaTime;
		}

		public void Stop() => active = false;
	}
}