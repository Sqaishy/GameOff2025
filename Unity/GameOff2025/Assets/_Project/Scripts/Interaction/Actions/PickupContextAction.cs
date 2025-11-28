using FMODUnity;
using UnityEngine;

namespace SubHorror.Interaction
{
	[CreateAssetMenu(menuName = "Sub Horror/Interaction/Actions/Pickup Action")]
	public class PickupContextAction : ContextAction
	{
		[SerializeField] private Item pickup;
		[SerializeField] private EventReference pickupAudio;

		public override void Execute(GameObject interactor, GameObject target)
		{
			//Get the (create this component ->) equipment component from the interactor and set the active
			//equipment from the instantiated pickup

			if (!interactor.TryGetComponent(out Equipment equipment))
				return;

			equipment.SetEquipment(pickup.Clone());
			RuntimeManager.PlayOneShotAttached(pickupAudio, interactor.gameObject);
		}
	}
}