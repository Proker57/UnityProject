using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;

namespace NoSuchStudio.Localization.Editor {
    /// <summary>
    /// Custom property drawer for <see cref="Locale"/>. Let's you select the Locale from a selection menu.
    /// All locales currently loaded by <see cref="LocalizationService"/> are available to choose from.
    /// </summary>
    /// <remarks>
    /// By using <see cref="LocalePropertyAttribute"/>, you can tell the drawer to include either all loaded locales or just
    /// the ones added by the user.
    /// </remarks>
    [CustomPropertyDrawer(typeof(Locale))]
    public class LocalePropertyDrawer : PropertyDrawer {
        /// <summary>
        /// Extract the name of a locale from a serialized property. 
        /// </summary>
        /// <param name="property">Can be either a string property or a generic property.</param>
        /// <returns>The name of the locale.</returns>
        public static string LocaleNameFromProperty(SerializedProperty property) {
            if (property.propertyType == SerializedPropertyType.Generic) {
                return property.FindPropertyRelative("name").stringValue;
            } else if (property.propertyType == SerializedPropertyType.String) {
                return property.stringValue;
            } else {
                return null;
            }
        }

        /// <summary>
        /// Extracts the locale name from the serialized property and looks it up in the database.
        /// </summary>
        /// <param name="property">Can be either a string property or a generic property.</param>
        /// <param name="localeDB">The localedatabase.</param>
        /// <returns>The locale from the property if it exists in the locale database, null otherwise.</returns>
        public static Locale LocaleFromProperty(SerializedProperty property, LocaleDatabase localeDB) {
            string localeName = LocaleNameFromProperty(property);
            if (!string.IsNullOrEmpty(localeName)) {
                return localeDB[localeName];
            }
            return null;
        }

        /// <summary>
        /// Save the locale to a serialized property.
        /// </summary>
        /// <param name="locale">Locale to be written to the serialized property.</param>
        /// <param name="property">Can be either a string property or a generic property.</param>
        public static void LocaleToProperty(Locale locale, SerializedProperty property) {
            if (property.propertyType == SerializedPropertyType.String) {
                property.stringValue = locale.Name;
            } else if (property.propertyType == SerializedPropertyType.Generic) {
                property.FindPropertyRelative("name").stringValue = locale.Name;
            }
        }

        public static string GetLocaleFlatDisplayName(LocaleDatabase localeDB, Locale l) {
            DisplayMode ldm = LocalizationEditorSettings.displayMode;
            return ldm == DisplayMode.Code ? l.Name : l.EnglishName;
        }

        public static string GetLocaleMenuDisplayName(LocaleDatabase localeDB, Locale l) {
            DisplayMode ldm = LocalizationEditorSettings.displayMode;
            var ret = ldm == DisplayMode.Code ? localeDB.GetLocalePathFromCache(l) : localeDB.GetLocaleEnglishPathFromCache(l);
            return ret;
        }

        public static IList<Locale> GetLocaleListByName(LocaleDatabase localeDB, IList<string> names) {
            return names.Select(ln => localeDB[ln]).ToList();
        }
        public static IList<Locale> GetLocaleListByEnglishName(LocaleDatabase localeDB, IList<string> englishNames) {
            return englishNames.Select(len => localeDB[len]).ToList();
        }

        public static void MenuDropdownLocaleField(LocalizationService ls, bool filterToAvailable, Rect position, SerializedProperty property, GUIContent label, bool flat = false) {
            var localeDB = ls.localeDatabase;
            DisplayMode ldm = LocalizationEditorSettings.displayMode;
            IList<Locale> availableLocales;
            if (filterToAvailable) {
                availableLocales = ldm == DisplayMode.Code
                    ? GetLocaleListByName(localeDB, ls.localeNames)
                    : GetLocaleListByEnglishName(localeDB, ls.localeEnglishNames);
            } else {
                availableLocales = ldm == DisplayMode.Code ? localeDB.allLocalesByName : localeDB.allLocalesByEnglishName;
            }

            if (availableLocales.Count == 0) {
                EditorGUI.LabelField(position, property.displayName, "No locales to choose from.");
            } else {
                string curLocaleName = LocaleNameFromProperty(property);
                if (string.IsNullOrEmpty(curLocaleName)) {
                    LocaleToProperty(availableLocales[0], property);
                    curLocaleName = LocaleNameFromProperty(property);
                }
                Locale curLocaleFromDB = localeDB[curLocaleName];
                if (curLocaleFromDB != null) {
                    int index = availableLocales.IndexOf(curLocaleFromDB);
                    if (index >= 0) {
                        Func<LocaleDatabase, Locale, string> displayNameFunc = (flat ? (Func<LocaleDatabase, Locale, string>)GetLocaleFlatDisplayName : GetLocaleMenuDisplayName);
                        index = EditorGUI.Popup(position, property.displayName, index, availableLocales.Select(l => {
                            return displayNameFunc(localeDB, l) + (localeDB.HasChildren(l) ? "/*neutral*" : "");
                            }).ToArray());
                        var locale = availableLocales[index];
                        LocaleToProperty(locale, property);
                    } else {
                        // Debug.LogFormat("locale {0} not in list [{1}] [{2}]", curLocaleFromDB, string.Join(", ", availableLocales), string.Join(", ", LocalizationService.Instance.locales));
                        // locale in database but not in the available locales
                        var buttonRect = EditorGUI.PrefixLabel(position, new GUIContent(property.displayName));
                        GUIContent buttonContent = new GUIContent("Reset", string.Format("'{0}' is not in localization service's list of locales.", curLocaleName));
                        if (GUI.Button(buttonRect, buttonContent)) {
                            var locale = availableLocales[0];
                            LocaleToProperty(locale, property);
                        }
                    }
                } else {
                    var buttonRect = EditorGUI.PrefixLabel(position, new GUIContent(property.displayName));
                    GUIContent buttonContent = new GUIContent("Reset", string.Format("'{0}' is not in the current locale database.", curLocaleName));
                    if (GUI.Button(buttonRect, buttonContent)) {
                        var locale = availableLocales[0];
                        LocaleToProperty(locale, property);
                    }
                }
            }
        }

