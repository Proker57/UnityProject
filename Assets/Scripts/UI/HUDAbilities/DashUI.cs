using UnityEditor.ShaderGraph.Internal;
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
            if (_player != null)
            {
                CooldownSlider();
            }
        }

        private void CooldownSlider()
        {
            if (_player.Dash.IsDashable == false)
            {
                _image.sprite = _cooldownSprite;

                _cooldownBar.SetActive(true);
                _fill.fillAmount = _player.Dash.DashTimerCounter;
                if (_player.Dash.DashTimerCounter <= 0)
                {
                    _image.sprite = _normalSprite;

                    _cooldownBar.SetActive(false);
                }
            }
        }

        private void OnEnable()
        {
            Events.PlayerOnScene += AssignPlayer;
        }

        private void OnDestroy()
        {
            Events.PlayerOnScene -= AssignPlayer;
        }

        private void AssignPlayer(bool isActive)
        {
            if (isActive)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
            else
            {
                _player = null;
            }
        }

    }
}
