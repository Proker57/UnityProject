using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace BOYAREngine
{
    public class ESCMenu : MonoBehaviour
    {
        [SerializeField] private SaveLoad _saveLoad;

        private VisualElement _screen;
        private Button _resume;
        private Button _save;
        private Button _load;
        private Button _mainMenu;

        private const string StringTableCollectionName = "ESCMenu";

        private void Awake()
        {
            var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _screen = rootVisualElement.Q<VisualElement>("screen-block");
            _resume = rootVisualElement.Q<Button>("menu_resume-button");
            _save = rootVisualElement.Q<Button>("menu_save-button");
            _load = rootVisualElement.Q<Button>("menu_load-button");
            _mainMenu = rootVisualElement.Q<Button>("menu_main_menu-button");

            _resume.RegisterCallback<ClickEvent>(ev => Resume());
            _save.RegisterCallback<ClickEvent>(ev => Save());
            _load.RegisterCallback<ClickEvent>(ev => Load());
            _mainMenu.RegisterCallback<ClickEvent>(ev => MainMenu());
        }

        private void Start()
        {
            Inputs.Input.Global.Escape.started += _ => Escape_started();
            Inputs.Input.Global.Escape.started += _ => Escape_canceled();
        }

        private void Escape_started()
        {
            if (_screen.style.display == DisplayStyle.Flex)
            {
                _screen.style.display = DisplayStyle.None;
            }
            else
            {
                _screen.style.display = DisplayStyle.Flex;
            }
        }

        private void Escape_canceled()
        {

        }

        private void Resume()
        {
            _screen.style.display = DisplayStyle.None;
        }

        private void Save()
        {
            _screen.style.display = DisplayStyle.None;
            _saveLoad.Save();
        }

        private void Load()
        {
            _screen.style.display = DisplayStyle.None;
            _saveLoad.Load();
        }

        private void MainMenu()
        {
            _screen.style.display = DisplayStyle.None;
            SceneLoader.SwitchScene("MainMenu");
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
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            yield return loadingOperation;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;
                _resume.text = GetLocalizedString(stringTable, "resume");
                _save.text = GetLocalizedString(stringTable, "save");
                _load.text = GetLocalizedString(stringTable, "load");
                _mainMenu.text = GetLocalizedString(stringTable, "main_menu");
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
