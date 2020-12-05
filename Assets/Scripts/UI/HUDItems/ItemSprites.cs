using UnityEngine;

namespace BOYAREngine
{
    public class ItemSprites : MonoBehaviour
    {
        public static ItemSprites Instance = null;

        public Sprite None;
        public Sprite SmallPotion;
        public Sprite MediumPotion;
        public Sprite HugePotion;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}
