using FMODUnity;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Noise/Set Studio Parameter Consequence")]
	public class SetStudioParameterConsequence : Consequence
	{
		[SerializeField] private string studioParameterName;
		[SerializeField] private float value;

		public override void Apply(Task failedTask)
		{
			RuntimeManager.StudioSystem.setParameterByName(studioParameterName, value);
		}
	}
}