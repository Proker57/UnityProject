using UnityEngine;

namespace BOYAREngine
{
    public class WeaponSprites : MonoBehaviour
    {
        public static WeaponSprites Instance = null;

        [Header("Sword Broken")]
        public Sprite SwordBrokenUi;
        public Sprite SwordBroken;
        [Header("Sword Small")]
        public Sprite SwordSmallUi;
        public Sprite SwordSmall;

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
