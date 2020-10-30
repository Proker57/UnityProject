using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    [RequireComponent(typeof(Image))]
    public class DashUI : MonoBehaviour
    {
        private float _cooldownTimer;

        [SerializeField] private GameObject _cooldownBar;
        [SerializeField] private Image _fill;
        private Image _image;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _cooldownSprite;
        private bool _isOnCooldown;
        

        private void Awake()
        {
            _cooldownBar.SetActive(false);
            _image = GetComponent<Image>();
        }

        private void DashCooldown(float cooldownTime)
        {
            _image.sprite = _cooldownSprite;
            _cooldownTimer = cooldownTime;
            _isOnCooldown = true;
        }

        private void DashReady()
        {
            _image.sprite = _normalSprite;
        }

        private void Update()
        {
            SliderCountdown();
        }

        private void OnEnable()
        {
            PlayerEvents.Dash += DashCooldown;
            PlayerEvents.DashReady += DashReady;
        }

        private void OnDisable()
        {
            PlayerEvents.Dash -= DashCooldown;
            PlayerEvents.DashReady -= DashReady;
        }

        private void SliderCountdown()
        {
            if (_isOnCooldown == true)
            {
                _cooldownBar.SetActive(true);
                _cooldownTimer -= Time.deltaTime;
                _fill.fillAmount = _cooldownTimer;
                if (_cooldownTimer <= 0)
                {
                    _cooldownBar.SetActive(false);
                    _cooldownTimer = 0;
                    _isOnCooldown = false;
                }
            }
        }
    }
}
