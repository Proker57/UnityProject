using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private float _speedRun;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _lerp;
    private float _movementDirection;
    private bool _isRunning;
    private bool _isLookingRight;
    [SerializeField] private bool _isMaxSpeedLimiterOn = true;
    public bool IsMaxSpeedLimiterOn
    {
        get => _isMaxSpeedLimiterOn;
        set => _isMaxSpeedLimiterOn = value;
    }
    private float Tolerance = 0;

    [Space, Header("Player sprite with bones")]
    [SerializeField] private Transform _spriteTransform;
    private Player _player;
    private Vector2 _direction;
#pragma warning restore 649

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _isRunning = Math.Abs(_movementDirection) > Tolerance;
        _movementDirection = _player.Input.PlayerInGame.Movement.ReadValue<float>();
        _direction = new Vector2(_movementDirection * _speedRun, 0);

        FlipSprite(_movementDirection);
        ChangeAnimation(_isRunning);
    }


    private void FixedUpdate()
    {
        if (_isRunning == true)
        {
            _player.Rigidbody2D.AddForce(_direction, ForceMode2D.Force);
        }

        if (_isMaxSpeedLimiterOn == true)
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

    private void FlipSprite(float direction)
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

    private void ChangeAnimation(bool isRunning)
    {
        if (isRunning == true)
        {
            _player.Animator.SetBool("isRun", true);
        }
        else
        {
            _player.Animator.SetBool("isRun", false);
        }
    }
}