using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BOYAREngine
{
    public class Jump : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private float _jumpForce;
        private float _distance = 0.1f;
        [Header("Ground Collision")]
        [SerializeField] private LayerMask _ground;
        [SerializeField] private Transform _leftGroundChecker;
        [SerializeField] private Transform _rightGroundChecker;

        [Space]
        [SerializeField] private bool _isJumping;
        [SerializeField] private bool _isGrounded;
        private Player _player;

        // TODO MarioJump
        [SerializeField] private float jumpTime = 0.3f;
        private float jumpTimeCounter;
        private bool _isStoppedJumping;
#pragma warning restore 649

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            // TODO MarioJump
            jumpTimeCounter = jumpTime;

            _player.Input.PlayerInGame.Jump.started += _ => Jump_started();
            _player.Input.PlayerInGame.Jump.canceled += _ => Jump_canceled();
        }

        // TODO MarioJump Update
        private void Update()
        {
            // MarioJump
            if (_isGrounded == true)
            {
                jumpTimeCounter = jumpTime;
            }
        }

        private void FixedUpdate()
        {
            CheckGround();

            if (_isJumping == true && _isStoppedJumping == false)
            {
                if (jumpTimeCounter > 0)
                {
                    _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x, _jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }
            }
        }

        private void Jump_started()
        {
            if (_isGrounded == true && _player.Crouch.HasCeiling == false)
            {
                _isJumping = true;
                // TODO MarioJump
                //var jumpVector = new Vector2(_player.Rigidbody2D.velocity.x, _jumpForce);
                //_player.Rigidbody2D.AddForce(jumpVector, ForceMode2D.Impulse);
                _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x * 2f, _jumpForce);
                _isStoppedJumping = false;
            }
        }

        private void Jump_canceled()
        {
            jumpTimeCounter = 0;
            _isStoppedJumping = true;
            _isJumping = false;
        }

        public void CheckGround()
        {
            Vector2 leftOrigin = _leftGroundChecker.position;
            Vector2 rightOrigin = _rightGroundChecker.position;
            Vector2 direction = new Vector2(0, -_distance);

            // TODO delete debug ray of jump
            Debug.DrawRay(leftOrigin, direction, Color.green, 0.8f);
            Debug.DrawRay(rightOrigin, direction, Color.yellow, 0.8f);

            RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, direction, _distance, _ground);
            RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, direction, _distance, _ground);
            if (leftHit.collider != null || rightHit.collider != null)
            {
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
            }
        }
    }
}
