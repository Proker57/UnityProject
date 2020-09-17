using NoSuchStudio.Localization.Editor;
using UnityEditor;
using UnityEngine;

namespace NoSuchStudio.Localization.Localizers.Editor {
    [CustomEditor(typeof(AudioSourceClipMappedLocalizer))]
    public class AudioSourceClipMappedLocalizerEditor : ComponentLocalizerEditor<AudioSourceClipMappedLocalizerEditor, AudioSourceClipMappedLocalizer, AudioSource> {
        [MenuItem("CONTEXT/AudioSource/Localize Clip")]
        static void Localize(MenuCommand command) {
            var c = (AudioSource)command.context;
            c.gameObject.AddComponent<AudioSourceClipMappedLocalizer>();
        }
        [MenuItem("CONTEXT/AudioSource/Localize Clip", true)]
        static bool ValidateLocalize(MenuCommand command) {
            var c = (AudioSource)command.context;
            return !c.gameObject.GetComponent<AudioSourceClipMappedLocalizer>();
        }
    }
}