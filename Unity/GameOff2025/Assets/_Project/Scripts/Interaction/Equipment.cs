using UnityEngine;

namespace SubHorror.Interaction
{
	public class Equipment : MonoBehaviour
	{
		[SerializeField] private Transform handSlot;

		private GameObject currentEquipment;

		/// <summary>
		///
		/// </summary>
		/// <param name="newEquipment">This GameObject MUST be instantiated prior to this method call</param>
		public void SetEquipment(GameObject newEquipment)
		{
			//First Destroy the current equipment gameobject
			if (currentEquipment)
				Destroy(currentEquipment);
			//Then set and position the new equipment game object
			currentEquipment = newEquipment;
			currentEquipment.transform.SetParent(handSlot);
			currentEquipment.transform.localPosition = Vector3.zero;
		}

		public void TryUnEquip(GameObject equipment)
		{
			if (currentEquipment != equipment || !currentEquipment)
				return;

			Destroy(currentEquipment);
		}
	}
}