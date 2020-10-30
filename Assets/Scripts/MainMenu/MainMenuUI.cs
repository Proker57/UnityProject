using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace BOYAREngine
{
    public class MainMenuUI : MonoBehaviour
    {
        public string NextScene;
        private string _stringTableCollectionName = "Main Menu";

        private Button _newGameButton;
        private Button _optionsButton;
        private Button _exitButton;

        private VisualElement _optionsBlock;
        private Button _ruButton;
        private Button _enButton;

        private void Awake()
        {
            var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _newGameButton = rootVisualElement.Q<Button>("new_game-button");
            _optionsButton = rootVisualElement.Q<Button>("options-button");
            _exitButton = rootVisualElement.Q<Button>("exit-button");

            _optionsBlock = rootVisualElement.Q<VisualElement>("options-block");
            _ruButton = rootVisualElement.Q<Button>("ru-button");
            _enButton = rootVisualElement.Q<Button>("en-button");

            _newGameButton.RegisterCallback<ClickEvent>(ev => NewGame());
            _optionsButton.RegisterCallback<ClickEvent>(ev => ToggleOptionsBlock());
            _exitButton.RegisterCallback<ClickEvent>(ev => ExitApplication());
            _ruButton.RegisterCallback<ClickEvent>(ev => ChangeRuLocale());
            _enButton.RegisterCallback<ClickEvent>(ev => ChangeEnLocale());
        }

        private void NewGame()
        {
            SceneLoader.SwitchScene(NextScene);
        }

        private void ToggleOptionsBlock()
        {
            if (_optionsBlock.style.display == DisplayStyle.None)
            {
                _optionsBlock.style.display = DisplayStyle.Flex;
            }
            else
            {
                _optionsBlock.style.display = DisplayStyle.None;
            }
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