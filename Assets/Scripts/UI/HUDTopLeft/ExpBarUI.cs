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
            if (_player == null) return;

            var currentExp = (float)_player.Stats.Exp;
            var maxExp = (float)_player.Stats.MaxExp;
            _image.fillAmount = currentExp / maxExp;
        }

        private void OnEnable()
        {
            Events.PlayerOnScene += AssignPlayer;
        }

        private void OnDisable()
        {
            Events.PlayerOnScene -= AssignPlayer;
        }

        private void AssignPlayer(bool isActive)
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }
}
