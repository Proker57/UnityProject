using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class Dash : MonoBehaviour
    {
        public float SpeedMultiplier;
        public bool IsDashable;
        [HideInInspector] public float DashTimerCounter;
        [SerializeField] private float _dashTimer = 1f;
        [Space]
        private Player _player;
        [SerializeField] private InputActionAsset _controls;

        private void Awake()
        {
            DashTimerCounter = _dashTimer;

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
        }

        private void Update()
        {
            if (!IsDashable)
            {
                DashCountdown();
            }
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
                _player.Movement.ReturnBaseSpeed();

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
