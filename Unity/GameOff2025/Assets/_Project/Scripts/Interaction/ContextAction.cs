using UnityEngine;

namespace SubHorror.Interaction
{
	public abstract class ContextAction : ScriptableObject
	{
		public abstract void Execute(GameObject interactor, GameObject target);
	}
}