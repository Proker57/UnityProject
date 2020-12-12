using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Text _text;
        private Player _player;

        private void Update()
        {
            if (_player == null) return;
            _text.text = "Lv." + _player.Stats.Level;
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
            _player = Player.Instance;
        }
    }
}