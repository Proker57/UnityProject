using Pathfinding;
using UnityEngine;

namespace BOYAREngine.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable, ISaveable
    {
        [Header("Init")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private CircleCollider2D _sightRadius;
        [SerializeField] private Animator _animator;

        [Header("A* Pathfinder")]
        [SerializeField] private AIDestinationSetter _aiDestinationSetter;
        [SerializeField] private AIPath _aiPath;
        [SerializeField] private Transform _basePosition;

        [Header("Sound")]
        public AudioSource AudioSource;
        public AudioClip HitSound;
        public AudioClip IdleSound;
        public AudioClip DeathSound;

        [Header("Vars")]
        public int AttackDamage = 1;

        public int EXP = 1;

        public float MaxSpeed = 1;
        public float SightRadius = 3;

        [Header("Serialization")]
        public int Health = 100;

        public bool IsFighting = false;
        public bool IsActive = true;

        private void Awake()
        {
            IsActive = gameObject.activeSelf;

            _aiPath.maxSpeed = MaxSpeed;
            _sightRadius.radius = SightRadius;
        }

        private void FixedUpdate()
        {
            if (_aiPath.desiredVelocity.x > 0.01f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (_aiPath.desiredVelocity.x < -0.01f)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        private void OnTriggerEnter2D(Object other)
        {
            if (other.name != "Low Collider") return;
            IsFighting = true;

            if (!AudioSource.isPlaying)
            {
                PlaySound(IdleSound);
            }

            _aiDestinationSetter.target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnTriggerExit2D(Object other)
        {
            if (other.name != "Low Collider") return;
            IsFighting = false;

            if (AudioSource.isPlaying)
            {
                StopSound();
            }

            _aiDestinationSetter.target = _basePosition;
        }

        private void PlaySound(AudioClip clip)
        {
            AudioSource.PlayOneShot(clip);
        }

        private void StopSound()
        {
            AudioSource.Stop();
        }

        public void GetDamage(int amount)
        {
            Health -= amount;

            if (Health <= 0)
            {
                Dead();
                return;
            }

            _animator.SetTrigger("Hit");

            AudioSource.PlayOneShot(HitSound);
        }

        public void Dead()
        {
            IsActive = false;

            _rigidbody2D.isKinematic = true;
            _boxCollider2D.enabled = false;
            _sightRadius.gameObject.SetActive(false);
            _animator.SetTrigger("Dead");

            _aiPath.enabled = false;

            AudioSource.PlayOneShot(DeathSound);

            IsFighting = false;

            PlayerEvents.GiveExp(EXP);

            Invoke(("Deactivate"), 1f);
        }

        private void Deactivate()
        {

            _spriteRenderer.gameObject.SetActive(false);
            _animator.enabled = false;
            AudioSource.enabled = false;
        }

        public object CaptureState()
        {
            return new EnemyData
            {
                Health = Health,
                IsActive = IsActive,
                IsFighting = IsFighting
            };
        }

        public void RestoreState(object state)
        {
            var enemyData = (EnemyData) state;

            Health = enemyData.Health;

            IsActive = enemyData.IsActive;
            IsFighting = enemyData.IsFighting;

            _spriteRenderer.gameObject.SetActive(IsActive);
            _rigidbody2D.bodyType = IsActive ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            _boxCollider2D.enabled = IsActive;
            _sightRadius.gameObject.SetActive(IsActive);
            _animator.enabled = IsActive;
            AudioSource.enabled = IsActive;

            _aiPath.enabled = IsActive;
        }
    }

    [System.Serializable]
    public class EnemyData
    {
        public int Health;
        public bool IsActive;
        public bool IsFighting;
    }
}
