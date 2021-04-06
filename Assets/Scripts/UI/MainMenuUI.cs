using System.Collections;
using System.Globalization;
using BOYAREngine.Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace BOYAREngine.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private const string StringTableCollectionName = "Main Menu";

        [SerializeField] private string NewGameSceneName = "TestLevel001";

        [Header("Current Selected Objects")]
        [SerializeField] private GameObject _newGameButtonGameObject;
        [SerializeField] private GameObject _optionsButtonGameObject;

        [Header("Main Menu")]
        [SerializeField] private Text _newGameButton;
        [SerializeField] private Text _loadButton;
        [SerializeField] private Text _optionsButton;
        [SerializeField] private Text _exitButton;

        [Header("Options")]
        [SerializeField] private GameObject _optionsBlock;
        [SerializeField] private Text _languageLabel;
        [SerializeField] private Button _ruButton;
        [SerializeField] private Button _enButton;
        [Space]
        [SerializeField] private Text _videoLabel;
        [SerializeField] private Toggle _toggleFullscreen;
        [SerializeField] private Text _toggleFullscreenText;
        [SerializeField] private Text _resolutionLabel;
        [SerializeField] private Button _lhdButton;
        [SerializeField] private Button _hdButton;
        [SerializeField] private Button _fhdButton;
        [Space]
        [SerializeField] private Text _soundLabel;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Text _musicSliderText;
        [SerializeField] private Text _musicValue;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Text _soundSliderText;
        [SerializeField] private Text _soundValue;
        [Space]
        [SerializeField] private Button _backSettingsButton;
        [SerializeField] private Text _backSettingsButtonText;
        [SerializeField] private Button _saveSettingsButton;
        [SerializeField] private Text _saveSettingsButtonText;

        private void Awake()
        {
            LoadPlayerPrefs();

            EventSystem.current.firstSelectedGameObject = _newGameButtonGameObject;
        }

        public void NewGame()
        {
            SceneLoader.SwitchScene(NewGameSceneName);
            GameController.IsNewGame = true;
            Events.NewGame?.Invoke();
        }

        public void Load()
        {
            GameController.Instance.GetComponent<SaveLoad>().Load();
            GameController.IsNewGame = false;

            SceneLoader.SwitchScene(GameController.Instance.SceneName);
        }

        public void FlexOptionsBlock()
        {
            _optionsBlock.SetActive(true);
        }

        public static void ExitApplication()
        {
            Application.Quit(0);
        }

        public static void ChangeRULocale()
        {
            PlayerPrefs.SetString("Locale", "ru");
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("ru");
        }

        public static void ChangeENLocale()
        {
            PlayerPrefs.SetString("Locale", "en");
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en");
        }

        public void FullscreenToggle()
        {
            if (_toggleFullscreen.isOn)
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            }
            else
            {
                Screen.SetResolution(640, 480, false);
            }
        }

        public void LowHDResolutionButton()
        {
            Screen.SetResolution(848, 480, _toggleFullscreen.isOn);
        }

        public void HDResolutionButton()
        {
            Screen.SetResolution(1280, 720, _toggleFullscreen.isOn);
        }

        public void FullHDResolutionButton()
        {
            Screen.SetResolution(1980, 1080, _toggleFullscreen.isOn);
        }

        public void MusicSlider()
        {
            AudioMixer.Instance.MasterMixer.SetFloat("Music", _musicSlider.value);
            _musicValue.text = _musicSlider.value.ToString("F1");
        }

        public void SoundSlider()
        {
            AudioMixer.Instance.MasterMixer.SetFloat("SFX", _soundSlider.value);
            AudioMixer.Instance.MasterMixer.SetFloat("BGSound", _soundSlider.value);
            _soundValue.text = _soundSlider.value.ToString("F1");
        }


        public void BackSettingsButton()
        {
            _optionsBlock.SetActive(false);

            EventSystem.current.SetSelectedGameObject(_optionsButtonGameObject);
        }

        public void SaveSettingsButton()
        {
            PlayerPrefs.SetString("Locale", LocalizationSettings.SelectedLocale.Identifier.Code);
            PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
            PlayerPrefs.SetFloat("SoundVolume", _soundSlider.value);
            PlayerPrefs.Save();
        }

        private void LoadPlayerPrefs()
        {
            if (PlayerPrefs.HasKey("Locale"))
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(PlayerPrefs.GetString("Locale"));
            }

            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            }
            if (PlayerPrefs.HasKey("SoundVolume"))
            {
                _soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
            }
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
                _newGameButton.text = GetLocalizedString(stringTable, "new_game");
                _loadButton.text = GetLocalizedString(stringTable, "load");
                _optionsButton.text = GetLocalizedString(stringTable, "options");
                _exitButton.text = GetLocalizedString(stringTable, "exit");

                _languageLabel.text = GetLocalizedString(stringTable, "language");
                _videoLabel.text = GetLocalizedString(stringTable, "video");
                _toggleFullscreenText.text = GetLocalizedString(stringTable, "fullscreen");
                _resolutionLabel.text = GetLocalizedString(stringTable, "resolution");
                _soundLabel.text = GetLocalizedString(stringTable, "sound_volume");
                _musicSliderText.text = GetLocalizedString(stringTable, "music");
                _soundSliderText.text = GetLocalizedString(stringTable, "sound");

                _backSettingsButtonText.text = GetLocalizedString(stringTable, "back_settings");
                _saveSettingsButtonText.text = GetLocalizedString(stringTable, "save_settings");
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

