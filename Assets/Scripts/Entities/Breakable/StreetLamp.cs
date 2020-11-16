using UnityEngine;

namespace BOYAREngine
{
    public class StreetLamp : MonoBehaviour, ISaveable
    {
        public bool IsActive;

        private int _health;

        [SerializeField] private GameObject _light;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private Damageable _damageable;

        private void Awake()
        {
            IsActive = gameObject.activeSelf;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _damageable = GetComponent<Damageable>();
        }

        private void Update()
        {
            _health = _damageable.Health;

            if (_health <= 0 && IsActive)
            {
                Dead();
            }
        }

        private void Dead()
        {
            IsActive = false;

            _spriteRenderer.enabled = false;
            _boxCollider2D.enabled = false;
            _light.SetActive(false);
        }

        public object CaptureState()
        {
            return new StreetLampData
            {
                Health = _health,
                IsActive = IsActive
            };
        }

        public void RestoreState(object state)
        {
            var saveLampData = (StreetLampData) state;

            IsActive = saveLampData.IsActive;
            _health = saveLampData.Health;

            _spriteRenderer.enabled = IsActive;
            _boxCollider2D.enabled = IsActive;
            _light.SetActive(IsActive);
            _damageable.Health = _health;
        }
    }

    [System.Serializable]
    public class StreetLampData
    {
        public bool IsActive;
        public int Health;
    }
}
