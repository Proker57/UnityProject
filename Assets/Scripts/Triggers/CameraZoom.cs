using Cinemachine;
using UnityEngine;

namespace BOYAREngine
{
    public class CameraZoom : MonoBehaviour, ISaveable
    {
        public bool IsActive = true;

        private CinemachineVirtualCamera _camera;
        [SerializeField] private float _size;
        [SerializeField] private float _zoomTime;

        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private float _currentTime = 0;
        private float _startSize;
        private bool _isStarted;


        private void Awake()
        {
            IsActive = gameObject.activeSelf;

            _camera = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
            _startSize = _camera.m_Lens.OrthographicSize;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name == "Low Collider")
            {
                if (IsActive)
                {
                    _isStarted = true;
                }
            }
        }

        private void Update()
        {
            if (_isStarted)
            {
                if (_currentTime <= _zoomTime)
                {
                    _currentTime += Time.deltaTime;
                    _camera.m_Lens.OrthographicSize = Mathf.Lerp(_startSize, _size, _currentTime / _zoomTime);
                }
                else
                {
                    _currentTime = 0f;
                    _isStarted = false;
                    IsActive = false;
                }
            }
        }

        public object CaptureState()
        {
            return new CameraZoomData()
            {
                CurrentTime = _currentTime,
                IsActive = IsActive,
                IsStarted = _isStarted
            };

        }

        public void RestoreState(object state)
        {
            var data = (CameraZoomData) state;
            _currentTime = data.CurrentTime;
            IsActive = data.IsActive;
            _isStarted = data.IsStarted;

            _spriteRenderer.gameObject.SetActive(IsActive);
            _boxCollider2D.enabled = IsActive;
        }
    }

    [System.Serializable]
    public class CameraZoomData
    {
        public float CurrentTime;
        public bool IsActive;
        public bool IsStarted;
    }
}

