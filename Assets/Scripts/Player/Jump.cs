using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class Jump : MonoBehaviour
    {
        private Player _player;

        [SerializeField] private Transform _originOfLeftRaycast;
        [SerializeField] private Transform _originOfRightRaycast;
        [SerializeField] private LayerMask _groundLayer;

        [SerializeField] private float _jumpForce;

        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _isOnPlatform;
        public bool IsJumping;
        private bool _hasStarted;

        [SerializeField] private float _jumpTime;
        private float _jumpTimeCounter;

        private bool _hasParticleFxPlayed;
        [Space]
        [SerializeField] private InputAction _jump;
        [SerializeField] private InputActionAsset _controls;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            var iam = _controls.FindActionMap("PlayerInGame");
            _jump = iam.FindAction("Jump");
            _jump.started += Jump_started;
            _jump.canceled += Jump_canceled;
        }

        private void Update()
        {
            CheckGround();
            CheckPlatform();

            if (_isGrounded && _hasStarted)
            {
                IsJumping = true;
                _jumpTimeCounter = _jumpTime;

                PlayParticleFx();

                _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x, _jumpForce);
            }

            if (_hasStarted && IsJumping)
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

        private void CheckGround()
        {
            var hitLeft = Physics2D.Raycast(_originOfLeftRaycast.position, Vector2.down, 0.1f, _groundLayer);
            var hitRight = Physics2D.Raycast(_originOfRightRaycast.position, Vector2.down, 0.1f, _groundLayer);
            _isGrounded = hitLeft.collider || hitRight.collider != null;
        }

        private void CheckPlatform()
        {
            var hitLeft = Physics2D.Raycast(_originOfLeftRaycast.position, Vector2.down, 0.1f, LayerMask.GetMask("Platform"));
            var hitRight = Physics2D.Raycast(_originOfRightRaycast.position, Vector2.down, 0.1f, LayerMask.GetMask("Platform"));
            _isOnPlatform = hitLeft.collider || hitRight.collider != null;
        }

        private void PlayParticleFx()
        {
            if (_isGrounded)
            {
                _player.ParticleSystem.Play();
            }
        }

        private void Jump_started(InputAction.CallbackContext ctx)
        {
            _hasStarted = true;

            if (_player.Crouch.IsCrouched && _isOnPlatform)
            {
                PlayerEvents.JumpDownPlatform();
            }
        }

        private void Jump_canceled(InputAction.CallbackContext ctx)
        {
            _hasStarted = false;
            _jumpTimeCounter = 0;
        }

        private void OnEnable()
        {
            HUDEvents.JumpCheckIsActive?.Invoke(true);
        }

        private void OnDisable()
        {
            HUDEvents.JumpCheckIsActive?.Invoke(false);
        }
    }
}

