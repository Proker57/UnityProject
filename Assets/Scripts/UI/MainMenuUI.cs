using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace BOYAREngine
{
    public class MainMenuUI : MonoBehaviour
    {
        private string _stringTableCollectionName = "Main Menu";

        [SerializeField] private const string NewGameSceneName = "TestLevel001";

        private UIDocument _uiDocument;

        private Button _newGameButton;
        private Button _loadButton;
        private Button _optionsButton;
        private Button _exitButton;

        private VisualElement _optionsBlock;
        private Label _languageLabel;
        private Button _ruButton;
        private Button _enButton;

        private Label _videoLabel;
        private Toggle _toggleFullscreen;
        private Label _resolutionLabel;
        private Button _lhdButton;
        private Button _hdButton;
        private Button _fhdButton;

        private Label _soundLabel;
        private Slider _musicSlider;
        private Slider _soundSlider;

        private Button _backSettingsButton;
        private Button _saveSettingsButton;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();

            var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _newGameButton = rootVisualElement.Q<Button>("new_game-button");
            _loadButton = rootVisualElement.Q<Button>("load-button");
            _optionsButton = rootVisualElement.Q<Button>("options-button");
            _exitButton = rootVisualElement.Q<Button>("exit-button");

            _optionsBlock = rootVisualElement.Q<VisualElement>("options-block");
            _videoLabel = rootVisualElement.Q<Label>("video-label");
            _languageLabel = rootVisualElement.Q<Label>("language-label");
            _ruButton = rootVisualElement.Q<Button>("ru-button");
            _enButton = rootVisualElement.Q<Button>("en-button");

            _toggleFullscreen = rootVisualElement.Q<Toggle>("fullscreen-toggle");
            _resolutionLabel = rootVisualElement.Q<Label>("resolution-label");
            _lhdButton = rootVisualElement.Q<Button>("lhd-button");
            _hdButton = rootVisualElement.Q<Button>("hd-button");
            _fhdButton = rootVisualElement.Q<Button>("fhd-button");

            _soundLabel = rootVisualElement.Q<Label>("sound-label");
            _musicSlider = rootVisualElement.Q<Slider>("music-slider");
            _soundSlider = rootVisualElement.Q<Slider>("sound-slider");

            _backSettingsButton = rootVisualElement.Q<Button>("back_settings-button");
            _saveSettingsButton = rootVisualElement.Q<Button>("save_settings-button");

            _newGameButton.RegisterCallback<ClickEvent>(ev => NewGame());
            _loadButton.RegisterCallback<ClickEvent>(ev => Load());
            _optionsButton.RegisterCallback<ClickEvent>(ev => FlexOptionsBlock(), TrickleDown.TrickleDown);
            _exitButton.RegisterCallback<ClickEvent>(ev => ExitApplication());
            _ruButton.RegisterCallback<ClickEvent>(ev => ChangeLocale("ru"));
            _enButton.RegisterCallback<ClickEvent>(ev => ChangeLocale("en"));
            _toggleFullscreen.RegisterCallback<ClickEvent>(ev => FullscreenToggle());
            _lhdButton.RegisterCallback<ClickEvent>(ev => LowHDResolutionButton());
            _hdButton.RegisterCallback<ClickEvent>(ev => HDResolutionButton());
            _fhdButton.RegisterCallback<ClickEvent>(ev => FullHDResolutionButton());
            _musicSlider.RegisterValueChangedCallback(x => MusicSlider());
            _soundSlider.RegisterValueChangedCallback(x => SoundSlider());

            _backSettingsButton.RegisterCallback<ClickEvent>(ev => BackSettingsButton());
            _saveSettingsButton.RegisterCallback<ClickEvent>(ev => SaveSettingsButton());

            LoadPlayerPrefs();
        }

        private void NewGame()
        {
            SceneLoader.SwitchScene(NewGameSceneName);
            GameController.IsNewGame = true;
            _uiDocument.enabled = false;
        }

        private static void Load()
        {
            GameController.Instance.GetComponent<SaveLoad>().Load();
            GameController.IsNewGame = false;

            SceneLoader.SwitchScene(GameController.Instance.SceneName);
        }

        private void FlexOptionsBlock()
        {
            _optionsBlock.style.display = DisplayStyle.Flex;
        }

        private static void ChangeLocale(string locale)
        {
            PlayerPrefs.SetString("Locale", locale);
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(locale);
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

        // ReSharper disable once InconsistentNaming
        private void LowHDResolutionButton()
        {
            Screen.SetResolution(848, 480, _toggleFullscreen.value);
        }

        // ReSharper disable once InconsistentNaming
        private void HDResolutionButton()
        {
            Screen.SetResolution(1280, 720, _toggleFullscreen.value);
        }

        // ReSharper disable once InconsistentNaming
        private void FullHDResolutionButton()
        {
            Screen.SetResolution(1980, 1080, _toggleFullscreen.value);
        }

        private void MusicSlider()
        {
            AudioMixerVolume.Instance.MasterMixer.SetFloat("Music", _musicSlider.value);
        }

        private void SoundSlider()
        {
            AudioMixerVolume.Instance.MasterMixer.SetFloat("SFX", _soundSlider.value);
        }



        private void BackSettingsButton()
        {
            _optionsBlock.style.display = DisplayStyle.None;
        }

        private void SaveSettingsButton()
        {
            PlayerPrefs.SetString("Locale", LocalizationSettings.SelectedLocale.Identifier.Code);
            PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
            PlayerPrefs.SetFloat("SoundVolume", _soundSlider.value);
            PlayerPrefs.Save();
        }

        private static void ExitApplication()
        {
            Application.Quit(0);
        }

        // ***************************************************************************************************

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
                _videoLabel.text = GetLocalizedString(stringTable, "video");
                _toggleFullscreen.label = GetLocalizedString(stringTable, "fullscreen");
                _resolutionLabel.text = GetLocalizedString(stringTable, "resolution");
                _soundLabel.text = GetLocalizedString(stringTable, "sound_volume");
                _musicSlider.label = GetLocalizedString(stringTable, "music");
                _soundSlider.label = GetLocalizedString(stringTable, "sound");
                _backSettingsButton.text = GetLocalizedString(stringTable, "back_settings");
                _saveSettingsButton.text = GetLocalizedString(stringTable, "save_settings");
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
