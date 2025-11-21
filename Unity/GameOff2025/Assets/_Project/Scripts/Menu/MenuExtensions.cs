public static class MenuExtensions
{
	public static void LoadScene(int sceneIndex)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
	}

	public static void ReloadActiveScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager
			.GetActiveScene().buildIndex);
	}
}