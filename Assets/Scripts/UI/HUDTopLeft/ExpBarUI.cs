using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class ExpBarUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private Player _player;

        private void Update()
        {
            if (_player == null && FindObjectOfType<Player>() != null)
            {
                _player = FindObjectOfType<Player>().GetComponent<Player>();
            }

            if (_player == null) return;
            var currentExp = (float) _player.Stats.PlayerData.EXP;
            var maxExp = (float)_player.Stats.MaxExp;
            _image.fillAmount = currentExp / maxExp;
        }
    }
}
