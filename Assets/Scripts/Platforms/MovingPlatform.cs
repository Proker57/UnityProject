using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class MovingPlatform : MonoBehaviour
    {
        private bool _isEnter;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        [Header("Manual platform")]
        [SerializeField] private GameObject _panel;

        [Header("General settings")]
        [SerializeField] private GameObject _platform;
        [SerializeField] private GameObject _target;
        [SerializeField] private float _moveDuration = 3;
        [SerializeField] private bool _isReturnable;

        private enum Type
        {
            Automatic,
            Manual
        }
        [SerializeField] private Type _type;

        [Space]
        [SerializeField] private InputAction _use;
        [SerializeField] private InputActionAsset _controls;

        private void Awake()
        {
            _startPosition = _platform.transform.position;
            _endPosition = _target.transform.position;
        }

        private void Start()
        {
            var iam = _controls.FindActionMap("PlayerInGame");
            _use = iam.FindAction("Use");
            _use.started += Use_started;
        }

        private void OnTriggerEnter2D(Object collider)
        {
            if (!collider.name.Equals("Low Collider")) return;
            _isEnter = true;

            if (_type == Type.Automatic)
            {
                Move();
            }
            if (_type == Type.Manual)
            {
                ShowPanel(true);
            }
        }

        private void OnTriggerExit2D(Object collider)
        {
            if (!collider.name.Equals("Low Collider")) return;
            _isEnter = false;

            if (_type == Type.Manual)
            {
                ShowPanel(false);
            }

            if (_isReturnable) Return();
        }

        private void Move()
        {
            _platform.transform.DOMove(_endPosition, _moveDuration, false);
        }

        private void Return()
        {
            _platform.transform.DOMove(_startPosition, _moveDuration, false);
        }

        private void ShowPanel(bool isActive)
        {
            _panel.SetActive(isActive);
        }

        private void Use_started(InputAction.CallbackContext ctx)
        {
            if (_type != Type.Manual || !_isEnter) return;
            _panel.SetActive(false);
            Move();
        }

        private void OnEnable()
        {
            //Inputs.Instance.Input.PlayerInGame.Use.started += _ => Use_started();
        }

        private void OnDisable()
        {
            //Inputs.Instance.Input.PlayerInGame.Use.started += _ => Use_started();
        }
    }
}
