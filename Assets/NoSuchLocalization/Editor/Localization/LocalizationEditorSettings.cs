using Newtonsoft.Json;

using System.Linq;

using UnityEditor;
using UnityEngine;

using NoSuchStudio.Common.Editor;

namespace NoSuchStudio.Localization.Editor {
    public enum DisplayMode {
        Code,
        Name
    }

    [InitializeOnLoad]
    public static class LocalizationEditorSettings {
        public const string MainMenuKey = "No Such Studio";
        public const string ModuleKey = "Localization";

        public const string MenuKeyDisplayMode = MainMenuKey + "/" + ModuleKey + "/" + "Display Mode";
        public const string MenuKeyDisplayModeCode = MenuKeyDisplayMode + "/Code";
        public const string MenuKeyDisplayModeName = MenuKeyDisplayMode + "/Name";

        static LocalizationEditorSettings() {
            var json = EditorPrefs.GetString(MenuKeyDisplayMode);
            DisplayMode dm = string.IsNullOrEmpty(json)
                ? DisplayMode.Name
                : JsonConvert.DeserializeObject<DisplayMode>(json, LocalizationSettings.jsonSettings);
            SetLocaleDisplayMode(dm);
        }

        private static DisplayMode _displayMode;
        public static DisplayMode displayMode {
            get {
                return _displayMode;
            }
            set {
                SetLocaleDisplayMode(value);
            }
        }

        private static void SetLocaleDisplayMode(DisplayMode ldm) {
            _displayMode = ldm;
            Menu.SetChecked(MenuKeyDisplayModeCode, _displayMode == DisplayMode.Code);
            Menu.SetChecked(MenuKeyDisplayModeName, _displayMode == DisplayMode.Name);
            // Save editor settings
            EditorPrefs.SetString(MenuKeyDisplayMode, JsonConvert.SerializeObject(_displayMode, LocalizationSettings.jsonSettings));
            RepainAllLocalizationEditors();
        }

        private static void RepainAllLocalizationEditors() {
            NoSuchEditor[] allEditors = Resources.FindObjectsOfTypeAll<NoSuchEditor>();
            allEditors.ToList().ForEach(e => e.Repaint());
        }

        [MenuItem(MenuKeyDisplayModeCode)]
        public static void SetLocaleDisplayModeCode() {
            SetLocaleDisplayMode(DisplayMode.Code);
        }

        [MenuItem(MenuKeyDisplayModeCode, true)]
        static bool ValidateSetLocaleDisplayModeCode() {
            //return _displayMode != LocaleDisplayMode.Code;
            return true;
        }

        [MenuItem(MenuKeyDisplayModeName)]
        public static void SetLocaleDisplayModeName() {
            SetLocaleDisplayMode(DisplayMode.Name);
        }

        [MenuItem(MenuKeyDisplayModeName, true)]
        static bool ValidateSetLocaleDisplayModeName() {
            //return _displayMode != LocaleDisplayMode.Name;
            return true;
        }
    }
}
