using UnityEngine;

namespace SubHorror.Interaction
{
	[CreateAssetMenu(menuName = "Sub Horror/Interaction/Actions/Kill Player Action")]
	public class KillPlayerContextAction : ContextAction
	{
		public override void Execute(GameObject interactor, GameObject target)
		{
			Debug.Log($"{interactor.name} killed {target.name}");
		}
	}
}