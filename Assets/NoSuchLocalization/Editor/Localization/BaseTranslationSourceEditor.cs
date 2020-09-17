using UnityEditor;

using NoSuchStudio.Common.Editor;

namespace NoSuchStudio.Localization.Editor {
    /// <summary>
    ///  Base Editor type for Translation Source components. <seealso cref="BaseTranslationSource"/>.
    /// </summary>
    /// <typeparam name="ET">The Editor type that inherits BaseTranslationSourceEditor.</typeparam>
    /// <typeparam name="ST">The TranslationSource component type.</typeparam>
    public abstract class BaseTranslationSourceEditor<ET, ST> : NoSuchEditor
        where ST : BaseTranslationSource {
        protected ST tsTarget;

        protected override void OnEnable() {
            base.OnEnable();
            tsTarget = (ST)target;
        }

        public void DrawTranslationStats(int phraseCount, int translationCount) {
            EditorGUILayout.LabelField(string.Format("Phrases: {0}, Translations: {1}", phraseCount, translationCount));
        }

        public override void OnInspectorGUI() {
            // connection status
            DrawServiceConnectionStatus(tsTarget);
            EditorGUILayout.Separator();

            // editor 
            DrawDefaultInspector();
        }
    }
}