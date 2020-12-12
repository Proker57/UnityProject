using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class ExpBarUI : MonoBehaviour
    {
        private bool _isUpdateable;
        [SerializeField] private Image _image;
        [SerializeField] private Text _progressText;
        private Player _player;

        private void Update()
        {
            if (_player == null) return;

            var currentExp = (float)_player.Stats.Exp;
            var maxExp = (float)_player.Stats.MaxExp;
            _image.fillAmount = currentExp / maxExp;

            if (_isUpdateable)
            {
                _progressText.text = currentExp + "/" + maxExp;
            }
        }

        public void ShowPanel()
        {
            _progressText.gameObject.SetActive(true);
            _isUpdateable = true;
        }

        public void ClosePanel()
        {
            _progressText.gameObject.SetActive(false);
            _isUpdateable = false;
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
