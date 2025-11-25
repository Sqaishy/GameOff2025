using System;
using SubHorror;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	[SerializeField] private GameObject background;

	private void Awake()
	{
		background.SetActive(false);
	}

	public void ShowGameOverUI()
	{
		background.SetActive(true);

		InputHandler.EnableCursor();
	}

	public void PlayAgain() => MenuExtensions.ReloadActiveScene();

	public void TransitionToScene(int sceneIndex) => MenuExtensions.LoadScene(sceneIndex);
}