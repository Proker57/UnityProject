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
            if (_player != null)
            {

                if (_player.Jump.IsDoubleJumping)
                {
                    _image.sprite = _cooldownSprite;
                }
                else
                {
                    _image.sprite = _normalSprite;
                }
            }
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
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }
}
