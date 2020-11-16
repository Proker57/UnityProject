using UnityEngine;

namespace BOYAREngine
{
    public class ShaderPreload : MonoBehaviour
    {
        private void Awake()
        {
            var path = "Shaders/Levels/ShaderVariants";
            var collection = Resources.Load<ShaderVariantCollection>(path);

            if (collection == null) return;
            collection.WarmUp();
            Debug.Log("Loaded");
            Resources.UnloadAsset(collection);
        }
    }
}
