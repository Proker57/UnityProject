using UnityEngine;

namespace BOYAREngine
{
    public class Crouch : MonoBehaviour
    {
#pragma warning disable 649
        public bool IsCrouched;
        public bool HasCeiling;
        [SerializeField] private float _distance;   // 0.05f
        public Transform LeftCeilingChecker;
        public Transform RightCeilingChecker;
        public LayerMask Ground;

        [SerializeField] private bool _isButtonPressed;
        private Player _player;
#pragma warning restore 649

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (IsCrouched && _isButtonPressed == false)
            {
                StartCrouch();
            }
        }

        private void FixedUpdate()
        {
            var leftCeilingOrigin = LeftCeilingChecker.position;
            var rightCeilingOrigin = RightCeilingChecker.position;
            var direction = new Vector2(0, _distance);

            // TODO delete debug ray of jump
            //Debug.DrawRay(leftCeilingOrigin, direction, Color.red, 0.8f);
            //Debug.DrawRay(rightCeilingOrigin, direction, Color.blue, 0.8f);
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

            if (HasCeiling == false && _isButtonPressed == false)
            {
                StopCrouch();
            }
        }

        private void Crouch_started()
        {
            _isButtonPressed = true;

            StartCrouch();
        }

        private void Crouch_canceled()
        {
            _isButtonPressed = false;

            if (HasCeiling == false)
            {
                StopCrouch();
            }

            IsCrouched = HasCeiling == true;
        }

        private void StartCrouch()
        {
            _player.HighCollider.enabled = false;
            IsCrouched = true;
            _player.Animator.SetBool("isCrouch", true);
        }

        private void StopCrouch()
        {
            _player.HighCollider.enabled = true;
            IsCrouched = false;
            _player.Animator.SetBool("isCrouch", false);
        }

        private void OnEnable()
        {
            _player.Input.PlayerInGame.Crouch.started += _ => Crouch_started();
            _player.Input.PlayerInGame.Crouch.canceled += _ => Crouch_canceled();
        }

        private void OnDisable()
        {
            _player.Input.PlayerInGame.Crouch.started -= _ => Crouch_started();
            _player.Input.PlayerInGame.Crouch.canceled -= _ => Crouch_canceled();
        }
    }
}
