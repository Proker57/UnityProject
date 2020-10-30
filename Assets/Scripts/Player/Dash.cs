using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Awake()
        {
            DashTimerCounter = _dashTimer;
            SpeedLimiterTimerCounter = _speedLimiterTimer;

            _player = GetComponent<Player>();
        }

        private void Dash_started()
        {
            if (IsDashable != true) return;
            //PlayerEvents.Dash(DashTimerCounter);

            _player.Movement.IsMaxSpeedLimiterOn = false;

            IsDashable = false;
            IsSpeedLimited = false;

            // TODO delete -1 (-1 now is a right side)
            var spriteScaleX = _player.transform.GetChild(0).transform.localScale.x * -1;
            _dashVector = new Vector2(spriteScaleX * _xVectorMultiply, _yVector);
            _player.Rigidbody2D.AddForce(_dashVector, ForceMode2D.Impulse);
        }

        private void Update()
        {
            DashCountdown();
            SpeedLimiterCountdown();
        }

        private void DashCountdown()
        {
            if (IsDashable != false) return;
            if (DashTimerCounter > 0)
            {
                DashTimerCounter -= Time.deltaTime;
            }
            else
            {
                //PlayerEvents.DashReady();
                DashTimerCounter = _dashTimer;
                IsDashable = true;
            }
        }

        private void SpeedLimiterCountdown()
        {
            if (IsSpeedLimited != false) return;
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
            _player.Input.PlayerInGame.Dash.started += _ => Dash_started();
        }

        private void OnDisable()
        {
            HUDEvents.DashCheckIsActive(false);
            _player.Input.PlayerInGame.Dash.started -= _ => Dash_started();
        }
    }
}
