using System;
using UnityEngine;

namespace SubHorror.Interaction
{
	public class Equipment : MonoBehaviour
	{
		[SerializeField] private Transform handSlot;

		private EquipmentSlot currentEquipment;

		private struct EquipmentSlot
		{
			public Item itemGuid;
			public GameObject itemObject;

			public EquipmentSlot(Item item)
			{
				itemGuid = item;
				itemObject = item.gameObject;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="newEquipment">This GameObject MUST be instantiated prior to this method call</param>
		public void SetEquipment(Item newEquipment)
		{
			//First Destroy the current equipment gameobject
			if (currentEquipment.itemObject)
				DestroyCurrentEquipment();
			//Then set and position the new equipment game object
			currentEquipment = new EquipmentSlot(newEquipment);
			currentEquipment.itemObject.transform.SetParent(handSlot);
			currentEquipment.itemObject.transform.localPosition = Vector3.zero;
		}

		public void TryUnEquip(Item equipment)
		{
			if (currentEquipment.itemGuid != equipment)
				return;

			DestroyCurrentEquipment();
		}

		private void DestroyCurrentEquipment()
		{
			currentEquipment.itemGuid = null;
			Destroy(currentEquipment.itemObject);
		}
	}
}