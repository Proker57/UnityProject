using UnityEngine;

namespace BOYAREngine
{
    public class ChangeLevel : MonoBehaviour
    {
        private BoxCollider2D _boxCollider2D;

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (collider2d.name != "Low Collider") return;
            SceneLoader.SwitchScene("TestLevel002");
        }
    }
}
