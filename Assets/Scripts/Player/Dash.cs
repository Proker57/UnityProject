using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class Dash : MonoBehaviour
    {
        public float SpeedMultiplier;
        public bool IsDashable;
        [HideInInspector] public float DashTimerCurrent;
        [SerializeField] private float _dashTimer = 1f;

        [SerializeField] private float _nextDashTimer = 0.3f;
        private float _nextDashTimerCurrent;
        private bool _isDashFinished;
        [Space]
        private Player _player;
        [SerializeField] private InputActionAsset _controls;

        private void Awake()
        {
            DashTimerCurrent = _dashTimer;

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
            _player.Movement.CurrentSpeed += SpeedMultiplier;
            IsDashable = false;
            _isDashFinished = false;
        }

        private void Update()
        {
            if (!IsDashable)
            {
                Cooldown();
            }

            if (!_isDashFinished)
            {
                DashingTime();
            }
        }

        private void Cooldown()
        {
            if (IsDashable) return;
            if (DashTimerCurrent > 0)
            {
                DashTimerCurrent -= Time.deltaTime;
            }
            else
            {
                DashTimerCurrent = _dashTimer;
                PlayerEvents.DashReady();
                IsDashable = true;
            }
        }

        private void DashingTime()
        {
            if (_isDashFinished && IsDashable) return;
            if (_nextDashTimerCurrent > 0)
            {
                _nextDashTimerCurrent -= Time.deltaTime;
            }
            else
            {
                _nextDashTimerCurrent = _nextDashTimer;
                _isDashFinished = true;
                _player.Movement.ReturnBaseSpeed();
            }
        }

        private void OnEnable()
        {
            HUDEvents.DashCheckIsActive?.Invoke(true);
        }

        private void OnDisable()
        {
            HUDEvents.DashCheckIsActive?.Invoke(false);
        }
    }
}
