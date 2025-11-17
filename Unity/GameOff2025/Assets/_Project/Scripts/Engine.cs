using System;
using System.Collections;
using SubHorror.Interaction;
using SubHorror.Noise;
using UnityEngine;

namespace SubHorror
{
	public class Engine : MonoBehaviour, IInteractable
	{
		[SerializeField] private NoiseSettings noiseSettings;

		private Coroutine repairCoroutine;
		private NoiseEmitter noiseEmitter;
		private ToggleNoise engineNoise;
		private GameObject interactor;
		private float repairTime;
		private float distanceToRepair;
		private float currentRepairTime;

		private void Awake()
		{
			noiseEmitter = GetComponent<NoiseEmitter>();
			engineNoise = new ToggleNoise(noiseEmitter, noiseSettings);
		}

		public void Initialize(float repairTime, float distanceToRepair)
		{
			this.repairTime = repairTime;
			this.distanceToRepair = distanceToRepair;
		}

		/// <returns>
		/// Gets the repair progress between 0 and 100
		/// </returns>
		public float GetRepairProgress()
		{
			return (currentRepairTime / repairTime) * 100;
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

			if (repairCoroutine == null)
				engineNoise.Play();

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

				//Do some distance check to see if the interactor is close enough to the engine
				if (InteractorTooFarFromEngine())
				{
					repairCoroutine = null;
					engineNoise.ResetNoise();
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