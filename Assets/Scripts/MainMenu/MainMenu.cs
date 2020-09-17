using System;
using NoSuchStudio.Localization;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
#pragma warning disable 649
    [Header("Toggle visibility of GameObjects")]
    [SerializeField] private GameObject _mainMenuButton;
    [SerializeField] private GameObject _optionsPanel;
    [Header("Fog")]
    [SerializeField] private GameObject _fog1;
    [SerializeField] private GameObject _fog2;
    [SerializeField] private float _scrollSpeed = 2;
#pragma warning restore 649

    public void StartNewGame(String sceneName)
    {
        SceneLoader.SwitchScene(sceneName);
    }

    public void OpenOptionsMenu()
    {
        _mainMenuButton.SetActive(false);
        _optionsPanel.SetActive(true);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void BackButtonOnClick()
    {
        _mainMenuButton.SetActive(true);
        _optionsPanel.SetActive(false);
    }

    public void SetLanguage(String language)
    {
        LocalizationService.CurrentLocale = language;
    }
}
