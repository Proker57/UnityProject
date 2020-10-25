using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace BOYAREngine
{
    public class Movement : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private float _speed;          // 700
        [SerializeField] private float _crouchSpeedMultiplier = 0.8f;   // 0.8f
        private float _speedRun;                        // _speed
        private float _speedCrouch;                     // _speed * _crouchSpeedMultiplier
        [SerializeField] private float _maxVelocity;    // 2f
        private float _maxVelocityRun;                  // _maxVelocity
        private float _maxVelocityCrouch;               // _maxVelocity * _crouchSpeedMultiplier
        [SerializeField] private float _lerp;           // 0.2f
        private float _movementDirection;
        private bool _isRunning;
        private bool _isLookingRight;

        [field: SerializeField]
        public bool IsMaxSpeedLimiterOn { get; set; } = true;

        private const float Tolerance = 0;

        [Space, Header("Player sprite with bones")]
        [SerializeField] private Transform _spriteTransform;
        private Player _player;
        private Vector2 _direction;
#pragma warning restore 649

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _speedRun = _speed;
            _speedCrouch = _speed * _crouchSpeedMultiplier;
            _maxVelocityRun = _maxVelocity;
            _maxVelocityCrouch = _maxVelocity * _crouchSpeedMultiplier;
        }

        private void Update()
        {
            _isRunning = Math.Abs(_movementDirection) > Tolerance;
            _movementDirection = _player.Input.PlayerInGame.Movement.ReadValue<float>();
            _direction = new Vector2(_movementDirection * _speed, 0);

            CrouchSpeedCheck();
            FlipSprite();
            ChangeAnimation();
        }


        private void FixedUpdate()
        {
            if (_isRunning == true)
            {
                _player.Rigidbody2D.AddForce(_direction, ForceMode2D.Force);
            }

            if (IsMaxSpeedLimiterOn == true)
            {
                MaxVelocityLimiter();
            }
            StopMovementLerp();
        }


        private void MaxVelocityLimiter()
        {
            if (_player.Rigidbody2D.velocity.x >= _maxVelocity)
            {
                _player.Rigidbody2D.velocity = new Vector2(_maxVelocity, _player.Rigidbody2D.velocity.y);
            }
            if (_player.Rigidbody2D.velocity.x <= _maxVelocity * -1)
            {
                _player.Rigidbody2D.velocity = new Vector2(_maxVelocity * -1, _player.Rigidbody2D.velocity.y);
            }
        }

        private void StopMovementLerp()
        {
            if (_isRunning == false)
            {
                _player.Rigidbody2D.velocity =
                new Vector2(Mathf.Lerp(_player.Rigidbody2D.velocity.x, 0, _lerp),
                    _player.Rigidbody2D.velocity.y);
            }
        }

        private void FlipSprite()
        {
            if (_movementDirection > 0)
            {
                _isLookingRight = true;

            }
            if (_movementDirection < 0)
            {
                _isLookingRight = false;

            }

            if (_isLookingRight)
            {
                _spriteTransform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                _spriteTransform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        private void ChangeAnimation()
        {
            if (_isRunning == true)
            {
                _player.Animator.SetBool("isRun", true);
            }
            else
            {
                _player.Animator.SetBool("isRun", false);
            }
        }

        private void CrouchSpeedCheck()
        {
            if (_player.Crouch.IsCrouched)
            {
                _speed = _speedCrouch;
                _maxVelocity = _maxVelocityCrouch;
            }
            else
            {
                _speed = _speedRun;
                _maxVelocity = _maxVelocityRun;
            }
        }
    }
}