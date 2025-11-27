using UnityEngine;

namespace SubHorror.Interaction
{
	[CreateAssetMenu(menuName = "Sub Horror/Interaction/Actions/Place Item Action")]
	public class PlaceItemContextAction : ContextAction
	{
		[SerializeField] private Item itemToPlace;
		[SerializeField] private Vector3 placementPosition;
		[SerializeField] private Quaternion placementRotation;

		public override void Execute(GameObject interactor, GameObject target)
		{
			Item clone = Instantiate(itemToPlace, target.transform);
			clone.transform.SetLocalPositionAndRotation(placementPosition, placementRotation);
		}
	}
}