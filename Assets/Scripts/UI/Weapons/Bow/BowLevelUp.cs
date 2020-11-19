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
            _ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManagerLegacy>();
        }

        // Called from UnityEvent-Inspector
        public void LevelUp()
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            if (_player.Stats.LevelUpPoints > 0)
            {
                Bow.Damage += DamageBooster;
                Bow.Level++;
                _player.Stats.LevelUpPoints--;

                if (_player.Stats.LevelUpPoints == 0)
                {
                    _ui.LevelUpGroup.SetActive(false);
                }
            }
        }

        private void Update()
        {
            if (!_isUpdateable) return;
            _bowHover.LevelValue.text = Bow.Level + "+1";
            _bowHover.DamageValue.text = Bow.Damage + "+" + DamageBooster;
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
