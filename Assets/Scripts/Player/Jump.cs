using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BOYAREngine
{
    public class Jump : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _distance;
        [Header("Ground Collision")]
        [SerializeField] private LayerMask _ground;
        [SerializeField] private Transform _leftGroundChecker;
        [SerializeField] private Transform _rightGroundChecker;
        [Space]
        public bool IsGrounded;
        private Vector2 _jumpVector;
        private Player _player;
#pragma warning restore 649

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
            if (IsGrounded == true)
            {
                _jumpVector = new Vector2(0, _jumpForce);
                _player.Rigidbody2D.AddForce(_jumpVector, ForceMode2D.Impulse);
            }
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
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }
    }
}
