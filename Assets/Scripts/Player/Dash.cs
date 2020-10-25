using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class Dash : MonoBehaviour
    {
        [SerializeField] private int _xVectorMultiply = 800;
        [SerializeField] private int _yVector = 0;
        [SerializeField] private float _speedLimiterTimer = 0.1f;
        [SerializeField] private float _dashTimer = 1f;
        [SerializeField] private float _ghostTrailTimer = 0.1f;
        [SerializeField] private float _distanceBetweenGhosts = 0.1f;

        [field: SerializeField]
        public bool IsDashable { get; set; } = true;

        private Player _player;
        private Vector2 _dashVector;
        [SerializeField] private List<GameObject> _ghostTrails;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Dash_started()
        {
            if (IsDashable != true) return;
            Events.Dash(_dashTimer);
            _player.Movement.IsMaxSpeedLimiterOn = false;
            IsDashable = false;
            // TODO delete -1 (-1 now is a right side)
            var spriteScaleX = _player.transform.GetChild(0).transform.localScale.x * -1;
            _dashVector = new Vector2(spriteScaleX * _xVectorMultiply, _yVector);
            _player.Rigidbody2D.AddForce(_dashVector, ForceMode2D.Impulse);
            StartCoroutine(WaitAndSetSpeedLimiter(_speedLimiterTimer));
            StartCoroutine(WaitAndSetDashable(_dashTimer));
        }

        private void OnEnable()
        {
            _player.Input.PlayerInGame.Dash.started += _ => Dash_started();
        }

        private void OnDisable()
        {
            _player.Input.PlayerInGame.Dash.started -= _ => Dash_started();
        }

        private IEnumerator WaitAndSetSpeedLimiter(float time)
        {
            yield return new WaitForSeconds(time);
            _player.Movement.IsMaxSpeedLimiterOn = true;
        }

        private IEnumerator WaitAndSetDashable(float time)
        {
            yield return new WaitForSeconds(time);
            IsDashable = true;
            Events.DashReady();
        }

        private IEnumerator WaitAndGhostOn(float time)
        {
            var alphaIndex = 0.2f;
            foreach (var ghost in _ghostTrails)
            {
                yield return new WaitForSeconds(time);
                var currentPosition = new Vector3(transform.position.x - _distanceBetweenGhosts, transform.position.y, transform.position.z);
                ghost.transform.position = currentPosition;
                var alpha = ghost.GetComponent<SpriteRenderer>().color;
                alpha.a -= alphaIndex;
                ghost.GetComponent<SpriteRenderer>().color = alpha;
                ghost.SetActive(true);
                _distanceBetweenGhosts += 0.2f;
                alphaIndex += 0.2f;
            }
        }
    }
}
