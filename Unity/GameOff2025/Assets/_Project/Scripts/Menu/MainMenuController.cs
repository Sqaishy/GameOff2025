using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using UnityEditor;
using UnityEngine.EventSystems;
using static UserSettingsJSON;

/// <summary>
/// Controls the main menu interactions.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _creditsMenu;
    [Tooltip("The gameObject that is first selected when the UI is enabled -> for controller support")]
    [SerializeField] private GameObject firstSelected;
    private SceneManager _sceneManager;
    [SerializeField] private List<Button> _hoverableButtons;
    [SerializeField] private bool _debugMode = false;
    UserSettings _userSettings;

    void Start()
    {
        _userSettings = new UserSettings();
        _sceneManager = GetComponent<SceneManager>();
        InitializeDefaultUserPrefs(_userSettings);
        InitializeHoverableButtons(_hoverableButtons);

        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    private void InitializeHoverableButtons(List<Button> hoverableButtons)
    {
        foreach (Button button in hoverableButtons)
        {
            button.onClick.AddListener(PlayClickingSound);
            var eventTrigger = button.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();
            var entry = new UnityEngine.EventSystems.EventTrigger.Entry
            {
                eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter
            };
            entry.callback.AddListener((eventData) => { PlayHoveringSound(); });
            eventTrigger.triggers.Add(entry);
        }
    }

    private void InitializeDefaultUserPrefs(UserSettings userSettings)
    {
        _userSettings.masterVolume = 1f;
        _userSettings.musicVolume = 1f;
        _userSettings.sfxVolume = 1f;
        _userSettings.ambienceVolume = 1f;
    }

    public void SetActiveUISelection(GameObject selection) => EventSystem.current.SetSelectedGameObject(selection,
        new BaseEventData(EventSystem.current));

    public void StartGame()
    {
        _sceneManager.ChangeScene(1);
    }
    public void ToggleOptions()
    {
        _mainMenu.SetActive(!_mainMenu.activeSelf);
        _optionsMenu.SetActive(!_optionsMenu.activeSelf);
    }
    public void ToggleCredits()
    {
        _mainMenu.SetActive(!_mainMenu.activeSelf);
        _creditsMenu.SetActive(!_creditsMenu.activeSelf);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PlayClickingSound()
    {
        AudioManager.Instance.PlayOneShot2D(FMODEvents.Instance.menuClick);
    }
    public void PlayHoveringSound()
    {
        AudioManager.Instance.PlayOneShot2D(FMODEvents.Instance.menuHover);
    }
}

public static class EventLogger
{
    [MenuItem("Tools/UI/Log Current Selection")]
    private static void LogCurrentUISelection()
    {
        Debug.Log($"Current UI element selected {EventSystem.current.currentSelectedGameObject.name}", EventSystem.current.currentSelectedGameObject);
    }
}
