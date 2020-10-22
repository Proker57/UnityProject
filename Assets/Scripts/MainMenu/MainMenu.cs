using System;
using DG.Tweening;
using NoSuchStudio.Localization;
using UnityEngine;
using UnityEngine.UIElements;

namespace BOYAREngine
{
    public class MainMenu : MonoBehaviour
    {
#pragma warning disable 649
        [Header("Toggle visibility of GameObjects")]
        [SerializeField] private GameObject _mainMenuButton;
        [SerializeField] private GameObject _optionsPanel;

#pragma warning restore 649

        public void StartNewGame(string sceneName)
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

        public void SetLanguage(string language)
        {
            LocalizationService.CurrentLocale = language;
        }
    }
}
