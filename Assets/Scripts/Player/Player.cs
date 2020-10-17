using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInput Input;
    public Rigidbody2D Rigidbody2D;
    public Jump Jump;
    public BoxCollider2D LowCollider;
    public CapsuleCollider2D HighCollider;
    public Animator Animator;
    public Dash Dash;
    public Movement Movement;

    private void Awake()
    {
        Input = new PlayerInput();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Jump = GetComponent<Jump>();
        LowCollider = GetComponent<BoxCollider2D>();
        HighCollider = GetComponent<CapsuleCollider2D>();
        Animator = GetComponent<Animator>();
        Dash = GetComponent<Dash>();
        Movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        Input.Enable();
        Jump.enabled = true;
        LowCollider.enabled = true;
        HighCollider.enabled = true;
        Animator.enabled = true;
        Dash.enabled = true;
        Movement.enabled = true;
    }

    private void OnDisable()
    {
        Input.Disable();
        Jump.enabled = false;
        LowCollider.enabled = false;
        HighCollider.enabled = false;
        Animator.enabled = false;
        Dash.enabled = false;
        Movement.enabled = false;
    }
}
