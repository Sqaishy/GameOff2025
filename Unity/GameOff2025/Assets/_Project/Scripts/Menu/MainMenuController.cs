using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using static UserSettingsJSON;

/// <summary>
/// Controls the main menu interactions.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _creditsMenu;
    private SceneManager _sceneManager;
    [SerializeField] private List<Button> _hoverableButtons;
    [SerializeField] private bool _debugMode = false;
    private AudioSource _audioSource_mainMenu;
    [SerializeField] List<AudioClip> _uiHoveringSfx;
    [SerializeField] List<AudioClip> _uiClickingSfx;
    UserSettings _userSettings;

    void Start()
    {
        _userSettings = new UserSettings();
        _sceneManager = GetComponent<SceneManager>();
        _audioSource_mainMenu = GetComponent<AudioSource>();
        InitializeDefaultUserPrefs(_userSettings);
        InitializeHoverableButtons(_hoverableButtons);
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
        // todo: to be implemented
        if (_uiClickingSfx.Count > 0)
        {
            int result = Random.Range(0, _uiClickingSfx.Count);
            _audioSource_mainMenu.PlayOneShot(_uiClickingSfx[result]);
        } else
        {
            Debug.LogError("Error - ClickingClips List is empty.");
        }
    }
    public void PlayHoveringSound()
    {
        // todo: to be implemented
        if (_uiHoveringSfx.Count > 0)
        {
            int result = Random.Range(0, _uiHoveringSfx.Count);
            _audioSource_mainMenu.PlayOneShot(_uiHoveringSfx[result]);
        } else
        {
            Debug.LogError("Error - HoveringClips List is empty.");
        }
    }
}
