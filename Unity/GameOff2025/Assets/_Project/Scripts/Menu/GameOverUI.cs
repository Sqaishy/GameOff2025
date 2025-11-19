using System;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	[SerializeField] private GameObject background;

	private void Awake()
	{
		background.SetActive(false);
	}

	public void ShowGameOverUI() => background.SetActive(true);

	public void PlayAgain() => MenuExtensions.ReloadActiveScene();

	public void TransitionToScene(int sceneIndex) => MenuExtensions.LoadScene(sceneIndex);
}