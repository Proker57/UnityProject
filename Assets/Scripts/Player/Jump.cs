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
        [SerializeField] private float _force;
        [SerializeField] private float _releaseTime;
        private float _releaseTimeCounter;

        [Header("Jump Off Platform")]
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _platformLayer;

        [Header("Logic")]
        public bool CanJump = true;
        public bool IsJumping;
        private bool _hasPressed;
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
                if (_hasPressed)
                {
                    JumpAction();
                    ControlHeight();
                }
            }
        }

        private void Jump_started(InputAction.CallbackContext ctx)
        {
            _hasPressed = true;

            JumpOffPlatform();
        }


        private void Jump_canceled(InputAction.CallbackContext ctx)
        {
            _hasPressed = false;
            _releaseTimeCounter = 0;
        }

        private void JumpAction()
        {
            if (IsOnGround() && _hasPressed && !(IsOnPlatform() && _player.Crouch.IsCrouched))
            {
                AbilityToJump();

                PlayParticleFx();

                _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x, _force);
            }
        }

        private void ControlHeight()
        {
            if (_hasPressed && IsJumping)
            {
                if (_releaseTimeCounter > 0)
                {
                    _player.Rigidbody2D.velocity = new Vector2(_player.Rigidbody2D.velocity.x, _force);
                    _releaseTimeCounter -= Time.deltaTime;
                }
                else
                {
                    IsJumping = false;
                }
            }
        }

        private void JumpOffPlatform()
        {
            if (_player.Crouch.IsCrouched && IsOnPlatform())
            {
                PlayerEvents.JumpDownPlatform();
            }
        }

        public void AbilityToJump()
        {
            IsJumping = true;
            _releaseTimeCounter = _releaseTime;
        }

        private bool IsOnGround()
        {
            var hitLeft = Physics2D.Raycast(_originOfLeftRaycast.position, Vector2.down, 0.1f, _groundLayer);
            var hitRight = Physics2D.Raycast(_originOfRightRaycast.position, Vector2.down, 0.1f, _groundLayer);
            return hitLeft.collider || hitRight.collider != null;
        }

        private bool IsOnPlatform()
        {
            var hitLeft = Physics2D.Raycast(_originOfLeftRaycast.position, Vector2.down, 0.1f, LayerMask.GetMask("Platform"));
            var hitRight = Physics2D.Raycast(_originOfRightRaycast.position, Vector2.down, 0.1f, LayerMask.GetMask("Platform"));
            return hitLeft.collider && hitRight.collider != null;
        }

        private void PlayParticleFx()
        {
            if (IsOnGround())
            {
                _player.DustFx.Play();
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

