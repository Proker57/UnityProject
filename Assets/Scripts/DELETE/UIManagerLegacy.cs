using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class UIManagerLegacy : MonoBehaviour
    {
        public static UIManagerLegacy Instance = null;
        public GameObject LevelUpGroup;
        [SerializeField] private GameObject _dashUI;
        [SerializeField] private GameObject _DoubleJumpUI;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void LevelUp()
        {
            LevelUpGroup.SetActive(true);
        }

        private void OnEnable()
        {
            PlayerEvents.LevelUp += LevelUp;
        }

        private void OnDisable()
        {
            PlayerEvents.LevelUp -= LevelUp;
        }
    }
}
