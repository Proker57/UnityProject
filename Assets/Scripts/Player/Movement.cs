using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class Movement : MonoBehaviour
    {
        private const float Tolerance = 0;

        public float CurrentSpeed;
        [Space]
        [Space, Header("Player sprite with bones")]
        [SerializeField] private Transform _spriteTransform;

        [HideInInspector] public float BaseSpeed;
        [HideInInspector] public bool IsLookingRight;
        private Vector2 _direction;
        private float _movementDirection;
        private bool _isRunning;
        private bool _isButtonPressed;
        [Space]
        private Player _player;
        [SerializeField] private InputActionAsset _controls;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            BaseSpeed = CurrentSpeed;

            _controls.FindActionMap("PlayerInGame").FindAction("Movement").started += Movement_started;
            _controls.FindActionMap("PlayerInGame").FindAction("Movement").canceled += Movement_canceled;
        }

        private void Update()
        {
            _movementDirection = _player.Input.PlayerInGame.Movement.ReadValue<float>();
            _isRunning = Math.Abs(_movementDirection) > Tolerance;
            _direction = new Vector2(_movementDirection * CurrentSpeed, _player.Rigidbody2D.velocity.y);
        }

        public void Movement_started(InputAction.CallbackContext ctx)
        {
            _isButtonPressed = true;
            Flip(ctx.ReadValue<float>());
            Animation(true);
        }

        public void Movement_canceled(InputAction.CallbackContext ctx)
        {
            _movementDirection = 0;
            Animation(false);
            _isButtonPressed = false;
        }

        private void FixedUpdate()
        {
            if (_isRunning && _isButtonPressed)
            {
                _player.Rigidbody2D.velocity = _direction;
            }
            else
            {
                _player.Rigidbody2D.velocity = new Vector2(0, _player.Rigidbody2D.velocity.y);
            }
        }

        public void ReturnBaseSpeed()
        {
            _player.Rigidbody2D.velocity = new Vector2(0, _player.Rigidbody2D.velocity.y);
            CurrentSpeed = BaseSpeed;
        }

        private void Flip(float ctxValue)
        {
            if (ctxValue > 0)
            {
                IsLookingRight = true;
                _spriteTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                IsLookingRight = false;
                _spriteTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        private void Animation(bool isRunning)
        {
            _player.Animator.SetBool("isRun", isRunning);
        }
    }
}