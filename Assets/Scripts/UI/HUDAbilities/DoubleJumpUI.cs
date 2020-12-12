using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    [RequireComponent(typeof(Image))]
    public class DoubleJumpUI : MonoBehaviour
    {
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _cooldownSprite;
        private Player _player;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            if (_player == null) return;
            _image.sprite = _player.Jump.IsDoubleJumping ? _cooldownSprite : _normalSprite;
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
