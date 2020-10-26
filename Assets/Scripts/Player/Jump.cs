using UnityEngine;

namespace BOYAREngine
{
    public class Jump : MonoBehaviour
    {
#pragma warning disable 649
        [Header("Jump settings")]
        [SerializeField] private float _jumpForce;                  // 3
        [SerializeField] private float jumpTime = 0.3f;             // 0.3
        private float _jumpTimeCounter;
        private float _distance = 0.1f;                             // 0.1f

        [Header("Double jump")]
        [SerializeField]
        public int JumpExtraCounts;                                 // 1
        private int _jumpExtraCountDefault;

        [Header("Ground Collision")]
        [SerializeField] private LayerMask _ground;
        [SerializeField] private Transform _leftGroundChecker;
        [SerializeField] private Transform _rightGroundChecker;

        [Header("Jump logic")]
        [SerializeField] private bool _isJumping;
        public bool IsDoubleJumping;
        private bool _isStoppedJumping;
        [SerializeField] private bool _isGrounded;

        private Player _player;
#pragma warning restore 649

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _jumpTimeCounter = jumpTime;
            _jumpExtraCountDefault = JumpExtraCounts;

            _player.Input.PlayerInGame.Jump.started += _ => Jump_started();
            _player.Input.PlayerInGame.Jump.canceled += _ => Jump_canceled();
        }

        private void Update()
        {
            if (_isGrounded == true)
            {
                _jumpTimeCounter = jumpTime;
            }
        }

        private void FixedUpdate()
        {
            if (_isJumping == true && _isStoppedJumping == false)
            {
                if (_jumpTimeCounter > 0)
                {
                    _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x, _jumpForce);
                    _jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    _isJumping = false;
                }
            }

            CheckGround();
        }

        private void Jump_started()
        {
            if (_isGrounded == true && _player.Crouch.HasCeiling == false)
            {
                JumpAction();
            }
            if (_isGrounded == false && JumpExtraCounts > 0)
            {
                IsDoubleJumping = true;
                Events.DoubleJump();
                JumpAction();
            }
        }

        private void JumpAction()
        {
            JumpExtraCounts--;
            _isJumping = true;
            _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x * 2f, _jumpForce);
            _isStoppedJumping = false;
        }

        private void Jump_canceled()
        {
            _jumpTimeCounter = 0;
            _isStoppedJumping = true;
            _isJumping = false;
        }

        public void CheckGround()
        {
            var leftOrigin = _leftGroundChecker.position;
            var rightOrigin = _rightGroundChecker.position;
            var direction = new Vector2(0, -_distance);

            // TODO delete debug ray of jump
            Debug.DrawRay(leftOrigin, direction, Color.green, 0.8f);
            Debug.DrawRay(rightOrigin, direction, Color.yellow, 0.8f);

            var leftHit = Physics2D.Raycast(leftOrigin, direction, _distance, _ground);
            var rightHit = Physics2D.Raycast(rightOrigin, direction, _distance, _ground);
            if (leftHit.collider != null || rightHit.collider != null)
            {
                JumpExtraCounts = _jumpExtraCountDefault;
                IsDoubleJumping = false;
                _isGrounded = true;
                Events.DoubleJumpReady();
            }
            else
            {
                _isGrounded = false;
            }
        }
    }
}