        /*public static void DoubleDropdownLocaleField(LocaleDatabase localeDB, Rect position, SerializedProperty property, GUIContent label) {
            var neutralLocaleNames = localeDB.neutralLocalesByName.Select(l => l.Name).ToList();
            var neutralLocaleEnglishNames = localeDB.neutralLocalesByEnglishName.Select(l => l.EnglishName).ToList();
            var specificLocaleNames = localeDB.specificLocalesByName.Select(l => l.Name).ToList();
            var specificLocaleEnglishNames = localeDB.specificLocalesByEnglishName.Select(l => l.EnglishName).ToList();
            var allLocaleNames = localeDB.allLocalesByName.Select(l => l.Name).ToList();
            LocaleDisplayMode ldm = LocalizationEditorSettings.displayMode;
            if (allLocaleNames.Count == 0) {
                EditorGUI.LabelField(position, property.displayName, "No locales to choose from.");
            } else {
                EditorGUI.LabelField(position, property.displayName);
                string curLocaleName = LocaleNameFromProperty(property);
                Locale curLocale = localeDB[curLocaleName];
                if (curLocale == null) curLocale = localeDB.allLocales[0];
                var curNeutralLocaleName = curLocale.LanguageInName;
                int prevNeutralIndex = Mathf.Max(0, neutralLocaleNames.IndexOf(curNeutralLocaleName));
                float labelW = EditorGUIUtility.labelWidth;
                float dropdownW = (position.size.x - labelW) * 0.5f;
                var rectNeutral = new Rect(position.position + new Vector2(labelW, 0f), new Vector2(dropdownW, position.height));
                int newNeutralIndex = EditorGUI.Popup(rectNeutral, prevNeutralIndex, ldm == LocaleDisplayMode.Code ? neutralLocaleNames.ToArray() : neutralLocaleEnglishNames.ToArray());
                //Debug.LogFormat("curneutrallocalename: {0}", curNeutralLocaleName);
                List<Locale> specifics = localeDB.GetSpecificLocales(localeDB[curNeutralLocaleName], true).ToList();
                List<string> specificNames = specifics.Select(l => l.RegionInName).ToList();
                List<string> specificEnglishNames = specifics.Select(l => l.RegionInEnglishName).ToList();
                int prevSpecificIndex;
                int newSpecificIndex;
                if (newNeutralIndex != prevNeutralIndex) {
                    prevSpecificIndex = 0;
                } else {
                    prevSpecificIndex = specificNames.IndexOf(curLocale.RegionInName);
                }
                specificEnglishNames[0] = specificNames[0] = "*neutral*";
                var rectSpecific = new Rect(position.position + new Vector2(labelW + dropdownW, 0f), new Vector2(dropdownW, position.height));
                newSpecificIndex = EditorGUI.Popup(rectSpecific, prevSpecificIndex, ldm == LocaleDisplayMode.Code ? specificNames.ToArray() : specificEnglishNames.ToArray());

                var newLocaleName = string.Format("{0}{1}", neutralLocaleNames[newNeutralIndex], newSpecificIndex == 0 ? "" : "-" + specificNames[newSpecificIndex]);
                var newLocale = localeDB[newLocaleName];
                // Debug.LogFormat("newlocalename: {0}", newLocaleName);
                LocaleToProperty(newLocale, property);
            }
        }*/

        public static void DrawLocaleField(bool filterToAvailable, Rect position, SerializedProperty property, GUIContent label, LocalePropertyDrawMode drawMode = LocalePropertyDrawMode.MenuDropdown) {
            LocalizationService serviceInstance = LocalizationService.Instance;
            if (property.serializedObject.targetObject is LocalizationService) {
                // locale property is part of a localization service component, use local object
                serviceInstance = property.serializedObject.targetObject as LocalizationService;
            }

            if (serviceInstance == null) {
                EditorGUI.LabelField(position, property.displayName, "Localization Service is not ready.");
            } else if (serviceInstance.localeDatabase.allLocalesByName.Count == 0) {
                EditorGUI.LabelField(position, property.displayName, "Localization Service's locale database is empty.");
            } else {
                // float startTime = Time.realtimeSinceStartup;
                switch (drawMode) {
                    case LocalePropertyDrawMode.FlatDropdown:
                        MenuDropdownLocaleField(serviceInstance, filterToAvailable, position, property, label, true);
                        break;
                    case LocalePropertyDrawMode.MenuDropdown:
                        MenuDropdownLocaleField(serviceInstance, filterToAvailable, position, property, label, false);
                        break;
                }
                // Debug.LogFormat("after ongui: {0} ({1})", Time.realtimeSinceStartup, Time.realtimeSinceStartup - startTime);
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            DrawLocaleField(false, position, property, label, LocalePropertyDrawMode.MenuDropdown);
        }
    }
}