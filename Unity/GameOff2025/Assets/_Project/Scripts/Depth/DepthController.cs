using System;
using UnityEngine;

namespace SubHorror.Depth
{
	public class DepthController : MonoBehaviour
	{
		[SerializeField] private GameDifficulty difficulty;
		[Tooltip("How many milestones the depth should have, at each milestone an event shoots out so " +
		         "objects can react to it" +
		         "\nMilestone depths are calculated as starting depth / depthMilestone (1000/10 = 100 meters")]
		[SerializeField] private float depthMilestone;

		public event Action OnDepthMilestone;

		private float milestone;
		private float depthPerSecond;
		private float currentDepth;
		private float currentMilestone;

		private void Awake()
		{
			milestone = difficulty.StartingDepth / depthMilestone;
			depthPerSecond = difficulty.StartingDepth / (difficulty.SurfaceTime * 60f);
			currentDepth = difficulty.StartingDepth;
			currentMilestone = currentDepth - milestone;
		}

		private void Update()
		{
			if (currentDepth <= 0f)
				return;

			currentDepth -= depthPerSecond * Time.deltaTime;

			Debug.Log($"Current Depth: {currentDepth:N0}m");

			if (currentDepth <= currentMilestone)
			{
				currentMilestone -= milestone;

				Debug.Log($"Depth milestone reached, new milestone {currentMilestone:N0}");
				OnDepthMilestone?.Invoke();
			}
		}
	}
}