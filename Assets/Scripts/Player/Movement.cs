using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speedRun;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _lerp;

    private float _movementDirection;
    private float Tolerance = 0;

    private Rigidbody2D _rigidbody2D;
    private Jump _jump;
    private Player _player;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _jump = GetComponent<Jump>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        _movementDirection = _player.Input.PlayerInGame.Movement.ReadValue<float>();
        var direction = new Vector2(_movementDirection * _speedRun, 0);
        
        if (IsRunning() == true || _jump.isGrounded == true)
        {
            _rigidbody2D.AddForce(direction, ForceMode2D.Force);

            MaxVelocityLimiter();
        }

        StopMovementLerp();
    }

    private bool IsRunning()
    {
        return Math.Abs(_movementDirection) > Tolerance;
    }

    private void MaxVelocityLimiter()
    {
        if (_rigidbody2D.velocity.x > 0 && _rigidbody2D.velocity.x >= _maxVelocity)
        {
            _rigidbody2D.velocity = new Vector2(_maxVelocity, _rigidbody2D.velocity.y);
        }
        else if (_rigidbody2D.velocity.x < 0 && _rigidbody2D.velocity.x <= _maxVelocity * -1)
        {
            _rigidbody2D.velocity = new Vector2(_maxVelocity * -1, _rigidbody2D.velocity.y);
        }
    }

    private void MaxVelocityLimiterClamp()
    {
        if (Mathf.Abs(_rigidbody2D.velocity.x) > _maxVelocity)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.normalized.x * _maxVelocity, _rigidbody2D.velocity.y);
        }
    }

    private void StopMovementLerp()
    {
        if (IsRunning() == false && _jump.isGrounded == true)
        {
            _rigidbody2D.velocity =
                new Vector2(Mathf.Lerp(_rigidbody2D.velocity.x, 0, _lerp), _rigidbody2D.velocity.y);
        }
        if (IsRunning() == false && _jump.isGrounded == false)
        {
            _rigidbody2D.velocity =
                new Vector2(_rigidbody2D.velocity.x, Mathf.Lerp(_rigidbody2D.velocity.y, 0, _lerp));
        }
    }
}