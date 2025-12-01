using System.Collections.Generic;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/List Consequences")]
	public class ListConsequence : Consequence
	{
		[SerializeField] private List<Consequence> consequences = new();

		public override void Apply(Task failedTask)
		{
			foreach (Consequence consequence in consequences)
				consequence.Apply(failedTask);
		}
	}
}