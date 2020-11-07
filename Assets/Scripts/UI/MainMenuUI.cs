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

        private GameController _gameController;

        private Button _newGameButton;
        private Button _loadButton;
        private Button _optionsButton;
        private Button _exitButton;

        private VisualElement _optionsBlock;
        private Button _ruButton;
        private Button _enButton;

        private void Awake()
        {
            _gameController = GetComponent<GameController>();

            var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _newGameButton = rootVisualElement.Q<Button>("new_game-button");
            _loadButton = rootVisualElement.Q<Button>("load-button");
            _optionsButton = rootVisualElement.Q<Button>("options-button");
            _exitButton = rootVisualElement.Q<Button>("exit-button");

            _optionsBlock = rootVisualElement.Q<VisualElement>("options-block");
            _ruButton = rootVisualElement.Q<Button>("ru-button");
            _enButton = rootVisualElement.Q<Button>("en-button");

            _newGameButton.RegisterCallback<ClickEvent>(ev => NewGame());
            _loadButton.RegisterCallback<ClickEvent>(ev => Load());
            _optionsButton.RegisterCallback<ClickEvent>(ev => FlexOptionsBlock());
            _exitButton.RegisterCallback<ClickEvent>(ev => ExitApplication());
            _ruButton.RegisterCallback<ClickEvent>(ev => ChangeRuLocale());
            _enButton.RegisterCallback<ClickEvent>(ev => ChangeEnLocale());
        }

        private void NewGame()
        {
            SceneLoader.SwitchScene("TestLevel001");
        }

        private void Load()
        {
            _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

            _gameController.GetComponent<SaveLoad>().Load();
            _gameController.IsNewGame = false;
            SceneLoader.SwitchScene(_gameController.SceneName);
        }

        private void FlexOptionsBlock()
        {
            _optionsBlock.style.display = DisplayStyle.Flex;
        }

        private void ChangeRuLocale()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("ru");
        }

        private void ChangeEnLocale()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en");
        }

        private void ExitApplication()
        {
            Application.Quit(0);
        }

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
