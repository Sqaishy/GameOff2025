using System;
using UnityEngine;
using UnityEngine.UI;

namespace SubHorror.Interaction
{
	public class InteractUI : MonoBehaviour
	{
		[SerializeField] private Interactor interactor;
		[SerializeField] private GameObject pointer;

		private void Awake()
		{
			TogglePointer(false);
		}

		private void OnEnable()
		{
			interactor.OnEnterInteractable += ShowPointer;
			interactor.OnExitInteractable += HidePointer;
		}

		private void OnDisable()
		{
			interactor.OnEnterInteractable -= ShowPointer;
			interactor.OnExitInteractable -= HidePointer;
		}

		private void TogglePointer(bool toggle)
		{
			pointer.SetActive(toggle);
		}

		private void ShowPointer()
		{
			TogglePointer(true);
		}

		private void HidePointer()
		{
			TogglePointer(false);
		}
	}
}