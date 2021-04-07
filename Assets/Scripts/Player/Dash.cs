using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class Dash : MonoBehaviour
    {
        public bool IsDashable;
        public bool IsSpeedLimited;

        [SerializeField] private int _xVectorMultiply = 800;
        [SerializeField] private int _yVector = 0;

        [SerializeField] private float _speedLimiterTimer = 0.1f;
        public float SpeedLimiterTimerCounter;

        [SerializeField] private float _dashTimer = 1f;
        public float DashTimerCounter;

        private Player _player;

        private Vector2 _dashVector;

        [Space]
        [SerializeField] private InputAction _dash;
        [SerializeField] private InputActionAsset _controls;

        private void Awake()
        {
            DashTimerCounter = _dashTimer;
            SpeedLimiterTimerCounter = _speedLimiterTimer;

            _player = GetComponent<Player>();
        }

        private void Start()
        {
            var iam = _controls.FindActionMap("PlayerInGame");
            _dash = iam.FindAction("Dash");
            _dash.started += Dash_started;
        }

        private void Dash_started(InputAction.CallbackContext ctx)
        {
            if (!IsDashable) return;

            PlayerEvents.Dash();
            _player.Movement.IsMaxSpeedLimiterOn = false;

            IsDashable = false;
            IsSpeedLimited = false;

            _dashVector = _player.Movement.IsLookingRight
                ? new Vector2(_xVectorMultiply, _yVector)
                : new Vector2(-_xVectorMultiply, _yVector);
            _player.Rigidbody2D.AddForce(_dashVector, ForceMode2D.Impulse);
        }

        private void Update()
        {
            DashCountdown();
            SpeedLimiterCountdown();
        }

        private void DashCountdown()
        {
            if (IsDashable) return;
            if (DashTimerCounter > 0)
            {
                DashTimerCounter -= Time.deltaTime;
            }
            else
            {
                DashTimerCounter = _dashTimer;
                PlayerEvents.DashReady();
                IsDashable = true;
            }
        }

        private void SpeedLimiterCountdown()
        {
            if (IsSpeedLimited) return;
            if (SpeedLimiterTimerCounter > 0)
            {
                SpeedLimiterTimerCounter -= Time.deltaTime;
            }
            else
            {
                _player.Movement.IsMaxSpeedLimiterOn = true;
                SpeedLimiterTimerCounter = _speedLimiterTimer;
                IsSpeedLimited = true;
            }
        }

        private void OnEnable()
        {
            //_playerInput.Enable();
            //_playerInput.PlayerInGame.Dash.started += _ => Dash_started();
            HUDEvents.DashCheckIsActive(true);
        }

        private void OnDisable()
        {
            //_playerInput.PlayerInGame.Dash.started -= _ => Dash_started();
            //_playerInput.Disable();
            HUDEvents.DashCheckIsActive?.Invoke(false);
        }
    }
}
