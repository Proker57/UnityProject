using UnityEngine;

namespace NoSuchStudio.Common {
    public class EditorUtilities {
        public static bool IsInMainStage(GameObject go) {
#if UNITY_EDITOR
            var mainStage = UnityEditor.SceneManagement.StageUtility.GetMainStageHandle();
            var currentStage = UnityEditor.SceneManagement.StageUtility.GetStageHandle(go);
            return currentStage == mainStage;
#else
            return true;
#endif
        }
    }
}
