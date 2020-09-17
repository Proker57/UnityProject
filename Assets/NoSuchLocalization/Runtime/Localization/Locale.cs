using Newtonsoft.Json;
using System;
using UnityEngine;

namespace NoSuchStudio.Localization {
    /// <summary>
    /// A Locale is a language or a language plus a region. To learn more read the related documentation page from
    /// For example:
    ///  - en-US is the locale for the version of English language that is spoken in the United States (US).
    ///  - en-GB is the locale for the version of English language that is spoken in the United Kingdom (UK).
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public struct Locale : IEquatable<Locale> {

        public readonly static Locale EmptyLocale = new Locale(null, null, null, false, null, null, false);
        
        [SerializeField][JsonProperty] private string name;
        [JsonProperty] private string englishName;
        [JsonProperty] private string nativeName;
        [JsonProperty] private string iso;
        [JsonProperty] private string parent;
        [JsonProperty(PropertyName="rtl")] private bool isRTL;
        [JsonProperty(PropertyName="neutral")] private bool isNeutral;

        /// <summary>
        /// Maps locale name to a canonical version to be used as keys for dictionaries.
        /// </summary>
        public static string NormalizeLocaleName(string localeName) {
            return string.IsNullOrEmpty(localeName) ? "" : localeName.ToLower();
        }

        /// <summary>
        /// Get the normalized name of the locale. It is the suggested form to get a unique name for a locale (e.g. for use as keys in a dictionary)
        /// </summary>
        public string NormalizedName {
            get { return NormalizeLocaleName(name); }
        }

        /// <summary>
        /// Code name of the locale.
        /// </summary>
        public string Name {
            get { return name; }
        }

        /// <summary>
        /// Unique normalized english name of the locale. Suitable for keys of a dictionary.
        /// </summary>
        public string NormalizedEnglishName {
            get { return NormalizeLocaleName(englishName); }
        }

        /// <summary>
        /// English name of the locale.
        /// </summary>
        public string EnglishName {
            get { return englishName; }
        }

        /// <summary>
        /// Native name of the locale.
        /// </summary>
        public string NativeName {
            get { return nativeName; }
        }

        /// <summary>
        /// ISO language code for the locale (is not unique).
        /// </summary>
        public string ISO {
            get { return iso; }
        }

        public string Parent {
            get { return parent; }
        }

        public bool IsNeutral {
            get { return isNeutral; }
        }
        /// <summary>
        /// Whether the locale is right-to-left.
        /// </summary>
        public bool IsRTL {
            get { return isRTL; }
        }

        internal Locale(string name, string englishName, string nativeName, bool isNeutral, string parent, string iso, bool isRTL) {
            this.name = name;
            this.englishName = englishName;
            this.nativeName = nativeName;
            this.isNeutral = isNeutral;
            this.parent = parent;
            this.iso = iso;
            this.isRTL = isRTL;
        }

        /// <summary>
        /// Get the language portion of the code name. 
        /// For United States English (en-US), English (en) will be returned.
        /// </summary>
        public string LanguageInName {
            get {
                if (isNeutral) return name;

                int dashIndex = name.LastIndexOf('-');
                if (dashIndex < 0) {
                    Debug.LogErrorFormat("specific locale with no dash! {0}", ToString());
                }
                return name.Substring(0, dashIndex);
            }
        }

        /// <summary>
        /// Get the language portion of the code name. 
        /// For United States English (en-US), United States (US) will be returned.
        /// </summary>
        public string RegionInName {
            get {
                int dashIndex = name.LastIndexOf('-');
                return dashIndex >= 0 ? name.Substring(dashIndex + 1) : "";
            }
        }

        /*public string LanguageInEnglishName {
            get {
                if (isNeutral) return englishName;
                if (!string.IsNullOrEmpty(parentName)) {

                }
                int parantheseOpenIndex = englishName.IndexOf('(');
                Assert.IsTrue(parantheseOpenIndex > 0);
                return englishName.Substring(0, parantheseOpenIndex);
            }
        }*/

        /// <summary>
        /// Get the language portion of the english name. 
        /// For "English (United States)", "United States" will be returned.
        /// </summary>
        public string RegionInEnglishName {
            get {
                if (isNeutral) return "";
                int parantheseOpenIndex = englishName.IndexOf('(');
                int parantheseCloseIndex = englishName.IndexOf(')');
                if (parantheseOpenIndex < 0 || parantheseCloseIndex < 0) {
                    Debug.LogError("WTF " + this.ToString());
                }
                var inParens = englishName.Substring(parantheseOpenIndex + 1, parantheseCloseIndex - parantheseOpenIndex - 1);
                int commaIndex = inParens.LastIndexOf(",");
                var region = inParens;
                if (commaIndex >= 0) {
                    region = inParens.Substring(commaIndex + 1);
                } 
                return region;
            }
        }

        public bool Equals(Locale other) {
            return other.name == name;
        }

        public override string ToString() {
            return name;
        }

        public string ToDebugString() {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}", name, englishName, nativeName, isNeutral, iso, isRTL);
        }

        public static implicit operator string(Locale l) {
            return l.name; 
        }

        public static implicit operator Locale(string str) { 
            if (LocalizationService.IsReady && LocalizationService.Instance.localeDatabase.ContainsLocale(str)) {
                return LocalizationService.Instance.localeDatabase[str];
            } else {
                return new Locale(str, "_", "_", false, "_", "_", false);
            }
        }

    }
}
