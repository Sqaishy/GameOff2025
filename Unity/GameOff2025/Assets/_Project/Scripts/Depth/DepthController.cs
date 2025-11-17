using System;
using System.Collections.Generic;
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

		private Depth depthHandler;
		private List<IDepth> depthContributors = new();
		private float milestone;
		private float currentDepth;
		private float currentMilestone;

		private void Awake()
		{
			depthHandler = new Depth(this);

			milestone = difficulty.StartingDepth / depthMilestones;
			currentDepth = difficulty.StartingDepth;
			currentMilestone = currentDepth - milestone;

			depthContributors.Add(new InfiniteDepth(
				difficulty.StartingDepth / (difficulty.SurfaceTime * 60f)));
		}

		private void Update()
		{
			if (currentDepth <= 0f)
				return;

			for (int i = depthContributors.Count - 1; i >= 0; i--)
			{
				IDepth depthContributor = depthContributors[i];

				currentDepth = Mathf.Clamp(currentDepth - depthContributor.DepthContribution(),
					0f, difficulty.StartingDepth);

				if (!depthContributor.Active)
					RemoveDepth(depthContributor);
			}

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

		public void AddDepth(IDepth depthContributor)
		{
			depthContributors.Add(depthContributor);
		}

		private void RemoveDepth(IDepth depthContributor)
		{
			depthContributors.Remove(depthContributor);
		}
	}
}