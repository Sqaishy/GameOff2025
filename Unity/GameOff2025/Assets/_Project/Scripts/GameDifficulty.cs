using UnityEngine;

[CreateAssetMenu(menuName = "Sub Horror/Game Difficulty")]
public class GameDifficulty : ScriptableObject
{
	[Tooltip("The maximum noise level an object can make before the monster goes into rage")]
	[SerializeField] private float maxNoiseThreshold;

	public float MaxNoiseThreshold => maxNoiseThreshold;
}
