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
            if (_player == null && FindObjectOfType<Player>() != null)
            {
                _player = FindObjectOfType<Player>().GetComponent<Player>();
            }

            if (_player == null) return;
            _text.text = "Lv." + _player.Stats.Level;
        }
    }
}