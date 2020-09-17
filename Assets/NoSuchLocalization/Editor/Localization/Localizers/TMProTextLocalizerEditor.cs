using NoSuchStudio.Localization.Editor;
using TMPro;
using UnityEditor;

namespace NoSuchStudio.Localization.Localizers.Editor {
    [CustomEditor(typeof(TMProTextLocalizer))]
    public class TMProTextLocalizerEditor : ComponentLocalizerEditor<TMProTextLocalizerEditor, TMProTextLocalizer, TextMeshProUGUI> {
        [MenuItem("CONTEXT/TextMeshProUGUI/Localize Text")]
        static void Localize(MenuCommand command) {
            var c = (TextMeshProUGUI)command.context;
            c.gameObject.AddComponent<TMProTextLocalizer>();
        }
        [MenuItem("CONTEXT/TextMeshProUGUI/Localize Text", true)]
        static bool ValidateLocalize(MenuCommand command) {
            var c = (TextMeshProUGUI)command.context;
            return !c.gameObject.GetComponent<TMProTextLocalizer>();
        }
    }
}