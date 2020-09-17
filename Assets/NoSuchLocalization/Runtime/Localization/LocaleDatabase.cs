using Newtonsoft.Json;
using NoSuchStudio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NoSuchStudio.Localization {

    /// <summary>
    /// Class for managing locales. This class loads the locales from a backing json file, validates the locales and provides utility methods for handling locales.
    /// Each LocaleDatabase can be loaded only once. To load another json, create a new instance.
    /// </summary>
    /// <remarks>
    /// There are two locale database files included with the Unity package.
    /// <para>"alllocales.json" contains a large list of all locales (~850). It includes languages like "English" as well as specific locales
    /// like "United States English" and "United Kingdom English". This is intended for more in-depth use cases.</para>
    /// <para>"neutrallocales.json" includes only the neutral locales a.k.a languages. For example there is only "English" in it without the region specifications.
    /// This is suitable for most cases.
    /// </para>
    /// </remarks>
    public class LocaleDatabase {

        public static readonly LocaleDatabase EmptyDatabase;

        static LocaleDatabase() {
            EmptyDatabase = new LocaleDatabase();
            EmptyDatabase.loaded = true;
        }

        public class NormalizedLocaleNameComparer : Comparer<string> {
            public override int Compare(string x, string y) {
                return NormalizeAndCompare(x, y);
            }
        }

        public static int NormalizeAndCompare(string x, string y) {
            var xnorm = Locale.NormalizeLocaleName(x);
            var ynorm = Locale.NormalizeLocaleName(y);
            return xnorm.CompareTo(ynorm);
        }

        private bool loaded = false;
        private SortedDictionary<string, Locale> _allLocalesByName;
        private IList<Locale> _allLocalesListByName;
        private SortedDictionary<string, Locale> _allLocalesByEnglishName;
        private IList<Locale> _allLocalesListByEnglishName;
        private SortedDictionary<string, Locale> _neutralLocalesByName;
        private IList<Locale> _neutralLocalesListByName;
        private SortedDictionary<string, Locale> _neutralLocalesByEnglishName;
        private IList<Locale> _neutralLocalesListByEnglishName;
        private SortedDictionary<string, Locale> _specificLocalesByName;
        private IList<Locale> _specificLocalesListByName;
        private SortedDictionary<string, Locale> _specificLocalesByEnglishName;
        private IList<Locale> _specificLocalesListByEnglishName;

        private Dictionary<string, string> _localePathCache;
        public string GetLocalePathFromCache(Locale l) {
            return _localePathCache[l.NormalizedName];
        }
        private Dictionary<string, string> _localeEnglishPathCache;
        public string GetLocaleEnglishPathFromCache(Locale l) {
            return _localeEnglishPathCache[l.NormalizedName];
        }
        private Dictionary<string, List<string>> _parentToChildren;
        
        /// <summary>
        /// Returns list of all locales that are curretnly loaded, sorted in ascending alphbetic (a-z) order of their name.
        /// </summary>
        public IList<Locale> allLocalesByName {
            get {
                return _allLocalesListByName;
            }
        }

        /// <summary>
        /// Returns list of all locales that are curretnly loaded, sorted in ascending alphbetic (a-z) order of their <b>english</b> name.
        /// </summary>
        public IList<Locale> allLocalesByEnglishName {
            get {
                /*var ret = new List<Locale>(_allLocales.Count);
                var sortedNames = _englishNameToName.Values.ToList();
                sortedNames.ForEach(n => ret.Add(_allLocales[n]));
                return ret;*/
                return _allLocalesListByEnglishName;
            }
        }

        /// <summary>
        /// Returns list of all neutral locales that are curretnly loaded, sorted in ascending alphbetic (a-z) order of their name.
        /// </summary>
        public IList<Locale> neutralLocalesByName {
            get {
                return _neutralLocalesByName.Values.ToList();
            }
        }

        /// <summary>
        /// Returns list of all neutral locales that are curretnly loaded, sorted in ascending alphbetic (a-z) order of their <b>english</b> name.
        /// </summary>
        public IList<Locale> neutralLocalesByEnglishName {
            get {
                /*var ret = new List<Locale>(_neutralLocales.Count);
                var sortedNames = _englishNameToName.Values.ToList();
                sortedNames.Where(n => _neutralLocales.ContainsKey(n)).ToList().ForEach(n => ret.Add(_allLocales[n]));
                return ret;*/
                return _neutralLocalesListByEnglishName;
            }
        }

        /// <summary>
        /// Returns list of all specific locales that are curretnly loaded, sorted in ascending alphbetic (a-z) order of their name.
        /// </summary>
        public IList<Locale> specificLocalesByName {
            get {
                return _specificLocalesListByName;
            }
        }

        /// <summary>
        /// Returns list of all specific locales that are curretnly loaded, sorted in ascending alphbetic (a-z) order of their <b>english</b> name.
        /// </summary>
        public IList<Locale> specificLocalesByEnglishName {
            get {
                return _specificLocalesListByEnglishName;
            }
        }
 
        /// <summary>
        /// Whether the locale database contains a locale. 
        /// </summary>
        /// <param name="localeName">The code name or english name of the locale. For example any of these 
        /// ("en", "EN", "English", "english") will return true if the database contain the neutral English locale.</param>
        /// <returns>true if a locale with the provided code name or english name exists in the database.</returns>
        public bool ContainsLocale(string localeName) {
            if (string.IsNullOrEmpty(localeName)) return false;
            var normalizedName = Locale.NormalizeLocaleName(localeName);
            return _allLocalesByName.ContainsKey(normalizedName) || _allLocalesByEnglishName.ContainsKey(normalizedName);
        }

        /// <summary>Retrieve a locale by its code name or english name.</summary>
        /// <param name="locale">The name of the locale. The name will be normalized. For example any of these
        /// ("en" or "EN" or "English" or "english") will return the neutral English locale.</param>
        /// <returns>Locale with the given code or english name if one exists, null otherwise.</returns>
        public Locale this[string locale]
        {
            get {
                string key = Locale.NormalizeLocaleName(locale);
                if (_allLocalesByName.ContainsKey(key)) return _allLocalesByName[key];
                if (_allLocalesByEnglishName.ContainsKey(key)) return _allLocalesByEnglishName[key];
                return Locale.EmptyLocale;
            }
        }

        private void ComputeSortedListsAndCaches() {
            _allLocalesListByName = _allLocalesByName.Values.ToList().AsReadOnly();
            _allLocalesListByEnglishName = _allLocalesByEnglishName.Values.ToList().AsReadOnly();
            _neutralLocalesListByName = _neutralLocalesByName.Values.ToList().AsReadOnly();
            _neutralLocalesListByEnglishName = _neutralLocalesByEnglishName.Values.ToList().AsReadOnly();
            _specificLocalesListByName = _specificLocalesByName.Values.ToList().AsReadOnly();
            _specificLocalesListByEnglishName = _specificLocalesByEnglishName.Values.ToList().AsReadOnly();

            _localePathCache = new Dictionary<string, string>();
            _localeEnglishPathCache = new Dictionary<string, string>();
            _allLocalesByName.Values.ToList().ForEach(l => {
                _localePathCache[l.NormalizedName] = GetLocaleNamePath(l);
                _localeEnglishPathCache[l.NormalizedName] = GetLocaleEnglishNamePath(l);
            });
        }

        public LocaleDatabase() {
            _allLocalesByName = new SortedDictionary<string, Locale>();
            _allLocalesByEnglishName = new SortedDictionary<string, Locale>();
            _neutralLocalesByName = new SortedDictionary<string, Locale>();
            _neutralLocalesByEnglishName = new SortedDictionary<string, Locale>();
            _specificLocalesByName = new SortedDictionary<string, Locale>();
            _specificLocalesByEnglishName = new SortedDictionary<string, Locale>();
            _parentToChildren = new Dictionary<string, List<string>>();
            ComputeSortedListsAndCaches();
        }

        /// <summary>
        /// Remove all locales. This instance of LocaleDatabase will be empty after this call.
        /// </summary>
        /// <remarks>
        /// Intended for internal use only. 
        /// LocaleDatabase instances are immutable once loaded.
        /// </remarks>
        private void Clear() {
            _allLocalesByName.Clear();
            _allLocalesByEnglishName.Clear();
            _neutralLocalesByName.Clear();
            _neutralLocalesByEnglishName.Clear();
            _specificLocalesByName.Clear();
            _specificLocalesByEnglishName.Clear();
            _parentToChildren.Clear();
            ComputeSortedListsAndCaches();
        }

        /// <param name="l"></param>
        /// <returns>The neutral locale (i.e. language) for the provided specific locale. If the provided locale is already neutral, it will be returned.</returns>
        public Locale GetNeutralLocale(Locale l) {
            return this[l.LanguageInName];
        }

        /// <summary>
        /// Returns a string with names of locales from the most general to the most specific provided to the call.
        /// </summary>
        /// <param name="locale">Locale to get path for.</param>
        /// <param name="delimiter">Delimiter between the locale names.</param>
        /// <returns>For English (Australia) it returns: "en/en-AU"</returns>
        public string GetLocaleNamePath(Locale locale, string delimiter = "/"/*, string suffixIfHasChildren = ""*/) {
            var curLocale = locale;
            string res = curLocale.Name;
            while (!string.IsNullOrEmpty(curLocale.Parent)) {
                curLocale = this[curLocale.Parent];
                res = curLocale.Name + delimiter + res;
            }

            /*if (locale.IsNeutral && !string.IsNullOrEmpty(suffixIfHasChildren)) {
                res = res + delimiter + suffixIfHasChildren;
            }*/

            return res;
        }

        /// <summary>
        /// Similar to <see cref="GetLocaleNamePath(Locale, string)"/>
        /// </summary>
        /// <param name="locale">Locale to get path for.</param>
        /// <param name="delimiter">Delimiter between the locale names.</param>
        /// <returns>For English (Australia) it returns: "English/English (Australia)"</returns>
        public string GetLocaleEnglishNamePath(Locale locale, string delimiter = "/"/*, string suffixIfHasChildren = ""*/) {
            var curLocale = locale;
            string res = curLocale.EnglishName;
            while (!string.IsNullOrEmpty(curLocale.Parent)) {
                curLocale = this[curLocale.Parent];
                res = curLocale.EnglishName + delimiter + res;
            }

            /*if (locale.IsNeutral && !string.IsNullOrEmpty(suffixIfHasChildren)) {
                res = res + delimiter + suffixIfHasChildren;
            }*/

            return res;
        }

        /// <param name="l">query locale</param>
        /// <returns>true if locale has child locales, false otherwise.</returns>
        public bool HasChildren(Locale l) {
            var key = l.NormalizedName;
            return _parentToChildren.ContainsKey(key) && _parentToChildren[key].Count > 0;
        }

        /// <param name="l">The neutral locale (i.e. language) for which to get specific locales.</param>
        /// <param name="includeNeutral">if true, the first locale in the list will be the provided neutral locale.</param>
        /// <returns>All the specific locales for the provided neutral locale.</returns>
        public List<Locale> GetSpecificLocales(Locale l, bool includeNeutral = false) {
            if (!l.IsNeutral) {
                throw new ApplicationException(string.Format("GetSpecificLocales must be called with neutral locales. {0} is specific.", l));
            }

            var key = l.NormalizedName;
            var ret = new List<Locale>();
            if (includeNeutral) ret.Add(l);
            _parentToChildren[key].ForEach(ln => ret.Add(this[ln]));
            return ret;
        }

        /// <summary>
        /// Validation includes:
        /// <ul>
        /// <li>Locales parent-child relationships don't form a loop.</li>
        /// <li>All locales have a valid parent.</li>
        /// </ul>
        /// </summary>
        /// <returns>true if validation passes, false otherwise.</returns>
        private bool ValidateDatabase() {
            // No Loops Validation
            bool noLoops = true;
            _allLocalesByName.Values.ToList().ForEach(l => {
                int depth = 0;
                Locale cur = l;
                while(depth < 5 && !string.IsNullOrEmpty(cur.Parent)) {
                    depth++;
                    cur = this[cur.Parent];
                }

                if (depth >= 5) {
                    Debug.LogErrorFormat("Locale parent loop starting with {0}", l);
                    noLoops = false;
                }
            });
            // Parents Validation: parents exist and are neutral
            bool validParents = true;
            _allLocalesByName.Values.ToList().ForEach(l => {
                if (string.IsNullOrEmpty(l.Parent)) {
                    if (!l.IsNeutral) {
                        validParents = false;
                        Debug.LogErrorFormat("Locale {0} is not neutral but doesn't have a parent.", l);
                    }
                } else {
                    if (!ContainsLocale(l.Parent)) {
                        validParents = false;
                        Debug.LogErrorFormat("Locale {0} has a non-existing locale as its parent: '{1}'", l, l.Parent);
                    } else if (!this[l.Parent].IsNeutral  && l.IsNeutral) {
                        validParents = false;
                        Debug.LogErrorFormat("Neutral locale '{0}' has non-neutral parent '{1}'", l, l.Parent);
                    }
                }
            });
            
            bool isValid = validParents
                && noLoops;
            if (!isValid) {
                int rules = 2;
                int failedRules = 0;
                StringBuilder sb = new StringBuilder();
                if (!noLoops) {
                    failedRules++;
                    sb.AppendLine("Loops detected among locales.");
                }
                if (!validParents) {
                    failedRules++;
                    sb.AppendLine("Some locales don't have valid neutral parents.");
                }
                UnityObjectLoggerExt.LogError<LocaleDatabase>("validation failed ({0} of {1} rules failed): {2}", failedRules, rules, sb);
            } else {
                UnityObjectLoggerExt.LogLog<LocaleDatabase>("validation passed.");
            }
            return isValid;
        }

        /// <summary>
        /// Load database from the provided json string. If the database is already loaded, doesn't do anything.
        /// </summary>
        /// <param name="jsonText">json containing all locales.</param>
        internal void LoadFromJson(string jsonText) {
            // Clear();
            var loadedLocales = JsonConvert.DeserializeObject<List<Locale>>(jsonText);
            LoadFromCollection(loadedLocales, true);
            UnityObjectLoggerExt.LogLog<LocaleDatabase>("loaded locale db: {0} entries\n{1}", _allLocalesByName.Count, _allLocalesByName.Values.ToList().ToStringExt());
        }

        /// <summary>
        /// Load database from the provided list of locales. If the database is already loaded, doesn't do anything.
        /// </summary>
        /// <param name="locales">list of locales.</param>
        internal void LoadFromCollection(List<Locale> locales, bool validate = false) {
            if (loaded) {
                UnityObjectLoggerExt.LogLog<LocaleDatabase>("db already loaded with {0} entries. LocaleDatabases are immutable once loaded.", _allLocalesByName.Count, _allLocalesByName.Values.ToList().ToStringExt());
                return;
            }
            loaded = true;
            // Clear();
            locales.ForEach(l => {
                var normalizedName = l.NormalizedName;
                var normalizedEnglishName = l.NormalizedEnglishName;
                if (_allLocalesByName.ContainsKey(normalizedName)) return;
                _allLocalesByName.Add(normalizedName, l);
                _allLocalesByEnglishName.Add(normalizedEnglishName, l);
                if (l.IsNeutral) {
                    _neutralLocalesByName.Add(normalizedName, l);
                    _neutralLocalesByEnglishName.Add(normalizedEnglishName, l);
                } else {
                    _specificLocalesByName.Add(normalizedName, l);
                    _specificLocalesByEnglishName.Add(normalizedEnglishName, l);
                }

                if (!string.IsNullOrEmpty(l.Parent)) {
                    var normalizedParentName = Locale.NormalizeLocaleName(l.Parent);
                    if (!_parentToChildren.ContainsKey(normalizedParentName)) _parentToChildren.Add(normalizedParentName, new List<string>());
                    _parentToChildren[normalizedParentName].Add(normalizedName);
                }
            });

            if (validate && !ValidateDatabase()) {
                Clear();
            } else {
                ComputeSortedListsAndCaches();
            }
        }

        public void Dump() {
            Debug.Log(_allLocalesByName.Values.ToList().ToStringExt());
        }
    }
}
