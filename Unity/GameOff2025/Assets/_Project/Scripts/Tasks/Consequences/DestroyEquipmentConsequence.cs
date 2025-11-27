using SubHorror.Interaction;
using UnityEngine;

namespace SubHorror.Tasks
{
	[CreateAssetMenu(menuName = "Sub Horror/Tasks/Consequences/Equipment/Destroy Equipment Consequence")]
	public class DestroyEquipmentConsequence : Consequence
	{
		[SerializeField] private GameObject equipment;

		public override void Apply(Task failedTask)
		{
			if (!failedTask.TaskOwner.TryGetComponent(out Equipment equipmentComponent))
				return;

			equipmentComponent.TryUnEquip(equipment);
		}
	}
}