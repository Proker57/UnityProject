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

        private void ChangeNormalIcon()
        {
            _image.sprite = _normalSprite;
        }

        private void ChangeCooldownIcon()
        {
            _image.sprite = _cooldownSprite;
        }

        private void OnEnable()
        {
            Events.DoubleJump += ChangeCooldownIcon;
            Events.DoubleJumpReady += ChangeNormalIcon;
        }
        private void OnDisable()
        {
            Events.DoubleJump -= ChangeCooldownIcon;
            Events.DoubleJumpReady -= ChangeNormalIcon;
        }
    }
}
