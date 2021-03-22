using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class ExpBarUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _progressText;
        private Player _player;

        private IEnumerator UpdateExp()
        {
            yield return new WaitForEndOfFrame();

            var currentExp = (float)_player.Stats.Exp;
            var maxExp = (float)_player.Stats.MaxExp;
            _image.fillAmount = currentExp / maxExp;

            _progressText.text = currentExp + "/" + maxExp;
        }

        public void ShowPanel()
        {
            _progressText.gameObject.SetActive(true);
        }

        public void ClosePanel()
        {
            _progressText.gameObject.SetActive(false);
        }

        private void OnGiveExp(int amount)
        {
            StartCoroutine(UpdateExp());
        }

        private void OnLoad()
        {
            StartCoroutine(UpdateExp());
        }

        private void OnEnable()
        {
            Events.PlayerOnScene += AssignPlayer;

            PlayerEvents.GiveExp += OnGiveExp;

            Events.Load += OnLoad;
        }

        private void OnDisable()
        {
            Events.PlayerOnScene -= AssignPlayer;

            PlayerEvents.GiveExp -= OnGiveExp;

            Events.Load -= OnLoad;
        }

        private void AssignPlayer(bool isActive)
        {
            _player = Player.Instance;
            StartCoroutine(UpdateExp());
        }
    }
}
