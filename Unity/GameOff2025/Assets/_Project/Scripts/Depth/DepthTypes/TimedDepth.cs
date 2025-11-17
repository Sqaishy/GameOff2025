using UnityEngine;

namespace SubHorror.Depth
{
	public class TimedDepth : IDepth
	{
		public bool Active => currentDuration < depthDuration;
		public float DepthPerSecond { get; }

		private float depthDuration;
		private float currentDuration;

		public TimedDepth(float depthPerSecond, float duration)
		{
			DepthPerSecond = depthPerSecond;
			depthDuration = duration;
		}

		public float DepthContribution()
		{
			currentDuration += Time.deltaTime;

			return DepthPerSecond * Time.deltaTime;
		}
	}
}