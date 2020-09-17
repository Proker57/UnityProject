using System;

using UnityEngine;

namespace NoSuchStudio.Localization {
    /// <summary>
    /// Base class for component localizers that depend on a phrase. UI texts are a good example.
    /// Override <see cref="UpdatePhrasedComponent"/> instead of <see cref="UpdateComponent"/> when
    /// inheriting this class.
    /// <see cref="phrase"/> property indicates the phrase to translate.
    /// <see cref="_translation"/> field is the translated phrase in current language. Use it when 
    /// updating the component.
    /// </summary>
    /// <remarks>
    /// For example, a LT component that localizes a text would have LT = LT and CT = Text.
    /// </remarks>
    /// <typeparam name="LT">The component that inherits ComponentLocalizer.</typeparam>
    /// <typeparam name="CT">The component that is localized by LT.</typeparam>
    public abstract class PhrasedComponentLocalizer<LT, CT> : ComponentLocalizer<LT, CT> 
        where CT : Component
        where LT : PhrasedComponentLocalizer<LT, CT> {
        /// <summary>
        /// The phrase for this localized component.
        /// </summary>
        [SerializeField] protected string _phrase;
        public string phrase {
            get {
                return _phrase;
            }
            set {
                Init();
                _phrase = value == null ? "" : value;
                if (IsConnected<LocalizationService>()) {
                    UpdateComponent();
                }
            }
        }

        [NonSerialized] protected string _translation;

        public abstract void UpdatePhrasedComponent();

        public sealed override void UpdateComponent() {
            _translation = LocalizationService.GetPhraseTranslation(_phrase);
            UpdatePhrasedComponent();
        }

        protected virtual void OnTranslationChange(string phrase, Locale locale, string translation) {
            _translation = translation;
            UpdatePhrasedComponent();
        }

        protected override void RegisterToLocalization() {
            LocalizationService.AddLocaleChangeListener(OnLocaleChange);
            LocalizationService.AddTranslationChangeListener(_phrase, OnTranslationChange);
        }

        protected override void UnregisterFromLocalization() {
            LocalizationService.RemoveLocaleChangeListener(OnLocaleChange);
            LocalizationService.RemoveTranslationChangeListener(_phrase, OnTranslationChange);
        }

#if UNITY_EDITOR
        protected override void OnValidate() {
            Init();
            phrase = _phrase;
        }
#endif
    }
}
