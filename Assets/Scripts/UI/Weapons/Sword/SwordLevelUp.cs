using System;
using UnityEngine;

namespace BOYAREngine
{
    public class SwordLevelUp : MonoBehaviour
    {
        public int DamageBooster;

        private bool _isUpdateable;
        [SerializeField] private SwordHover _swordHover;
        private UIManagerLegacy _ui;
        private Player _player;

        private void Awake()
        {
            _ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManagerLegacy>();
        }

        // Click Event
        public void LevelUp()
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            if (_player.Stats.LevelUpPoints > 0)
            {
                Sword.Damage += DamageBooster;
                Sword.Level++;
                _player.Stats.LevelUpPoints--;

                if (_player.Stats.LevelUpPoints == 0)
                {
                    ClosePanel();
                    _ui.LevelUpGroup.SetActive(false);
                }
            }
        }

        private void Update()
        {
            if (!_isUpdateable) return;
            _swordHover.LevelValue.text = Sword.Level + "<color=#9AEE49>+1</color>";
            _swordHover.DamageValue.text = Sword.Damage + "<color=#9AEE49>+" + DamageBooster + "</color>";
        }

        // Hover event
        public void ShowPanel()
        {
            _isUpdateable = true;

            _swordHover.ShowPanel();
        }

        public void ClosePanel()
        {
            _isUpdateable = false;

            _swordHover.ClosePanel();
        }
    }
}
