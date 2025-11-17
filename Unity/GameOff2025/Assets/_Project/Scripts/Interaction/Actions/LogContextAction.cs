using UnityEngine;

namespace SubHorror.Interaction
{
	[CreateAssetMenu(menuName = "Sub Horror/Interaction/Actions/Log Action")]
	public class LogContextAction : ContextAction
	{
		public override void Execute(GameObject interactor, GameObject target)
		{
			Debug.Log($"Interactor {interactor.name} interacting with {target.name}");
		}
	}
}