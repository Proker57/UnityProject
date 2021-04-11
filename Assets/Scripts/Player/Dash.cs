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

        [Space]
        [SerializeField] private InputActionAsset _controls;

        private void Awake()
        {
            DashTimerCounter = _dashTimer;
            SpeedLimiterTimerCounter = _speedLimiterTimer;

            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _controls.FindActionMap("PlayerInGame").FindAction("Dash").started += Dash_started;
        }

        private void Dash_started(InputAction.CallbackContext ctx)
        {
            if (!IsDashable) return;

            PlayerEvents.Dash();
            _player.Movement.IsMaxSpeedLimiterOn = false;

            IsDashable = false;
            IsSpeedLimited = false;

            var dashVector = _player.Movement.IsLookingRight
                ? new Vector2(_xVectorMultiply, _yVector)
                : new Vector2(-_xVectorMultiply, _yVector);
            _player.Rigidbody2D.AddForce(dashVector, ForceMode2D.Impulse);
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
            HUDEvents.DashCheckIsActive(true);
        }

        private void OnDisable()
        {
            HUDEvents.DashCheckIsActive?.Invoke(false);
        }
    }
}
