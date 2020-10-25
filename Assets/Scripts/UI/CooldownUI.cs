using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace BOYAREngine
{
    [RequireComponent(typeof(Image))]
    public class CooldownUI : MonoBehaviour
    {
        private float _cooldownTimer;

        private Image _image;
        private Slider _slider;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _cooldownSprite;
        private bool _isOnCooldown;
        

        private void Awake()
        {
            _image = GetComponent<Image>();
            _slider = GetComponent<Slider>();
        }

        private void DashCD(float cooldownTime)
        {
            _image.sprite = _cooldownSprite;
            _cooldownTimer = cooldownTime;
            _slider.maxValue = _cooldownTimer;
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
            Events.Dash += DashCD;
            Events.DashReady += DashReady;
        }

        private void OnDisable()
        {
            Events.Dash -= DashCD;
            Events.DashReady -= DashReady;
        }

        private void SliderCountdown()
        {
            if (_isOnCooldown == true)
            {
                _cooldownTimer -= Time.deltaTime;
                _slider.value = _cooldownTimer;
                if (_cooldownTimer <= 0)
                {
                    _cooldownTimer = 0;
                    _isOnCooldown = false;
                }
            }
        }
    }
}
