using UnityEngine;

namespace BOYAREngine
{
    public class SwordLevelUp : MonoBehaviour
    {
        public int DamageBooster;

        private bool _isUpdateable;

        [SerializeField] private InventoryWeaponStatsHover _inventoryWeaponStatsHover;
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
                _player = Player.Instance;
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
            //_meleeHover.LevelValue.text = Sword.Level + "<color=#9AEE49>+1</color>";
            _inventoryWeaponStatsHover.DamageValue.text = Sword.Damage + "<color=#9AEE49>+" + DamageBooster + "</color>";
        }

        // Hover event
        public void ShowPanel()
        {
            _isUpdateable = true;

            _inventoryWeaponStatsHover.ShowPanel();
        }

        public void ClosePanel()
        {
            _isUpdateable = false;

            _inventoryWeaponStatsHover.ClosePanel();
        }
    }
}
