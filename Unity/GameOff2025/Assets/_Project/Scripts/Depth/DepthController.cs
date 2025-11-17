using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SubHorror.Depth
{
	public class DepthController : MonoBehaviour
	{
		[SerializeField] private GameDifficulty difficulty;
		[Tooltip("How many milestones the depth should have, at each milestone an event shoots out so " +
		         "objects can react to it" +
		         "\nMilestone depths are calculated as starting depth / depthMilestone (1000/10 = 100 meters")]
		[SerializeField] private float depthMilestones;

		public static event Action OnDepthMilestone;
		public static event Action OnReachedSurface;
		public float DepthMilestones => depthMilestones;
		public float CurrentDepth => currentDepth;

		private float milestone;
		private float depthPerSecond;
		private float currentDepth;
		private float currentMilestone;

		private void Awake()
		{
			milestone = difficulty.StartingDepth / depthMilestones;
			depthPerSecond = difficulty.StartingDepth / (difficulty.SurfaceTime * 60f);
			currentDepth = difficulty.StartingDepth;
			currentMilestone = currentDepth - milestone;
		}

		private void Update()
		{
			if (currentDepth <= 0f)
				return;

			currentDepth -= depthPerSecond * Time.deltaTime;

			if (currentDepth > currentMilestone)
				return;

			currentMilestone -= milestone;

			Debug.Log($"Depth milestone reached, new milestone {currentMilestone:N0}");
			OnDepthMilestone?.Invoke();

			if (currentDepth <= 0f)
				OnReachedSurface?.Invoke();
		}

		/// <returns>
		/// The current depth as a percentage between 0 and 1
		/// </returns>
		public float GetDepthPercentage01() => currentDepth / difficulty.StartingDepth;
	}
}