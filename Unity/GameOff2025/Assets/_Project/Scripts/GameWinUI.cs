using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SubHorror
{
	public class GameWinUI : MonoBehaviour
	{
		[SerializeField] private GameObject winPanel;
		[Tooltip("The gameObject that is first selected when the UI is enabled -> for controller support")]
		[SerializeField] private GameObject firstSelected;

		private void Awake()
		{
			winPanel.SetActive(false);
		}

		public void ShowWinUI()
		{
			winPanel.SetActive(true);

			InputHandler.EnableCursor();
			StartCoroutine(SelectUIElement());
		}

		public void PlayAgain() => MenuExtensions.ReloadActiveScene();

		public void TransitionToScene(int sceneIndex) => MenuExtensions.LoadScene(sceneIndex);

		private IEnumerator SelectUIElement()
		{
			yield return new WaitForSeconds(0.5f);

			Debug.Log($"Select UI element: {firstSelected.name}", firstSelected);

			EventSystem.current.SetSelectedGameObject(firstSelected);

			Debug.Log($"UI element selected: {EventSystem.current.currentSelectedGameObject.name}", EventSystem.current.currentSelectedGameObject);
		}
	}
}