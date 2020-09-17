using NoSuchStudio.Localization.Editor;
using TMPro;
using UnityEditor;

namespace NoSuchStudio.Localization.Localizers.Editor {
    [CustomEditor(typeof(TMProFontMappedLocalizer))]
    public class TMProFontMappedLocalizerEditor : ComponentLocalizerEditor<TMProFontMappedLocalizerEditor, TMProFontMappedLocalizer, TextMeshProUGUI> {
        [MenuItem("CONTEXT/TextMeshProUGUI/Localize Font")]
        static void Localize(MenuCommand command) {
            var c = (TextMeshProUGUI)command.context;
            c.gameObject.AddComponent<TMProFontMappedLocalizer>();
        }
        [MenuItem("CONTEXT/TextMeshProUGUI/Localize Font", true)]
        static bool ValidateLocalize(MenuCommand command) {
            var c = (TextMeshProUGUI)command.context;
            return !c.gameObject.GetComponent<TMProFontMappedLocalizer>();
        }
    }
}