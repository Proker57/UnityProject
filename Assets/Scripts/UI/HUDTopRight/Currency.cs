using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class Currency : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private Text _currencyText;
#pragma warning restore 649
        private Player _player;

        private void Update()
        {
            if (_player != null)
            {
                _currencyText.text = _player.Stats.Currency + "G";
            }
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
