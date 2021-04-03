using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class Currency : MonoBehaviour
    {
        [SerializeField] private Text _currencyText;
        private Player _player;

        private void OnGiveCurrency(int amount)
        {
            StartCoroutine(UpdateCurrencyUi());
        }

        private IEnumerator UpdateCurrencyUi()
        {
            yield return new WaitForEndOfFrame();

            if (_player != null)
            {
                _currencyText.text = _player.Stats.Currency + "G";
            }
        }

        private void OnEnable()
        {
            Events.PlayerOnScene += AssignPlayer;

            PlayerEvents.GiveCurrency += OnGiveCurrency;
        }

        private void OnDisable()
        {
            Events.PlayerOnScene -= AssignPlayer;

            PlayerEvents.GiveCurrency -= OnGiveCurrency;
        }

        private void AssignPlayer(bool isActive)
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }
}
