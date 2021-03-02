using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private CircleCollider2D _circleCollider2D;

    public AudioClip HitSound;
    public AudioClip IdleSound;

    public AudioSource AudioSource;

    public int AttackDamage = 1;

    public float Speed = 400;
    public float SightRadius = 3;

    public bool IsFighting = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name != "Low Collider") return;

        IsFighting = true;

        if (!AudioSource.isPlaying)
        {
            PlaySound(IdleSound);
        }
        
        LookAtPlayer(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "Low Collider") return;

        IsFighting = false;

        if (AudioSource.isPlaying)
        {
            StopSound();
        }
    }

    public void LookAtPlayer(Collider2D collision)
    {
        if (transform.position.x > collision.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }

    private void StopSound()
    {
        AudioSource.Stop();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, SightRadius);
    }
#endif
}
