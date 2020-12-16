using UnityEngine;

namespace BOYAREngine
{
    public class BowLevelUp : MonoBehaviour
    {
        public int DamageBooster;

        private bool _isUpdateable;
        [SerializeField] private BowHover _bowHover;
        private UIManagerLegacy _ui;
        private Player _player;

        private void Awake()
        {
            _ui = UIManagerLegacy.Instance;
        }

        // Called from UnityEvent-Inspector
        public void LevelUp()
        {
            if (_player == null)
            {
                _player = Player.Instance;
            }

            if (_player.Stats.LevelUpPoints <= 0) return;
            Bow.Damage += DamageBooster;
            Bow.Level++;
            _player.Stats.LevelUpPoints--;

            if (_player.Stats.LevelUpPoints != 0) return;
            ClosePanel();
            _ui.LevelUpGroup.SetActive(false);
        }

        private void Update()
        {
            if (!_isUpdateable) return;
            //_bowHover.LevelValue.text = Bow.Level + "<color=#9AEE49>" + "+1" + "</color>";
            _bowHover.DamageValue.text = Bow.Damage + "<color=#9AEE49>" + "+" + DamageBooster + "</color>";
        }

        // Hover event
        public void ShowPanel()
        {
            _isUpdateable = true;

            _bowHover.ShowPanel();
        }

        public void ClosePanel()
        {
            _isUpdateable = false;

            _bowHover.ClosePanel();
        }
    }
}
