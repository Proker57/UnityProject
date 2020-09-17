using NoSuchStudio.Localization.Editor;
using UnityEditor;
using UnityEngine.UI;

namespace NoSuchStudio.Localization.Localizers.Editor {
    [CustomEditor(typeof(ImageSpriteMappedLocalizer))]
    public class ImageSpriteMappedLocalizerEditor : ComponentLocalizerEditor<ImageSpriteMappedLocalizerEditor, ImageSpriteMappedLocalizer, Image> {
        [MenuItem("CONTEXT/Image/Localize Sprite")]
        static void Localize(MenuCommand command) {
            var c = (Image)command.context;
            c.gameObject.AddComponent<ImageSpriteMappedLocalizer>();
        }
        [MenuItem("CONTEXT/Image/Localize Sprite", true)]
        static bool ValidateLocalize(MenuCommand command) {
            var c = (Image)command.context;
            return !c.gameObject.GetComponent<ImageSpriteMappedLocalizer>();
        }
    }
}