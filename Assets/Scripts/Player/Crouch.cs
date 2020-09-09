using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public bool isCrouched;
    private BoxCollider2D _lowCollider;
    private CapsuleCollider2D _highCollider;
    private Player _player;

    private void Awake()
    {
        _lowCollider = GetComponent<BoxCollider2D>();
        _highCollider = GetComponent<CapsuleCollider2D>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _player.Input.PlayerInGame.Crouch.performed += _ => Crouch_performed();
        _player.Input.PlayerInGame.Crouch.canceled += _ => Crouch_canceled();
    }

    private void Crouch_performed()
    {
        isCrouched = true;
    }

    private void Crouch_canceled()
    {
        isCrouched = false;
    }
}
