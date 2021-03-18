using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    [RequireComponent(typeof(Image))]
    public class DoubleJumpUI : MonoBehaviour
    {
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _cooldownSprite;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void DoubleJump()
        {
            _image.sprite = _cooldownSprite;
        }

        private void DoubleJumpReady()
        {
            _image.sprite = _normalSprite;
        }

        private void OnEnable()
        {
            PlayerEvents.DoubleJump += DoubleJump;
            PlayerEvents.DoubleJumpReady += DoubleJumpReady;
        }
        private void OnDisable()
        {
            PlayerEvents.DoubleJump -= DoubleJump;
            PlayerEvents.DoubleJumpReady -= DoubleJumpReady;
        }
    }
}
