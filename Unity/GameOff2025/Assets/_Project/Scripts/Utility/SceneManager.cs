using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages the game's scenes
/// </summary>
public class SceneManager : MonoBehaviour
{
    void OnEnable()
    {
        // todo: to be implemented
        // this is how I implemented it in my university exercises, via Observer Pattern:
        /**
        UserController.OnChangeLvl0 += ChangeScene;
        UserController.OnChangeLvl1 += ChangeScene;
        UserController.OnChangeLvl2 += ChangeScene;
        */
    }
    void OnDisable()
    {
        // todo: to be implemented
        // this is how I implemented it in my university exercises, via Observer Pattern:
        /**
        UserController.OnChangeLvl0 -= ChangeScene;
        UserController.OnChangeLvl1 -= ChangeScene;
        UserController.OnChangeLvl2 -= ChangeScene;
        */
    }

    /// <summary>
    /// Changes the Scene based upon the given index.
    /// </summary>
    /// <param name="inputSceneIndex">Index of the new scene.</param>
    public void ChangeScene(int inputSceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(inputSceneIndex);
    }
    /// <summary>
    /// Switches the Scene to the next one which should be in chronological order under SceneManager.
    /// </summary>
    public void SwitchNextScene()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCount;
        int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (currentScene + 1 <= sceneCount || currentScene + 1 >= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            RestartScene();
        }
    }
    /// <summary>
    /// Reloads the current Scene.
    /// </summary>
    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// Closes the Game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
