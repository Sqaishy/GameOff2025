using FMODUnity;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Depth/Stop Depth Consequence")]
	public class StopDepthConsequence : Consequence
	{
		[SerializeField] private bool stopClimb;
		[SerializeField] private EventReference engineShutdown;

		public override void Apply(Task failedTask)
		{
			Depth.Depth.ToggleDepthClimb(!stopClimb);

			if (engineShutdown.IsNull)
				return;

			RuntimeManager.PlayOneShotAttached(engineShutdown, failedTask.TaskOwner);
			RuntimeManager.StudioSystem.setParameterByName(Engine.EngineParamName, 0f);
		}
	}
}