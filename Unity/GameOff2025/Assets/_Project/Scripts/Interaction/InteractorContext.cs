using UnityEngine;

namespace SubHorror.Interaction
{
	[CreateAssetMenu(menuName = "Sub Horror/Interaction/Interactor Context")]
	public class InteractorContext : ScriptableObject { }

	public abstract class ContextAction : ScriptableObject
	{
		public abstract void Execute(GameObject interactor);
	}
}