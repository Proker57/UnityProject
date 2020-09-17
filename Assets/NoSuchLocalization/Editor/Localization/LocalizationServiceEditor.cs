using UnityEditor;
using UnityEngine;

using NoSuchStudio.Common.Service.Editor;

namespace NoSuchStudio.Localization.Editor {
    /// <summary>
    /// Editor for <see cref="LocalizationService"/>.
    /// </summary>
    [CustomEditor(typeof(LocalizationService))]
    [CanEditMultipleObjects]
    public sealed class LocalizationServiceEditor : ServiceEditor<LocalizationServiceEditor, LocalizationService> {
        public override void OnInspectorGUI() {
            serializedObject.Update();
            // status
            DrawServiceStatus();

            // restart button
            if (GUILayout.Button("Restart")) {
                serviceInstance.ReRegisterService();
            }
            EditorGUILayout.Separator();

            // database section
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_databasePreset"));
            var dbPreset = serviceInstance.databasePreset;
            if (dbPreset == LocalizationService.DatabasePreset.Custom) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_localeDatabaseAsset"));
            }
            serializedObject.ApplyModifiedProperties();
            // default editor
            DrawDefaultInspector();
        }
    }
}