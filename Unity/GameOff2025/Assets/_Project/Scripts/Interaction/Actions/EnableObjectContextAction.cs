using UnityEngine;
using UnityEngine.Serialization;

namespace SubHorror.Interaction
{
	[CreateAssetMenu(menuName = "Sub Horror/Interaction/Actions/Enable GameObject Action")]
	public class EnableObjectContextAction : ContextAction
	{
		[SerializeField] private string objectScenePath;

		public override void Execute(GameObject interactor, GameObject target)
		{
			string[] objectPath = objectScenePath.Split("/");

			GameObject foundObject = GameObject.Find(objectPath[0]);
			GameObject targetObject = null;

			for (int i = 1; i < objectPath.Length; i++)
				targetObject = foundObject.transform.Find(objectPath[i]).gameObject;

			if (targetObject)
			{
				targetObject.SetActive(true);
				Debug.Log($"Found target object {targetObject.name}");
			}
		}
	}
}