using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace BOYAREngine
{
    public class MainMenuUI : MonoBehaviour
    {
        private string _stringTableCollectionName = "Main Menu";

        [SerializeField] private string _newGameSceneName = "TestLevel000";

        private GameController _gameController;
        private UIDocument _uiDocument;

        private Button _newGameButton;
        private Button _loadButton;
        private Button _optionsButton;
        private Button _exitButton;

        private VisualElement _optionsBlock;
        private Label _languageLabel;
        private Button _ruButton;
        private Button _enButton;
        private Toggle _toggleFullscreen;
        private Label _resolutionLabel;
        private Button _lhdButton;
        private Button _hdButton;
        private Button _fhdButton;

        private void Awake()
        {
            _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            _uiDocument = GetComponent<UIDocument>();

            var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _newGameButton = rootVisualElement.Q<Button>("new_game-button");
            _loadButton = rootVisualElement.Q<Button>("load-button");
            _optionsButton = rootVisualElement.Q<Button>("options-button");
            _exitButton = rootVisualElement.Q<Button>("exit-button");

            _optionsBlock = rootVisualElement.Q<VisualElement>("options-block");
            _languageLabel = rootVisualElement.Q<Label>("language-label");
            _ruButton = rootVisualElement.Q<Button>("ru-button");
            _enButton = rootVisualElement.Q<Button>("en-button");
            _toggleFullscreen = rootVisualElement.Q<Toggle>("fullscreen-toggle");
            _resolutionLabel = rootVisualElement.Q<Label>("resolution-label");
            _lhdButton = rootVisualElement.Q<Button>("lhd-button");
            _hdButton = rootVisualElement.Q<Button>("hd-button");
            _fhdButton = rootVisualElement.Q<Button>("fhd-button");

            _newGameButton.RegisterCallback<ClickEvent>(ev => NewGame());
            _loadButton.RegisterCallback<ClickEvent>(ev => Load());
            _optionsButton.RegisterCallback<ClickEvent>(ev => FlexOptionsBlock());
            _exitButton.RegisterCallback<ClickEvent>(ev => ExitApplication());
            _ruButton.RegisterCallback<ClickEvent>(ev => ChangeRuLocale());
            _enButton.RegisterCallback<ClickEvent>(ev => ChangeEnLocale());
            _toggleFullscreen.RegisterCallback<ClickEvent>(ev => FullscreenToggle());
            _lhdButton.RegisterCallback<ClickEvent>(ev => LowHDResolutionButton());
            _hdButton.RegisterCallback<ClickEvent>(ev => HDResolutionButton());
            _fhdButton.RegisterCallback<ClickEvent>(ev => FullHDResolutionButton());

            if (PlayerPrefs.HasKey("Locale"))
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(PlayerPrefs.GetString("Locale"));
            }
        }

        private void NewGame()
        {
            SceneLoader.SwitchScene(_newGameSceneName);
            GameController.IsNewGame = true;
            _uiDocument.enabled = false;
        }

        private void Load()
        {
            _gameController.GetComponent<SaveLoad>().Load();
            GameController.IsNewGame = false;
            SceneLoader.SwitchScene(_gameController.SceneName);
        }

        private void FlexOptionsBlock()
        {
            _optionsBlock.style.display = DisplayStyle.Flex;
        }

        private void ChangeRuLocale()
        {
            PlayerPrefs.SetString("Locale", "ru");
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("ru");
        }

        private void ChangeEnLocale()
        {
            PlayerPrefs.SetString("Locale", "en");
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en");
        }

        private void FullscreenToggle()
        {
            if (_toggleFullscreen.value)
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            }
            else
            {
                Screen.SetResolution(640, 480, false);
            }
        }

        private void LowHDResolutionButton()
        {
            Screen.SetResolution(848, 480, _toggleFullscreen.value);
        }

        private void HDResolutionButton()
        {
            Screen.SetResolution(1280, 720, _toggleFullscreen.value);
        }

        private void FullHDResolutionButton()
        {
            Screen.SetResolution(1980, 1080, _toggleFullscreen.value);
        }

        private void ExitApplication()
        {
            Application.Quit(0);
        }

        // ***************************************************************************************************

        private void OnEnable()
        {
            StartCoroutine(LoadStrings());
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        private void OnSelectedLocaleChanged(Locale obj)
        {
            StartCoroutine(LoadStrings());
        }

        private IEnumerator LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(_stringTableCollectionName);
            yield return loadingOperation;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;
                _newGameButton.text = GetLocalizedString(stringTable, "new_game");
                _loadButton.text = GetLocalizedString(stringTable, "load");
                _optionsButton.text = GetLocalizedString(stringTable, "options");
                _exitButton.text = GetLocalizedString(stringTable, "exit");
                _languageLabel.text = GetLocalizedString(stringTable, "language");
                _toggleFullscreen.label = GetLocalizedString(stringTable, "fullscreen");
                _resolutionLabel.text = GetLocalizedString(stringTable, "resolution");
            }
            else
            {
                Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException.ToString());
            }
        }

        private string GetLocalizedString(StringTable table, string entryName)
        {
            var entry = table.GetEntry(entryName);
            return entry.GetLocalizedString();
        }
    }
}
