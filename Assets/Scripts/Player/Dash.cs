using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace BOYAREngine
{
    public class Dash : MonoBehaviour
    {
        [SerializeField] private int _xVectorMultiply = 800;
        [SerializeField] private int _yVector = 0;
        [SerializeField] private float _speedLimiterTimer = 0.1f;
        [SerializeField] private float _dashTimer = 1f;
        [SerializeField] private float _ghostTrailTimer = 0.1f;
        [SerializeField] private bool _isDashable = true;
        public bool IsDashable
        {
            get => _isDashable;
            set => _isDashable = value;
        }
        private Player _player;
        private Vector2 _dashVector;
        [SerializeField] private List<GameObject> _ghostTrails;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _player.Input.PlayerInGame.Dash.started += _ => Dash_started();
        }

        private void Dash_started()
        {
            if (_isDashable == true)
            {
                GhostTrailOn();
                _player.Movement.IsMaxSpeedLimiterOn = false;
                _isDashable = false;
                // TODO delete -1 (-1 now is a right side)
                var spriteScaleX = _player.transform.GetChild(0).transform.localScale.x * -1;
                _dashVector = new Vector2(spriteScaleX * _xVectorMultiply, _yVector);
                _player.Rigidbody2D.AddForce(_dashVector, ForceMode2D.Impulse);
                StartCoroutine(WaitAndSetSpeedLimiter(_speedLimiterTimer));
                StartCoroutine(WaitAndSetDashable(_dashTimer));
            }
        }

        private void GhostTrailOn()
        {
            StartCoroutine(WaitAndGhostOn(_ghostTrailTimer));
        }

        public void GhostTrailOff()
        {

        }

        IEnumerator WaitAndSetSpeedLimiter(float time)
        {
            yield return new WaitForSeconds(time);
            _player.Movement.IsMaxSpeedLimiterOn = true;
        }

        IEnumerator WaitAndSetDashable(float time)
        {
            yield return new WaitForSeconds(time);
            _isDashable = true;
        }

        IEnumerator WaitAndGhostOn(float time)
        {
            var _distanceBetweenGhosts = 0.1f;
            var alphaindex = 0.2f;
            foreach (var ghost in _ghostTrails)
            {
                yield return new WaitForSeconds(time);
                var currentPostion = new Vector3(transform.position.x - _distanceBetweenGhosts, transform.position.y, transform.position.z);
                ghost.transform.position = currentPostion;
                var alpha = ghost.GetComponent<SpriteRenderer>().color;
                alpha.a -= alphaindex;
                ghost.GetComponent<SpriteRenderer>().color = alpha;
                ghost.SetActive(true);
                _distanceBetweenGhosts += 0.2f;
                alphaindex += 0.2f;
            }
        }
    }
}
