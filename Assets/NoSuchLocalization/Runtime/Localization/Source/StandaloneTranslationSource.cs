using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NoSuchStudio.Localization.Source {
    /// <summary>
    /// Use this class to translate phrases in Unity Editor.
    /// </summary>
    [ExecuteInEditMode]
    public partial class StandaloneTranslationSource : BaseTranslationSource, ITranslationSource
        , ISerializationCallbackReceiver {
        [Serializable]
        struct StringPair {
            [LocaleProperty(false)] public string locale;
            public string translation;
            public StringPair(string k, string v) {
                locale = k;
                translation = v;
            }
            public void SetKey(string k) {
                locale = k;
            }
        }

        [Serializable]
        struct TranslationData {
            public string phrase;
            public List<StringPair> translations;
            public TranslationData(string p, List<StringPair> ts) {
                phrase = p;
                translations = ts;
            }

            public void SetLang(string p) {
                phrase = p;
            }

            public void SetTranslations(List<StringPair> ts) {
                translations = ts;
            }
        }
        [SerializeField] private List<TranslationData> _translationList;


        [NonSerialized] protected bool _dataChanged;

        protected override void Init() {
            base.Init();
            _translationList = _translationList ?? new List<TranslationData>();
        }

        public bool AddPhrase(string phrase) {
            if (_translations.ContainsKey(phrase)) return false;
            _translations[phrase] = new Dictionary<string, string>();
            return true;
        }

        public bool AddTranslation(string phrase, string lang, string value) {
            if (_translations.ContainsKey(phrase) && _translations[phrase].ContainsKey(lang) && _translations[phrase][lang] == value) return false;
            AddPhrase(phrase);
            _translations[phrase][lang] = value;
            _dataChanged = true;
            return true;
        }

        public bool RemoveTranslation(string phrase, string lang, string value) {
            if (_translations.ContainsKey(phrase) && _translations[phrase].ContainsKey(lang) && _translations[phrase][lang] == value) {
                _translations[phrase].Remove(lang);
                _dataChanged = true;
                return true;
            }
            return false;
        }

        protected virtual void Update() {
            if (_dataChanged && IsConnected<LocalizationService>()) {
                Connect<LocalizationService>();
            }
        }

        public void OnValidate() {
            Init();
            if (IsConnected<LocalizationService>()) {
                Connect<LocalizationService>();
            }
        }

        public void OnBeforeSerialize() {
            Init();
            _translationList.Clear();

            foreach (var kvp in _translations) {
                _translationList.Add(new TranslationData(kvp.Key, kvp.Value.ToList().Select(kv => new StringPair(kv.Key, kv.Value)).ToList()));
            }
        }

        // TODO proper diff of translations
        public void OnAfterDeserialize() {
            Init();
            _translations.Clear();

            int idCounter = 0;
            for (int i = 0; i < _translationList.Count(); i++) {
                TranslationData td = _translationList[i];
                string phrase = td.phrase;
                while (string.IsNullOrEmpty(phrase) || _translations.ContainsKey(phrase)) {
                    phrase = string.Format("phrase_{0}", idCounter++);
                }
                _translations[phrase] = new Dictionary<string, string>();
                int langCounter = 0;
                td.translations.ForEach(trans => {
                    string locale = trans.locale;
                    while (string.IsNullOrEmpty(locale) || _translations[phrase].ContainsKey(locale)) {
                        locale = string.Format("locale_{0}", langCounter++);
                    }
                    _translations[phrase][locale] = trans.translation;
                });
            }
        }
    }
}
