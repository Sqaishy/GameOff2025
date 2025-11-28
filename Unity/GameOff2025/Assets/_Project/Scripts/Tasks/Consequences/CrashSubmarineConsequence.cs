using FMODUnity;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Crash Submarine Consequence")]
	public class CrashSubmarineConsequence : Consequence
	{
		[SerializeField] private EventReference crashAudio;

		public override void Apply(Task failedTask)
		{
			FindFirstObjectByType<LeakyHoleSpawner>().SpawnHole();

			RuntimeManager.PlayOneShot(crashAudio);
		}
	}
}