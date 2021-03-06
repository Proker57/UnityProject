using BOYAREngine.Enemies.AI;
using UnityEngine;

namespace BOYAREngine.Enemies
{
    [RequireComponent(typeof(AIBase))]
    public class Enemy : MonoBehaviour, IDamageable, ISaveable
    {
        [Header("Init")]
        public SpriteRenderer SpriteRenderer;
        public CircleCollider2D SightRadius;
        [HideInInspector] public Rigidbody2D Rigidbody2D;
        [HideInInspector] public BoxCollider2D BoxCollider2D;
        [HideInInspector] public Animator Animator;

        [Header("AI")]
        [HideInInspector] public AIBase _aiBase;

        [Header("Sound")]
        public AudioSource AudioSource;
        public AudioClip HitSound;
        public AudioClip IdleSound;
        public AudioClip DeathSound;

        [Header("Vars")]
        public int AttackDamage = 1;
        public int EXP = 1;

        public float MaxSpeed = 1;

        [Header("Serialization")]
        public int Health = 100;

        public bool IsFighting = false;
        public bool IsActive = true;

        private void Awake()
        {
            IsActive = gameObject.activeSelf;

            Rigidbody2D = GetComponent<Rigidbody2D>();
            BoxCollider2D = GetComponent<BoxCollider2D>();
            Animator = GetComponent<Animator>();
            _aiBase = GetComponent<AIBase>();
        }

        private void OnTriggerEnter2D(Object other)
        {
            if (other.name != "Low Collider") return;
            IsFighting = true;

            if (!AudioSource.isPlaying)
            {
                PlaySound(IdleSound);
            }

            _aiBase.Target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnTriggerExit2D(Object other)
        {
            if (other.name != "Low Collider") return;
            IsFighting = false;

            if (AudioSource.isPlaying)
            {
                StopSound();
            }

            _aiBase.Target = _aiBase.DefaultPosition;
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

            Animator.SetTrigger("Hit");

            AudioSource.PlayOneShot(HitSound);
        }

        public void Dead()
        {
            IsActive = false;

            Rigidbody2D.isKinematic = true;
            BoxCollider2D.enabled = false;
            SightRadius.gameObject.SetActive(false);
            _aiBase.enabled = false;
            Animator.SetTrigger("Dead");

            AudioSource.PlayOneShot(DeathSound);

            IsFighting = false;

            PlayerEvents.GiveExp(EXP);

            Invoke(("Deactivate"), 1f);
        }

        private void Deactivate()
        {

            SpriteRenderer.gameObject.SetActive(false);
            Animator.enabled = false;
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

            _aiBase.enabled = IsActive;
            SpriteRenderer.gameObject.SetActive(IsActive);
            Rigidbody2D.bodyType = IsActive ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            BoxCollider2D.enabled = IsActive;
            SightRadius.gameObject.SetActive(IsActive);
            Animator.enabled = IsActive;
            AudioSource.enabled = IsActive;
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
