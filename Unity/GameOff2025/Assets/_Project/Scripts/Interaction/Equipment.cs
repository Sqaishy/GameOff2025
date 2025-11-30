using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubHorror.Interaction
{
	public class Equipment : MonoBehaviour
	{
		[SerializeField] private Transform handSlot;

		private Stack<EquipmentSlot> equipmentStack = new();
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
			EquipmentSlot newEquipmentSlot = new EquipmentSlot(newEquipment);
			currentEquipment = newEquipmentSlot;
			equipmentStack.Push(currentEquipment);
			currentEquipment.itemObject.transform.SetParent(handSlot);
			currentEquipment.itemObject.transform.localPosition = Vector3.zero;
		}

		public void TryUnEquip(Item equipment)
		{
			if (!currentEquipment.itemGuid)
				return;

			if (currentEquipment.itemGuid != equipment)
				return;

			DestroyCurrentEquipment();

			//TODO The peek may not work as I am destroying the object when unequipping it so may be a null ref
			//Will come back to this when I have multiple tasks active at once to test

			equipmentStack.Pop();

			if (equipmentStack.TryPeek(out EquipmentSlot slot))
			{
				currentEquipment.itemGuid = slot.itemGuid;
				currentEquipment.itemObject = slot.itemObject;
			}
		}

		private void DestroyCurrentEquipment()
		{
			Destroy(currentEquipment.itemObject);
			currentEquipment = default;
		}
	}
}