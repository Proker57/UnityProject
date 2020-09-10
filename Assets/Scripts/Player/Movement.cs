using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speedRun;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _lerp;
    private float _movementDirection;
    private bool isRunning;
    private float Tolerance = 0;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        IsRunning();
    }

    private void FixedUpdate()
    {
        _movementDirection = _player.Input.PlayerInGame.Movement.ReadValue<float>();
        var direction = new Vector2(_movementDirection * _speedRun, 0);
        
        if (isRunning == true)
        {
            _player.Rigidbody2D.AddForce(direction, ForceMode2D.Force);
        }

        MaxVelocityLimiter();
        StopMovementLerp();
    }

    private void IsRunning()
    {
        isRunning = Math.Abs(_movementDirection) > Tolerance;
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
        if (isRunning == false)
        {
            _player.Rigidbody2D.velocity =
            new Vector2(Mathf.Lerp(_player.Rigidbody2D.velocity.x, 0, _lerp), 
                _player.Rigidbody2D.velocity.y);
        }
    }
}