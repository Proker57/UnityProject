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
        public int JumpExtraCountDefault;

        [Header("Ground Collision")]
        [SerializeField] private LayerMask _ground;
        [SerializeField] private Transform _leftGroundChecker;
        [SerializeField] private Transform _rightGroundChecker;

        [Header("Jump logic")]
        public bool IsJumping;
        public bool IsDoubleJumping;
        public bool IsStoppedJumping;
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
            JumpExtraCountDefault = JumpExtraCounts;

            _player.Input.PlayerInGame.Jump.started += _ => Jump_started();
            _player.Input.PlayerInGame.Jump.canceled += _ => Jump_canceled();

            HUDEvents.JumpCheckIsActive(JumpExtraCounts > 0);
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
            CheckGround();

            if (IsJumping == true && IsStoppedJumping == false)
            {
                if (_jumpTimeCounter > 0)
                {
                    _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x, _jumpForce);
                    _jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    IsJumping = false;
                }
            }

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
                //PlayerEvents.DoubleJump();
                JumpAction();
            }
        }

        private void JumpAction()
        {
            JumpExtraCounts--;
            IsJumping = true;
            _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x * 2f, _jumpForce);
            IsStoppedJumping = false;
        }

        private void Jump_canceled()
        {
            _jumpTimeCounter = 0;
            IsStoppedJumping = true;
            IsJumping = false;
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
                JumpExtraCounts = JumpExtraCountDefault;
                IsDoubleJumping = false;
                _isGrounded = true;
                //PlayerEvents.DoubleJumpReady();
            }
            else
            {
                _isGrounded = false;
            }
        }

        private void OnEnable()
        {
            HUDEvents.JumpCheckIsActive(true);
        }

        private void OnDisable()
        {
            HUDEvents.JumpCheckIsActive(false);
        }
    }
}
