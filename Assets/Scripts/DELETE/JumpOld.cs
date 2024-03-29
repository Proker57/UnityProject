using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class JumpOld : MonoBehaviour
    {
#pragma warning disable 649
        [Header("Jump settings")]
        [SerializeField] private float _jumpForce;                  // 3
        [SerializeField] private float _jumpTime = 0.3f;             // 0.3
        [SerializeField] private float _jumpTimeCounter;
        private float _distance = 0.1f;                             // 0.1f

        [Header("Double jump")]
        [SerializeField]
        public int JumpExtraCounts;                                 // 1
        public int JumpExtraCountDefault;

        [Header("Ground Collision")]
        [SerializeField] private LayerMask _ground;
        [SerializeField] private LayerMask _platform;
        [SerializeField] private Transform _leftGroundChecker;
        [SerializeField] private Transform _rightGroundChecker;

        [Header("Jump logic")]
        public bool IsJumping;
        public bool IsDoubleJumping;
        public bool IsStoppedJumping;
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _isOnPlatform;

        private Player _player;

        [Space]
        [SerializeField] private InputAction _jump;
        [SerializeField] private InputActionAsset _controls;
#pragma warning restore 649

        private bool _canJump;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _jumpTimeCounter = _jumpTime;
            JumpExtraCountDefault = JumpExtraCounts;

            HUDEvents.JumpCheckIsActive(JumpExtraCounts > 0);

            var iam = _controls.FindActionMap("PlayerInGame");
            _jump = iam.FindAction("Jump");
            _jump.started += Jump_started;
            _jump.canceled += Jump_canceled;
        }

        private void Update()
        {
            if (_isGrounded || _isOnPlatform)
            {
                _jumpTimeCounter = _jumpTime;
            }

            CheckGround();
            CheckPlatform();

            if (!IsJumping || IsStoppedJumping) return;
            if (_jumpTimeCounter > 0f)
            {
                _canJump = true;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                IsJumping = false;
                _canJump = false;
            }
        }

        private void FixedUpdate()
        {
            //CheckGround();
            //CheckPlatform();

            if (!_canJump) return;
            _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x, _jumpForce);
            //_jumpTimeCounter -= Time.fixedDeltaTime;
        }

        private void DoJump()
        {

        }

        private void Jump_started(InputAction.CallbackContext ctx)
        {
            if (_player.Crouch.IsCrouched && _isOnPlatform)
            {
                PlayerEvents.JumpDownPlatform();
                return;
            }

            if ((_isGrounded || _isOnPlatform) && !_player.Crouch.HasCeiling)
            {
                JumpAction();
            }

            if ((_isGrounded || _isOnPlatform) || JumpExtraCounts <= 0) return;
            IsDoubleJumping = true;
            PlayerEvents.DoubleJump();
            JumpAction();
        }

        private void JumpAction()
        {
            JumpExtraCounts--;
            IsJumping = true;
            _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x * 2f, _jumpForce);
            //_player.DustFx.Play();
            IsStoppedJumping = false;
        }

        private void Jump_canceled(InputAction.CallbackContext ctx)
        {
            _jumpTimeCounter = 0;
            IsStoppedJumping = true;
            IsJumping = false;

            _canJump = false;
        }

        public void CheckGround()
        {
            var leftOrigin = _leftGroundChecker.position;
            var rightOrigin = _rightGroundChecker.position;
            var direction = new Vector2(0, -_distance);
            var leftHit = Physics2D.Raycast(leftOrigin, direction, _distance, _ground);
            var rightHit = Physics2D.Raycast(rightOrigin, direction, _distance, _ground);
            if (leftHit.collider != null || rightHit.collider != null)
            {
                JumpExtraCounts = JumpExtraCountDefault;
                IsDoubleJumping = false;
                PlayerEvents.DoubleJumpReady();
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
            }
        }

        public void CheckPlatform()
        {
            var leftOrigin = _rightGroundChecker.position;
            var rightOrigin = _leftGroundChecker.position;
            var direction = new Vector2(0, -_distance);
            var leftHit = Physics2D.Raycast(leftOrigin, direction, _distance, _platform);
            var rightHit = Physics2D.Raycast(rightOrigin, direction, _distance, _platform);
            if (leftHit.collider != null || rightHit.collider != null)
            {
                _isOnPlatform = true;
            }
            else
            {
                _isOnPlatform = false;
            }
        }

        private void OnEnable()
        {
            HUDEvents.JumpCheckIsActive(true);
        }

        private void OnDisable()
        {
            //_player.Input.PlayerInGame.Jump.Dispose();

            HUDEvents.JumpCheckIsActive?.Invoke(false);
        }
    }
}
