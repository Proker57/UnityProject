using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    [RequireComponent(typeof(Image))]
    public class DashUI : MonoBehaviour
    {
        private float _dashTimer;
        private float _dashTimerCounter;

        [SerializeField] private GameObject _cooldownBar;
        [SerializeField] private Image _fill;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _cooldownSprite;
        private Image _image;
        private Player _player;

        private void Awake()
        {
            _cooldownBar.SetActive(false);

            _image = GetComponent<Image>();
        }

        private void Update()
        {
            if (_player == null) return;
            CooldownSlider();
        }

        private void CooldownSlider()
        {
            if (_player.Dash.IsDashable) return;
            _fill.fillAmount = _player.Dash.DashTimerCounter;
        }

        private void Dashed()
        {
            _image.sprite = _cooldownSprite;
            _cooldownBar.SetActive(true);
        }

        private void DashReady()
        {
            _image.sprite = _normalSprite;
            _cooldownBar.SetActive(false);
        }

        private void OnEnable()
        {
            PlayerEvents.Dash += Dashed;
            PlayerEvents.DashReady += DashReady;

            Events.PlayerOnScene += AssignPlayer;
        }

        private void OnDestroy()
        {
            PlayerEvents.Dash -= Dashed;
            PlayerEvents.DashReady -= DashReady;

            Events.PlayerOnScene -= AssignPlayer;
        }

        private void AssignPlayer(bool isActive)
        {
            _player = isActive ? GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() : null;
        }

    }
}
