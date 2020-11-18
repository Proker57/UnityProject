using UnityEngine;

namespace BOYAREngine
{
    public class SwordLevelUp : MonoBehaviour
    {
        [SerializeField] private int _damegeBooster;
        private GameObject _ui;

        private void Awake()
        {
            _ui = GameObject.FindGameObjectWithTag("UI");
        }

        public void LevelUp()
        {
            Sword.Damage += _damegeBooster;
            _ui.GetComponent<UIManagerLegacy>().LevelUpGroup.SetActive(false);
        }
    }
}
