using UnityEngine;

[CreateAssetMenu(menuName = "Sub Horror/Game Difficulty")]
public class GameDifficulty : ScriptableObject
{
	[Header("Noise")]
	[Tooltip("The maximum noise level an object can make before the monster goes into rage")]
	[SerializeField] private float maxNoiseThreshold;

	[Header("Depth")]
	[Tooltip("The depth that the submarine starts at")]
	[SerializeField] private float startingDepth;
	[Tooltip("How long in minutes it takes for the submarine to reach the surface")]
	[SerializeField] private float surfaceTime;

	public float MaxNoiseThreshold => maxNoiseThreshold;
	public float StartingDepth => startingDepth;
	public float SurfaceTime => surfaceTime;
}
