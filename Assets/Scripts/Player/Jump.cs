using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private Transform _originOfLeftRaycast;
        [SerializeField] private Transform _originOfRightRaycast;
        [SerializeField] private LayerMask _groundLayer;

        [Header("Jump Vars")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpTime;
        private float _jumpTimeCounter;

        [Header("Jump Off Platform")]
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _platformLayer;

        [Header("Logic")]
        public bool CanJump = true;
        public bool IsJumping;
        private bool _buttonPressed;
        [Space]
        [SerializeField] private InputActionAsset _controls;
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _controls.FindActionMap("PlayerInGame").FindAction("Jump").started += Jump_started;
            _controls.FindActionMap("PlayerInGame").FindAction("Jump").canceled += Jump_canceled;
        }

        private void Update()
        {
            if (CanJump && !_player.Crouch.HasCeiling)
            {
                if (_buttonPressed)
                {
                    JumpAction();
                    HeightControl();
                }
            }
        }

        private void Jump_started(InputAction.CallbackContext ctx)
        {
            _buttonPressed = true;

            JumpOffPlatform();
        }


        private void Jump_canceled(InputAction.CallbackContext ctx)
        {
            _buttonPressed = false;
            _jumpTimeCounter = 0;
        }

        private void JumpAction()
        {
            if (IsOnGround() && _buttonPressed && !(CheckPlatform() && _player.Crouch.IsCrouched))
            {
                AbilityToJump();

                PlayParticleFx();

                _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x, _jumpForce);
            }
        }

        private void HeightControl()
        {
            if (_buttonPressed && IsJumping)
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

        private void JumpOffPlatform()
        {
            if (_player.Crouch.IsCrouched && CheckPlatform())
            {
                PlayerEvents.JumpDownPlatform();
            }
        }

        public void AbilityToJump()
        {
            IsJumping = true;
            _jumpTimeCounter = _jumpTime;
        }

        private bool IsOnGround()
        {
            var hitLeft = Physics2D.Raycast(_originOfLeftRaycast.position, Vector2.down, 0.1f, _groundLayer);
            var hitRight = Physics2D.Raycast(_originOfRightRaycast.position, Vector2.down, 0.1f, _groundLayer);
            return hitLeft.collider || hitRight.collider != null;
        }

        private bool CheckPlatform()
        {
            var hitLeft = Physics2D.Raycast(_originOfLeftRaycast.position, Vector2.down, 0.1f, LayerMask.GetMask("Platform"));
            var hitRight = Physics2D.Raycast(_originOfRightRaycast.position, Vector2.down, 0.1f, LayerMask.GetMask("Platform"));
            return hitLeft.collider && hitRight.collider != null;
        }

        private void PlayParticleFx()
        {
            if (IsOnGround())
            {
                _player.ParticleSystem.Play();
            }
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

