using UnityEngine;

namespace SubHorror.Tasks
{
	public abstract class Consequence : ScriptableObject
	{
		public abstract void Apply(Task failedTask);
	}
}