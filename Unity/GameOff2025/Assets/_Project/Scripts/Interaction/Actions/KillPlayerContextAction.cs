using System.Collections;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

namespace SubHorror.Interaction
{
	[CreateAssetMenu(menuName = "Sub Horror/Interaction/Actions/Kill Player Action")]
	public class KillPlayerContextAction : ContextAction
	{
		[Tooltip("How long it should take for the player to face the monster")]
		[SerializeField] private float rotationTime;

		public override void Execute(GameObject interactor, GameObject target)
		{
			Debug.Log($"{interactor.name} killed {target.name}");

			//I want to do a few things with this
			//First disable player input, this includes the cinemachine camera pan tilt <- so long as this
			//is active you cannot manually rotate the camera
			//Fade to a game over screen

			target.transform.root.GetComponent<InputHandler>().enabled = false;
			target.transform.root.GetComponentInChildren<CinemachinePanTilt>().enabled = false;

			interactor.GetComponent<MonoBehaviour>().StartCoroutine(
				GameOverSequence(interactor, target));
		}

		private IEnumerator GameOverSequence(GameObject interactor, GameObject target)
		{
			Vector3 lookPosition = interactor.transform.position - target.transform.position;
			lookPosition.y = 0f;

			Quaternion targetRotation = Quaternion.LookRotation(lookPosition);

			CinemachineCamera camera = target.transform.root.GetComponentInChildren<CinemachineCamera>();

			float currentRotationTime = 0f;

			while (currentRotationTime < 1f)
			{
				camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, targetRotation,
					currentRotationTime * rotationTime);

				currentRotationTime += Time.deltaTime;

				yield return null;
			}

			FindFirstObjectByType<GameOverUI>().ShowGameOverUI();
		}
	}
}