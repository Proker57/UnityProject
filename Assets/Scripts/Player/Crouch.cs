using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class Crouch : MonoBehaviour
    {
        public Transform LeftCeilingChecker;
        public Transform RightCeilingChecker;
        public LayerMask Ground;
        public bool IsCrouched;
        public bool HasCeiling;
        [SerializeField] private float _distance;   // 0.05f

        private bool _isButtonPressed;
        [Space]
        private Player _player;
        [SerializeField] private InputActionAsset _controls;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _controls.FindActionMap("PlayerInGame").FindAction("Crouch").started += Crouch_started;
            _controls.FindActionMap("PlayerInGame").FindAction("Crouch").canceled += Crouch_canceled;
        }

        private void Update()
        {
            if (IsCrouched)
            {
                CheckForCeiling();
            }

            if (IsCrouched && !HasCeiling && !_isButtonPressed)
            {
                StopCrouch();
            }
        }


        private void Crouch_started(InputAction.CallbackContext ctx)
        {
            _isButtonPressed = true;

            StartCrouch();
        }

        private void Crouch_canceled(InputAction.CallbackContext ctx)
        {
            _isButtonPressed = false;

            if (HasCeiling == false)
            {
                StopCrouch();
            }

            IsCrouched = HasCeiling;
        }

        private void StartCrouch()
        {
            _player.HighCollider.enabled = false;
            IsCrouched = true;
            _player.Movement.CurrentSpeed *= 0.8f;
            _player.Animator.SetBool("isCrouch", true);
        }

        private void StopCrouch()
        {
            if (!HasCeiling && !_isButtonPressed)
            {
                _player.HighCollider.enabled = true;
                IsCrouched = false;
                _player.Movement.ReturnBaseSpeed();
                _player.Animator.SetBool("isCrouch", false);
            }
        }

        private void CheckForCeiling()
        {
            var leftCeilingOrigin = LeftCeilingChecker.position;
            var rightCeilingOrigin = RightCeilingChecker.position;
            var direction = Vector2.up * _distance;

            var leftCeilingHit = Physics2D.Raycast(leftCeilingOrigin, direction, _distance, Ground);
            var rightCeilingHit = Physics2D.Raycast(rightCeilingOrigin, direction, _distance, Ground);
            if (leftCeilingHit.collider != null || rightCeilingHit.collider != null)
            {
                HasCeiling = true;
            }
            else
            {
                HasCeiling = false;
            }
        }
    }
}
