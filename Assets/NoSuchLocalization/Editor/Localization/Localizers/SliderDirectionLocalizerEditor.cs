using NoSuchStudio.Localization.Editor;
using UnityEditor;
using UnityEngine.UI;

namespace NoSuchStudio.Localization.Localizers.Editor {
    [CustomEditor(typeof(SliderDirectionLocalizer))]
    public class SliderDirectionLocalizerEditor : ComponentLocalizerEditor<SliderDirectionLocalizerEditor, SliderDirectionLocalizer, Slider> {
        [MenuItem("CONTEXT/Slider/Localize Direction")]
        static void Localize(MenuCommand command) {
            var c = (Slider)command.context;
            c.gameObject.AddComponent<SliderDirectionLocalizer>();
        }
        [MenuItem("CONTEXT/Slider/Localize Direction", true)]
        static bool ValidateLocalize(MenuCommand command) {
            var c = (Slider)command.context;
            return !c.gameObject.GetComponent<SliderDirectionLocalizer>();
        }
    }
}