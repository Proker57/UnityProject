using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class HpBarUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;
        private Player _player;

        private void Update()
        {
            if (_player == null && FindObjectOfType<Player>() != null)
            {
                _player = FindObjectOfType<Player>().GetComponent<Player>();
            }

            if (_player == null) return;
            var fillHealthValue = (float)_player.Stats.PlayerData.Health / (float)_player.Stats.PlayerData.MaxHealth;
            _image.fillAmount = fillHealthValue;

            var currentHealth = _player.Stats.PlayerData.Health;
            var maxHealth = _player.Stats.PlayerData.MaxHealth;
            _text.text = currentHealth + "/" + maxHealth;
        }
    }
}
