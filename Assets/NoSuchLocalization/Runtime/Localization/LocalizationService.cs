using NoSuchStudio.Common;
using NoSuchStudio.Common.Service;
using NoSuchStudio.Localization.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace NoSuchStudio.Localization {

    /// <summary>
    /// Is responsible for connecting translation sources and localized components together. 
    /// This service acts as a central hub. Essentially implementing a publisher-consumer pattern.
    /// Translation sources publish their translations to the service.
    /// Localized components read the corresponding values from the service.
    /// </summary>
    /// <remarks>
    /// For common usage, you should use the static methods of this class.
    /// </remarks>
    [ExecuteAlways]
    public partial class LocalizationService : Service<LocalizationService> {
        public enum DatabasePreset {
            None,
            Languages,
            Locales,
            Custom,
        }
        public delegate void LocaleChangeDelegate(Locale locale);
        private LocaleChangeDelegate _localeChangeEvent = null;
        private void DoAddLocaleChangeListener(LocaleChangeDelegate action) {
            _localeChangeEvent += action;
        }

        private void DoRemoveLocaleChangeListener(LocaleChangeDelegate action) {
            _localeChangeEvent -= action;
        }

        public delegate void TranslationChangeDelegate(string phrase, Locale locale, string translation);
        private Dictionary<string, TranslationChangeDelegate> _translationChangeEvents;

        private void DoAddTranslationChangeListener(string phrase, TranslationChangeDelegate action) {
            if (string.IsNullOrEmpty(phrase)) return;
            if (!_translationChangeEvents.ContainsKey(phrase)) {
                _translationChangeEvents[phrase] = null;
            }
            _translationChangeEvents[phrase] += action;
        }

        private void DoRemoveTranslationChangeListener(string phrase, TranslationChangeDelegate action) {
            if (string.IsNullOrEmpty(phrase)) return;
            if (!_translationChangeEvents.ContainsKey(phrase)) {
                return;
            }
            _translationChangeEvents[phrase] -= action;
        }

        private HashSet<(string, string)> _changedTranslations;

        // populated from registered sources
        [NonSerialized] private Dictionary<string, Dictionary<string, ITranslationSource>> _translationSources;

        [SerializeField, HideInInspector] private DatabasePreset _databasePreset;
        public DatabasePreset databasePreset {
            get { return _databasePreset; }
        }
        [SerializeField, HideInInspector] private TextAsset _localeDatabaseAsset;
        private LocaleDatabase _localeDatabase;
        public LocaleDatabase localeDatabase {
            get { return _localeDatabase; }
        }

        [SerializeField] private List<Locale> _locales;
        public IList<Locale> GetLocales() {
            return _locales.AsReadOnly();
        }

        public List<Locale> locales {
            set {
                _locales = value;
#if UNITY_EDITOR
                ComputeCaches();
#endif
            }
        }
       
        [SerializeField][LocaleProperty(true, LocalePropertyDrawMode.FlatDropdown)] private Locale _defaultLocale;
        private Locale defaultLocale {
            get { return _defaultLocale; }
            set { _defaultLocale = value; }
        }

        [SerializeField][LocaleProperty(true, LocalePropertyDrawMode.FlatDropdown)] private Locale _currentLocale;
        private Locale currentLocale {
            get { return _currentLocale; }
            set {
                SelectLocale(value.Name, true, false);
            }
        }

        [SerializeField] private bool _autoDetectLocale;
        public bool autoDetectLocale {
            get { return _autoDetectLocale; }
            set {
                _autoDetectLocale = value;
            }
        }

        [SerializeField] private bool _saveLocale;
        public bool saveLocale {
            get { return _saveLocale; }
            set {
                _saveLocale = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locale">locale name to switch to. Assumes Locale is valid for switching.</param>
        /// <param name="dontSave">if true, the locale won't be saved for prefs.</param>
        private void SetCurrentLocale(string locale, bool dontSave = false) {
            _currentLocale = locale;
            if (_saveLocale && !dontSave) {
                WriteLocaleToPrefs();
            }
            _localeChangeEvent?.Invoke(_currentLocale);
        }

        private void WriteLocaleToPrefs() {
            PlayerPrefs.SetString(PlayerPrefKeyLocale, _currentLocale);
            PlayerPrefs.Save();
        }

        private bool LoadLocaleFromPrefs() {
            if (PlayerPrefs.HasKey(PlayerPrefKeyLocale)) {
                return SelectLocale(PlayerPrefs.GetString(PlayerPrefKeyLocale), true, true);
            }
            return false;
        }

        private bool SelectLocale(string localeName, bool useNeutralIfFailed = true, bool useDefaultIfFailed = true) {
            if (!_localeDatabase.ContainsLocale(localeName)) {
                LogWarn("locale {0} not in db", localeName);
                if (useDefaultIfFailed) SelectLocale(_defaultLocale, true, false);
                return false;
            }
            var locale = _localeDatabase[localeName];

            if (_locales.Contains(locale)) {
                LogLog("locale {0} is enabled, switching...", localeName);
                SetCurrentLocale(localeName);
                return true;
            } else if (useNeutralIfFailed && !locale.IsNeutral && _locales.Contains(locale.Parent)) {
                LogLog("locale {0} is not enabled but its neutral locale {1} is, switching...", localeName, locale.Parent);
                SelectLocale(locale.Parent, true, useDefaultIfFailed);
                return true;
            } else {
                LogLog("locale {0} is not enabled", localeName);
                if (useDefaultIfFailed) SelectLocale(_defaultLocale, true, false);
                return false;
            }
        }

        private bool DoDetectAndApplySystemLocale(bool useDefaultIfFailed = false) {
            var cultureInfo = CultureUtil.SystemCultureInfo();
            var systemLocaleName = cultureInfo.Name.ToLower();
            return SelectLocale(systemLocaleName, useDefaultIfFailed);
        }

        /*private bool DoAddLocale(string locale) {
            if (_locales.Contains(locale)) {
                return false;
            } else {
                _locales.Add(locale);
                return true;
            }
        }

        private bool DoRemoveLocale(string locale) {
            if (_locales.Contains(locale)) {
                _locales.Remove(locale);
                return true;
            } else {
                return false;
            }
        }*/

        private bool DoAddPhrase(string phrase) {
            if (_translationSources.ContainsKey(phrase)) {
                return false;
            } else {
                _translationSources[phrase] = new Dictionary<string, ITranslationSource>();
                return true;
            }
        }

        private bool DoAddLocalizationSource(string phrase, string locale, ITranslationSource source) {
            if (_translationSources.ContainsKey(phrase)
                && _translationSources[phrase].ContainsKey(locale)
                && _translationSources[phrase][locale] == source) {
                return false;
            }
            DoAddPhrase(phrase);
            var curPhrase = _translationSources[phrase];
            if (curPhrase.ContainsKey(locale) && curPhrase[locale] != source) {
                LogWarn("Multiple localization sources for phrase {0}, locale {1}, old source: {2}, new source: {3}",
                    phrase, locale, curPhrase[locale].mono.name, source.mono.name);
            }
            curPhrase[locale] = source;
            _changedTranslations.Add((phrase, locale));
            return true;
        }

        private bool DoRemoveLocalizationSource(string phrase, string locale, ITranslationSource source) {
            if (_translationSources.ContainsKey(phrase)
                && _translationSources[phrase].ContainsKey(locale)
                && _translationSources[phrase][locale] == source) {
                _translationSources[phrase].Remove(locale);
                _changedTranslations.Add((phrase, locale));
                return true;
            }
            return false;
        }

        private string DoGetPhraseTranslation(string phrase) {
            // LogLog("getphrasetrans curlocale", _currentLocale);
            if (string.IsNullOrEmpty(phrase)) {
                return "Error: phrase is null or empty";
            } else if (string.IsNullOrEmpty(_currentLocale)) {
                return "Error: current locale is null or empty";
            } else if (!_translationSources.ContainsKey(phrase)) {
                return string.Format("Error: phrase '{0}' not found.", phrase);
            } else {
                Locale l = _currentLocale;
                do {
                    if (_translationSources[phrase].ContainsKey(l.Name)) {
                        return _translationSources[phrase][l.Name].GetTranslation(phrase, l.Name);
                    }
                    l = _localeDatabase[l.Parent];
                } while (l != null);
                return string.Format("Error: phrase '{0} has no translation in {1} nor its neutral locales.", phrase, _currentLocale);
            }
        }

        public override void OnServiceRegister() {
            Clear();
            
            // decide current locale
            bool autoDetectLocale = _autoDetectLocale;
            if (!Helpers.IsEditMode) {
                if (_saveLocale) {
                    bool loaded = LoadLocaleFromPrefs();
                    if (loaded) autoDetectLocale = false;
                }
                if (_autoDetectLocale) {
                    DetectAndApplySystemLocale(true);
                }
            }

            // connect all components
            MonoBehaviour[] monos = GameObject.FindObjectsOfType<MonoBehaviour>();
            monos.ToList()
                .Where(m => m is ILocalizationServiceComponent)
                .Select(m => m as ILocalizationServiceComponent).ToList().ForEach(ilc => {
                    ilc.Connect<LocalizationService>();
                }
            );
        }
        public override void OnServiceUnregister() {
            MonoBehaviour[] monos = GameObject.FindObjectsOfType<MonoBehaviour>();
            monos.ToList()
                .Where(m => m is ILocalizationServiceComponent)
                .Select(m => m as ILocalizationServiceComponent).ToList().ForEach(ilc => {
                    ilc.Disconnect<LocalizationService>();
                }
            );
        }

        private void SettleLocaleDatabase() {
            switch (_databasePreset) {
                case DatabasePreset.None:
                    _localeDatabase = LocaleDatabase.EmptyDatabase;
                    break;
                case DatabasePreset.Custom:
                    LoadLocalesDB();
                    break;
                case DatabasePreset.Languages:
                    _localeDatabase = LanguagesData.database;
                    break;
                case DatabasePreset.Locales:
                    _localeDatabase = LocalesData.database;
                    break;
            }
        }

        private void LoadLocalesDB() {
            Stopwatch stopwatch = Stopwatch.StartNew();
            _localeDatabase = new LocaleDatabase();
            string jsonText = null;
            if (_localeDatabaseAsset) {
                jsonText = _localeDatabaseAsset.text;
            }

            if (string.IsNullOrEmpty(jsonText)) {
                LogWarn("cannot load locales database file.");
                return;
            }
            _localeDatabase.LoadFromJson(jsonText);
            stopwatch.Stop();
            LogLog("Locale database loaded in {0:0.0000} seconds.", stopwatch.ElapsedMilliseconds / 1000f);
        }

        private bool DoSetTranslationChanged(string phrase, string lang, ITranslationSource source) {
            if (!_translationSources.ContainsKey(phrase)
                || !_translationSources[phrase].ContainsKey(lang)
                || _translationSources[phrase][lang] != source) {
                return false;
            }

            _changedTranslations.Add((phrase, lang));
            return true;
        }

        private void ProcessChangedTranslations() {
            _changedTranslations.ToList().ForEach((vt) => {
                if (vt.Item2 == _currentLocale && _translationChangeEvents.ContainsKey(vt.Item1)) {
                    _translationChangeEvents[vt.Item1]?.Invoke(vt.Item1, vt.Item2, DoGetPhraseTranslation(vt.Item1));
                }
            });
            _changedTranslations.Clear();
        }

        void Update() {
            if (_changedTranslations.Count() > 0) {
                ProcessChangedTranslations();
            }
        }

        void Awake() {
            Init();
            SettleLocaleDatabase();
        }

        void Start() {
            _currentLocale = _localeDatabase[_currentLocale];
            _localeChangeEvent?.Invoke(_currentLocale);
        }

        public void Init() {
            _localeDatabase = _localeDatabase ?? new LocaleDatabase();
            
            _locales = _locales ?? new List<Locale>();
            _localeChangeEvent = _localeChangeEvent ?? null;

            _translationSources = _translationSources ?? new Dictionary<string, Dictionary<string, ITranslationSource>>();
            _changedTranslations = _changedTranslations ?? new HashSet<(string, string)>();
            _translationChangeEvents = _translationChangeEvents ?? new Dictionary<string, TranslationChangeDelegate>();
        }

        private void Clear() {
            Init();
            _translationSources.Clear();
            _localeChangeEvent = null;
            _translationChangeEvents.Clear();
        }

#if UNITY_EDITOR
        private SortedSet<string> _localeNames;
        public IList<string> localeNames {
            get {
                if (_localeNames == null) ComputeCaches();
                return _localeNames.ToList().AsReadOnly(); 
            }
        }
        private SortedSet<string> _localeEnglishNames;
        public IList<string> localeEnglishNames {
            get {
                if (_localeEnglishNames == null) ComputeCaches();
                return _localeEnglishNames.ToList().AsReadOnly(); 
            }
        }
        void ComputeCaches() {
            _localeNames = new SortedSet<string>(_locales.Select(l => l.Name).ToList());
            _localeEnglishNames = new SortedSet<string>(_locales.Select(l => _localeDatabase.ContainsLocale(l.Name) ? _localeDatabase[l.Name].EnglishName : l.Name).ToList());
        }

        [NonSerialized] TextAsset cachedDBAsset;
        [NonSerialized] DatabasePreset cachedDatabasePreset;
        [NonSerialized] Locale cachedCurrentLocale;
        public void Reset() {
            if (_instanceReady) UnregisterInstance(this);
            Init();
            _localeDatabaseAsset = null;
            _databasePreset = DatabasePreset.Languages;
            if (_instanceReady) RegisterInstance(this);
        }

        public void OnValidate() {
            Init();

            if (cachedDatabasePreset != _databasePreset) {
                cachedDatabasePreset = _databasePreset;
                cachedDBAsset = _localeDatabaseAsset;
                SettleLocaleDatabase();
            } else if (cachedDBAsset != _localeDatabaseAsset) {
                cachedDBAsset = _localeDatabaseAsset;
                if (_databasePreset == DatabasePreset.Custom) {
                    LogWarn("DB Asset file change: {0} -> {1}", cachedDBAsset?.name, _localeDatabaseAsset?.name);
                    LoadLocalesDB();
                }
            }

            if (cachedCurrentLocale != _currentLocale) {
                _currentLocale = _localeDatabase[_currentLocale.Name];
                cachedCurrentLocale = _currentLocale;
                if (_instanceReady && !string.IsNullOrEmpty(_currentLocale)) {
                    _localeChangeEvent?.Invoke(_currentLocale);
                }
            }

            ComputeCaches();
        }
#endif
    }
}
