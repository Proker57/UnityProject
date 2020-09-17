using NoSuchStudio.Common.Editor;

using UnityEditor;
using UnityEngine;

namespace NoSuchStudio.Common.Service.Editor {
    /// <summary>
    /// Base <see cref="UnityEditor.Editor"/> class for editors of <see cref="Service{T}"/> types.
    /// </summary>
    public abstract class ServiceEditor<SE, S> : NoSuchEditor 
        where S : Service<S>
        where SE : ServiceEditor<SE, S>
        {
        protected S serviceInstance;

        protected override void OnEnable() {
            base.OnEnable();
            serviceInstance = (S)target;
        }

        public void DrawServiceStatus() {
            bool instanceReady = serviceInstance.InstanceReady;
            bool instanceOn = Service<S>.Instance == serviceInstance;
            bool good = instanceReady && instanceOn;
            string status = "Live";
            if (!EditorUtilities.IsInMainStage(serviceInstance.gameObject)) {
                status = "Not In Scene (prefab?)";
            } else if (!instanceReady) {
                status = "Not Enabled / Active";
            } else if (!instanceOn) {
                status = "Not Live (Other instance?)";
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Status", status, good ? styleOn : styleOff);
            EditorGUILayout.EndHorizontal();
        }

        public override void OnInspectorGUI() {
            // status
            DrawServiceStatus();

            // restart button
            if (GUILayout.Button("Restart")) {
                serviceInstance.ReRegisterService();
            }
            EditorGUILayout.Separator();

            // default editor
            DrawDefaultInspector();
        }
    }
}