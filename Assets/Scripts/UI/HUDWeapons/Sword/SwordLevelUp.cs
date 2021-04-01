using UnityEngine;

namespace BOYAREngine
{
    public class SwordLevelUp : MonoBehaviour
    {
        public int DamageBooster;

        private bool _isUpdateable;

        [SerializeField] private InventoryPanelHover _inventoryPanelHover;
        private UIManagerLegacy _ui;
        private Player _player;

        private void Awake()
        {
            _ui = UIManagerLegacy.Instance;
        }

        // Click Event
        public void LevelUp()
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            if (_player.Stats.LevelUpPoints <= 0) return;
            Sword.Damage += DamageBooster;
            Sword.Level++;
            _player.Stats.LevelUpPoints--;

            if (_player.Stats.LevelUpPoints != 0) return;
            ClosePanel();
            _ui.LevelUpGroup.SetActive(false);
        }

        private void Update()
        {
            if (!_isUpdateable) return;
            _inventoryPanelHover.WeaponDamage.text = Sword.Damage + "<color=#9AEE49>+" + DamageBooster + "</color>";
        }

        // Hover event
        public void ShowPanel()
        {
            _isUpdateable = true;

            _inventoryPanelHover.ShowPanel();
        }

        public void ClosePanel()
        {
            _isUpdateable = false;

            _inventoryPanelHover.ClosePanel();
        }
    }
}
