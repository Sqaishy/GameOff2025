using System;
using UnityEngine;

namespace SubHorror.Interaction
{
	[CreateAssetMenu(menuName = "Sub Horror/Interaction/Actions/Destroy Equipment Action")]
	public class DestroyEquipmentContextAction : ContextAction
	{
		[SerializeField] private Item equipment;

		public override void Execute(GameObject interactor, GameObject target)
		{
			Equipment equipmentComponent = interactor.GetComponentInParent<Equipment>();

			if (!equipmentComponent)
				return;

			equipmentComponent.TryUnEquip(equipment);
		}
	}
}