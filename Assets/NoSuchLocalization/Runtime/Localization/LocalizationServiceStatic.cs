using System;
using NoSuchStudio.Common.Service;

namespace NoSuchStudio.Localization {
    public partial class LocalizationService : Service<LocalizationService> {

        public const string ComponentMenuPath = "Localization";
        public const string PlayerPrefKeyLocale = "nosuchstudio.localization.currentlocale";

        /// <returns>
        /// Return the default locale of the service, null if the service is not ready.
        /// </returns>
        public static string DefaultLocale {
            get {
                return IsReady ? Instance.defaultLocale : null;
            }
            set {
                if (Instance == null) return;
                Instance.defaultLocale = value;
            }
        }

        /// <summary>
        /// Return the default locale of the service, null if the service is not ready.
        /// </summary>
        [Obsolete("use DefaultLocale instead")]
        public static string DefaultLanguage {
            get {
                return DefaultLocale;
            }
            set {
                DefaultLocale = value;
            }
        }

        /// <returns>
        /// Return the current locale of the service, null if the service is not ready.
        /// </returns>
        [Obsolete("use CurrentLocale instead")]
        public static Locale CurrentLanguage {
            get { return CurrentLocale; }
            set {
                CurrentLocale = value;
            }
        }

        /// <returns>
        /// Return the current locale of the service, null if the service is not ready.
        /// </returns>
        public static Locale CurrentLocale {
            get { return IsReady ? Instance.currentLocale : null; }
            set {
                if (Instance == null) return;
                Instance.currentLocale = value;
            }
        }


        /// <summary>
        /// Add an event listener for when the current locale changes.
        /// </summary>
        /// <param name="action">Delegate to run when current Locale changes.</param>
        [Obsolete("use AddLocaleChangeListener instead")]
        public static void AddLanguageChangeListener2(LocaleChangeDelegate action) {
            if (IsReady) Instance.DoAddLocaleChangeListener(action);
        }

        /// <summary>
        /// Remove an event listener for when the current locale changes.
        /// </summary>
        /// <param name="action">Delegate to run when current Locale changes.</param>
        [Obsolete("use RemoveLocaleChangeListener instead")]
        public static void RemoveLanguageChangeListener2(LocaleChangeDelegate action) {
            if (IsReady) Instance.DoRemoveLocaleChangeListener(action);
        }

        /// <summary>
        /// Add an event listener for when the current locale changes.
        /// </summary>
        public static void AddLocaleChangeListener(LocaleChangeDelegate action) {
            if (IsReady) Instance.DoAddLocaleChangeListener(action);
        }

        /// <summary>
        /// Remove an event listener for when the current locale changes.
        /// </summary>
        public static void RemoveLocaleChangeListener(LocaleChangeDelegate action) {
            if (IsReady) Instance.DoRemoveLocaleChangeListener(action);
        }

        public static void AddTranslationChangeListener(string phrase, TranslationChangeDelegate action) {
            if (IsReady) Instance.DoAddTranslationChangeListener(phrase, action);
        }

        public static void RemoveTranslationChangeListener(string phrase, TranslationChangeDelegate action) {
            if (IsReady) Instance.DoRemoveTranslationChangeListener(phrase, action);
        }

        // public static bool AddPhrase(string phrase)
        // {
        //     return Instance.DoAddPhrase(phrase);
        // }

        /// <summary>
        /// Called by translation sources to make their data available to the localization system.
        /// <see cref="ITranslationSource"/>
        /// </summary>
        /// <param name="phrase">The phrase for which there is a translation.</param>
        /// <param name="locale">The locale for which there is a translation.</param>
        /// <param name="source">The translation source providing the translation of phrase in the given locale</param>
        public static void AddLocalizationSource(string phrase, string locale, ITranslationSource source) {
            if (IsReady) Instance.DoAddLocalizationSource(phrase, locale, source);
        }

        /// <summary>
        /// Called by translation sources to remove their data from the localization service when they go offline (get disabled or destroyed).
        /// <see cref="ITranslationSource"/>
        /// </summary>
        /// <param name="phrase">The phrase for which there is a translation.</param>
        /// <param name="locale">The locale for which there is a translation.</param>
        /// <param name="source">The translation source providing the translation of phrase in the given locale</param>
        public static void RemoveLocalizationSource(string phrase, string locale, ITranslationSource source) {
            if (IsReady) Instance.DoRemoveLocalizationSource(phrase, locale, source);
        }

        /// <summary>
        /// Get the translation string for a given phrase.
        /// </summary>
        /// <param name="phrase">phrase to look up.</param>
        /// <returns>The translation if phrase if found. And error string otherwise.</returns>
        public static string GetPhraseTranslation(string phrase) {
            if (!IsReady) return "Error: no active and enabled Localization instances in scene.";
            return Instance.DoGetPhraseTranslation(phrase);
        }

        /// <summary>
        /// Set the <see cref="currentLocale"/> to the system locale if the system locale is recognized and has been enabled in the localization service.
        /// </summary>
        /// <param name="useDefaultIfFailed"></param>
        /// <returns>true if system locale was successfully applied, false otherwise.</returns>
        public bool DetectAndApplySystemLocale(bool useDefaultIfFailed = false) {
            return IsReady ? Instance.DoDetectAndApplySystemLocale(useDefaultIfFailed) : false;
        }

        /// <summary>
        /// <para>Get the currently loaded locale database. Avoid modifying the database from code.</para>
        /// <para>To modify the database, update the backing json file and reload the database from the Editor for <see cref="LocalizationService"/></para>
        /// </summary>
        /// <returns>The current locale database.</returns>
        public LocaleDatabase GetLocaleDatabase() {
            return IsReady ? Instance.localeDatabase : null;
        }
    }
}
