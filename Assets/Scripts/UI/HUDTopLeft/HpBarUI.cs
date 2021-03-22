using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class HpBarUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;
        private Player _player;

        private void UpdateHpBar()
        {
            if (_player == null) return;

            var currentHealth = _player.Stats.Health;
            var maxHealth = _player.Stats.MaxHealth;
            var fillHealthValue = (float)currentHealth / (float)maxHealth;

            _image.fillAmount = fillHealthValue;
            _text.text = currentHealth + "/" + maxHealth;
        }

        private void OnLoad()
        {
            UpdateHpBar();
        }

        private void OnEnable()
        {
            PlayerEvents.UpdateHPBar += UpdateHpBar;

            Events.Load += OnLoad;
            Events.PlayerOnScene += AssignPlayer;
        }

        private void OnDisable()
        {
            PlayerEvents.UpdateHPBar -= UpdateHpBar;

            Events.PlayerOnScene -= AssignPlayer;
        }

        private void AssignPlayer(bool isActive)
        {
            _player = Player.Instance;
            UpdateHpBar();
        }
    }
}
