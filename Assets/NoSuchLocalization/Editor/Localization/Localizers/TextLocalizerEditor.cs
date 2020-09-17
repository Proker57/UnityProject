using NoSuchStudio.Localization.Editor;
using UnityEditor;
using UnityEngine.UI;

namespace NoSuchStudio.Localization.Localizers.Editor {
    [CustomEditor(typeof(TextLocalizer))]
    public class TextLocalizerEditor : ComponentLocalizerEditor<TextLocalizerEditor, TextLocalizer, Text> {
        [MenuItem("CONTEXT/Text/Localize Text")]
        static void Localize(MenuCommand command) {
            var c = (Text)command.context;
            c.gameObject.AddComponent<TextLocalizer>();
        }
        [MenuItem("CONTEXT/Text/Localize Text", true)]
        static bool ValidateLocalize(MenuCommand command) {
            var c = (Text)command.context;
            return !c.gameObject.GetComponent<TextLocalizer>();
        }
    }
}
