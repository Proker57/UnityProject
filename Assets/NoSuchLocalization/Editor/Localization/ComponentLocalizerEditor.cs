using UnityEditor;
using UnityEngine;
using NoSuchStudio.Common.Editor;

namespace NoSuchStudio.Localization.Editor {
    /// <summary>
    /// Base class for Editors of <see cref="ComponentLocalizer{LT, CT}"/>s.
    /// </summary>
    /// <typeparam name="ET">The Editor class that inherits ComponentLocalizerEditor.</typeparam>
    /// <typeparam name="LT">The ComponentLocalizer type that the Editor class handles.</typeparam>
    /// <typeparam name="CT">The type of <see cref="UnityEngine.Component"/> that LT handles.</typeparam>
    public abstract class ComponentLocalizerEditor<ET, LT, CT> : NoSuchEditor
        where ET : ComponentLocalizerEditor<ET, LT, CT>
        where LT : ComponentLocalizer<LT, CT>
        where CT : Component {
        /// <summary>
        /// Editor <see cref="UnityEditor.Editor.target"/> as the target type of this Editor.
        /// </summary>
        protected ComponentLocalizer<LT, CT> lcTarget;

        protected override void OnEnable() {
            base.OnEnable();
            lcTarget = (ComponentLocalizer<LT, CT>)target;
        }

        public override void OnInspectorGUI() {
            // connection status
            DrawServiceConnectionStatus(lcTarget);
            EditorGUILayout.Separator();

            // reload button
            if (GUILayout.Button("Reload")) {
                lcTarget.Reconnect<LocalizationService>();
            }
            EditorGUILayout.Separator();

            // default inspector
            DrawDefaultInspector();
        }
    }
}