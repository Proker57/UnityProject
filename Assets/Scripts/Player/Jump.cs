using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    [Header("Ground Collision")]
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Transform _origin;
    [SerializeField] private float distance;

    public bool isGrounded;

    private Rigidbody2D _rigidbody2D;
    private Player _player;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _player.Input.PlayerInGame.Jump.performed += _ => Jump_performed();
    }

    private void Jump_performed()
    {
        var jumpVector = new Vector2(0, _jumpForce);

        if (IsGrounded() == true)
        {
            _rigidbody2D.AddForce(jumpVector, ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        Vector2 origin = _origin.position;
        Vector2 direction = Vector2.down;

        Debug.DrawRay(origin, direction, Color.green, 0.8f);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, _ground);
        if (hit.collider != null)
        {
            isGrounded = true;
            return true;
        }

        isGrounded = false;
        return false;
    }
}
