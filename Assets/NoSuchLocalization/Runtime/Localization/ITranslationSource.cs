namespace NoSuchStudio.Localization {
    /// <summary>
    /// Interface for all classes that act as translation sources for <see cref="LocalizationService"/>.
    ///</summary>
    public interface ITranslationSource : ILocalizationServiceComponent {
        /// <returns>
        /// Returns the translation for the given phrase in the given language. null if a translation does not exist.
        ///</returns>
        string GetTranslation(string phrase, string language);
    }
}
