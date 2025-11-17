using System.Collections;
using SubHorror.Interaction;
using UnityEngine;

namespace SubHorror
{
	public class Engine : MonoBehaviour, IInteractable
	{
		private Coroutine repairCoroutine;
		private GameObject interactor;
		private float repairTime;
		private float distanceToRepair;
		private float currentRepairTime;

		public void Initialize(float repairTime, float distanceToRepair)
		{
			this.repairTime = repairTime;
			this.distanceToRepair = distanceToRepair;
		}

		public bool EngineFixed()
		{
			return currentRepairTime >= repairTime;
		}

		public bool CanInteract()
		{
			return true;
		}

		public void Interact(GameObject interactor, InteractorContext context)
		{
			//On interact start a coroutine

			this.interactor = interactor;

			repairCoroutine ??= StartCoroutine(RepairEngine());
		}

		public void ResetInteraction()
		{

		}

		private IEnumerator RepairEngine()
		{
			while (currentRepairTime < repairTime)
			{
				currentRepairTime += Time.deltaTime;

				Debug.Log($"Repair time: {currentRepairTime:N0}");

				//Do some distance check to see if the interactor is close enough to the engine
				if (InteractorTooFarFromEngine())
				{
					repairCoroutine = null;
					yield break;
				}

				yield return null;
			}
		}

		private bool InteractorTooFarFromEngine()
		{
			return Vector3.Distance(transform.position, interactor.transform.position) > distanceToRepair;
		}
	}
}