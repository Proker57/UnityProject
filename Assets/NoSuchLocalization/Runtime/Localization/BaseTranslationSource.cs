using NoSuchStudio.Common;
using NoSuchStudio.Common.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NoSuchStudio.Localization {
    /// <summary>
    /// Base class for classes that provide translations for phrases. The translations
    /// could be serialized in the scene or 
    /// come from a File (<see cref="FileTranslationSource"/>) or from other places.
    /// </summary>
    [ExecuteInEditMode]
    public abstract class BaseTranslationSource : NoSuchMonoBehaviour, ITranslationSource {
        /// <summary>
        /// Populated list of translations this source provides.
        /// </summary>
        [NonSerialized] protected Dictionary<string, Dictionary<string, string>> _translations;
        public Dictionary<string, Dictionary<string, string>> translations {
            get { return _translations; }
        }

        /// <summary>
        /// Whether the source is ready to be connected to the <see cref="LocalizationService"/>.
        /// Unless overriden, it happens in <see cref="OnEnable"/>.
        /// </summary>
        [NonSerialized] protected bool _readyToConnect;

        /// <summary>
        /// Keeps the connection status to different services.
        /// </summary>
        [NonSerialized] protected Dictionary<Type, bool> _connected;

        protected virtual void Init() {
            _connected = _connected ?? new Dictionary<Type, bool>();
            _translations = _translations ?? new Dictionary<string, Dictionary<string, string>>();
        }

        public MonoBehaviour mono {
            get { return this; }
        }

        /// <summary>
        /// Check connection to service.
        /// </summary>
        /// <returns>
        /// Returns true if connected to the service.
        /// </returns>
        public virtual bool IsConnected<ST>() where ST : Service<ST> {
            return _connected.ContainsKey(typeof(ST)) ? _connected[typeof(ST)] : false;
        }

        /// <summary>
        /// Try to connect to the given service. Check <see cref="IsConnected{ST}"/>
        /// to check if the connection attempt was succssful.
        /// </summary>
        public void Connect<ST>() where ST : Service<ST> {
            if (!EditorUtilities.IsInMainStage(gameObject)) return;
            if (!_readyToConnect) return;
            if (!Service<ST>.IsReady) return;
            if (typeof(ST) != typeof(LocalizationService)) return;
            if (IsConnected<ST>()) Disconnect<ST>();
            _translations.ToList().ForEach(kvp => {
                string phrase = kvp.Key;
                var phraseTranslations = kvp.Value;
                phraseTranslations.ToList().ForEach(translation => {
                    LocalizationService.AddLocalizationSource(phrase, translation.Key, this);
                });
            });
            _connected[typeof(ST)] = true;
        }

        /// <summary>
        /// Try to disconnect from the given service. Check <see cref="IsConnected{ST}"/>
        /// to check if the connection attempt was succssful.
        /// </summary>
        public void Disconnect<ST>() where ST : Service<ST> {
            if (!EditorUtilities.IsInMainStage(gameObject)) return;
            if (!_readyToConnect) return;
            if (!Service<ST>.IsReady) return;
            if (typeof(ST) != typeof(LocalizationService)) return;
            _translations.ToList().ForEach(kvp => {
                string phrase = kvp.Key;
                var phraseTranslations = kvp.Value;
                phraseTranslations.ToList().ForEach(translation => {
                    LocalizationService.RemoveLocalizationSource(phrase, translation.Key, this);
                });
            });
            _connected[typeof(ST)] = false;
        }

        void IServiceComponent<LocalizationService>.Connect<ST>() {
            Connect<LocalizationService>();
        }

        void IServiceComponent<LocalizationService>.Disconnect<ST>() {
            Disconnect<LocalizationService>();
        }

        bool IServiceComponent<LocalizationService>.IsConnected<ST>() {
            return IsConnected<LocalizationService>();
        }

        protected virtual void Awake() {
            Init();
        }

        protected virtual void OnEnable() {
            _readyToConnect = true;
            Connect<LocalizationService>();
        }

        protected virtual void OnDisable() {
            Disconnect<LocalizationService>();
            _readyToConnect = false;
        }
        
        /// <summary>
        /// Get translation for "phrase" in "locale" from this translation source.
        /// </summary>
        /// <param name="phrase">phrase to get translation for.</param>
        /// <param name="locale">locale of the translation.</param>
        /// <returns>Translated string if one exists, null otherwise.</returns>
        public virtual string GetTranslation(string phrase, string locale) {
            if (_translations.ContainsKey(phrase) && _translations[phrase].ContainsKey(locale)) {
                return _translations[phrase][locale];
            } else {
                return null;
            }

        }

        /// <summary>
        /// Removes all translations from this translation source.
        /// </summary>
        protected virtual void Reset() {
            Init();
            Disconnect<LocalizationService>();
            _translations.Clear();
            Connect<LocalizationService>();
        }
    }
}
