using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    [RequireComponent(typeof(Image))]
    public class DoubleJumpUI : MonoBehaviour
    {
        private Image _image;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _cooldownSprite;
        private bool _isOnCooldown;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            PlayerEvents.DoubleJump += ChangeCooldownIcon;
            PlayerEvents.DoubleJumpReady += ChangeNormalIcon;
        }
        private void OnDisable()
        {
            PlayerEvents.DoubleJump -= ChangeCooldownIcon;
            PlayerEvents.DoubleJumpReady -= ChangeNormalIcon;
        }

        private void ChangeNormalIcon()
        {
            _image.sprite = _normalSprite;
        }

        private void ChangeCooldownIcon()
        {
            _image.sprite = _cooldownSprite;
        }
    }
}
