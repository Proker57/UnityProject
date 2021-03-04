using UnityEngine;

namespace BOYAREngine
{
    public class StreetLamp : MonoBehaviour, ISaveable, IDamageable
    {
        public bool IsActive;

        public int Health;

        [SerializeField] private GameObject _light;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private IDamageable _damageable;

        private void Awake()
        {
            IsActive = gameObject.activeSelf;

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public void GetDamage(int amount)
        {
            Health -= amount;

            if (Health <= 0 && IsActive)
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
                Health = Health,
                IsActive = IsActive
            };
        }

        public void RestoreState(object state)
        {
            var saveLampData = (StreetLampData) state;

            IsActive = saveLampData.IsActive;
            Health = saveLampData.Health;

            _spriteRenderer.enabled = IsActive;
            _boxCollider2D.enabled = IsActive;
            _light.SetActive(IsActive);
        }
    }

    [System.Serializable]
    public class StreetLampData
    {
        public bool IsActive;
        public int Health;
    }
}
