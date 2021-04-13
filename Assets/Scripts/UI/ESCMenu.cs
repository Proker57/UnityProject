using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class ESCMenu : MonoBehaviour
    {
        [SerializeField] private SaveLoad _saveLoad;
        [SerializeField] private PlayerInput _inputs;
        [SerializeField] private SceneLoader _sceneLoader;
        [Space]
        [SerializeField] private GameObject _pauseBlock;
        [SerializeField] private Text _resume;
        [SerializeField] private Text _save;
        [SerializeField] private Text _load;
        [SerializeField] private Text _options;
        [SerializeField] private Text _mainMenu;
        [Space]
        [SerializeField] private GameObject _selectedObject;

        private const string StringTableCollectionName = "ESCMenu";

        private void Awake()
        {

            _inputs = new PlayerInput();

            LoadStrings();
        }

        private void Escape_started()
        {
            if (_sceneLoader.CurrentSceneName.Equals("Main") || _sceneLoader.CurrentSceneName.Equals("MainMenu"))
            {
                
            }
            else
            {
                _pauseBlock.SetActive(!_pauseBlock.activeSelf);
                InputToggles.Pause();
                EventSystem.current.SetSelectedGameObject(_selectedObject);
                Inputs.Instance.PlayerInput.SwitchCurrentActionMap("Pause");
            }
        }

        public void Resume()
        {
            ResumePause();
        }

        public void Save()
        {
            ResumePause();
            _saveLoad.Save();
        }

        public void Load()
        {
            ResumePause();
            _saveLoad.Load();
        }

        public void Options()
        {
            // TODO Options
        }

        public void Exit()
        {
            ResumePause();
            DestroyPlayer();
            SceneLoader.SwitchScene("MainMenu");
        }

        private void ResumePause()
        {
            _pauseBlock.SetActive(false);
            InputToggles.Game();
        }

        private void DestroyPlayer()
        {
            Destroy(GameObject.FindGameObjectWithTag("Player").gameObject);
        }

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;

            _inputs.Enable();
            _inputs.Global.Escape.started += _ => Escape_started();
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;

            _inputs.Disable();
            _inputs.Global.Escape.started -= _ => Escape_started();
        }

        private void OnSelectedLocaleChanged(Locale obj)
        {
            LoadStrings();
        }

        private async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;
                _resume.text = GetLocalizedString(stringTable, "resume");
                _save.text = GetLocalizedString(stringTable, "save");
                _load.text = GetLocalizedString(stringTable, "load");
                _options.text = GetLocalizedString(stringTable, "options");
                _mainMenu.text = GetLocalizedString(stringTable, "main_menu");
            }
            else
            {
                Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException);
            }
        }

        private static string GetLocalizedString(StringTable table, string entryName)
        {
            var entry = table.GetEntry(entryName);
            return entry.GetLocalizedString();
        }
    }
}
