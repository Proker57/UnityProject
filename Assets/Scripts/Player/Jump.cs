using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    [Header("Ground Collision")]
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Transform _leftGroundChecker;
    [SerializeField] private Transform _rightGroundChecker;
    [SerializeField] private float distance;
    private Vector2 _jumpVector;

    public bool isGrounded;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        
        _player.Input.PlayerInGame.Jump.started += _ => Jump_started();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Jump_started()
    {
        if (isGrounded == true)
        {
            _jumpVector = new Vector2(0, _jumpForce);
            _player.Rigidbody2D.AddForce(_jumpVector, ForceMode2D.Impulse);
        }
    }

    public void CheckGround()
    {
        Vector2 leftOrigin = _leftGroundChecker.position;
        Vector2 rightOrigin = _rightGroundChecker.position;
        Vector2 direction = new Vector2(0, -distance);

        // TODO delete debug ray of jump
        Debug.DrawRay(leftOrigin, direction, Color.green, 0.8f);
        Debug.DrawRay(rightOrigin, direction, Color.yellow, 0.8f);

        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, direction, distance, _ground);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, direction, distance, _ground);
        if (leftHit.collider != null || rightHit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
