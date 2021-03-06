using UnityEngine;
using UnityEngine.Events;

namespace BOYAREngine.Enemies.AI
{
    [RequireComponent(typeof(Enemy))]
    public class AIBase : MonoBehaviour
    {
        [Header("Waypoints")]
        public Transform Target;
        public Transform DefaultPosition;

        [Header("Vars")]
        public float AddForce = 400f;
        public float JumpForce = 50f;
        [Space]
        public float NearRadius = .5f;
        public float SlowLerpDistance = .3f;
        public float Distance;

        [Header("AI Logic")]
        public bool CanFollow = true;
        public bool IsReturnable = true;
        public bool HasCatchedTarget;
        [Space]
        public UnityEvent CatchEvent;

        [Header("Jump")]
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _canJump = true;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _nextTimeJumpTimer;
        [SerializeField] private float _nextTimeJumpBase = 1f;

        [Header("Attack")]
        [SerializeField] private bool _canAttack;
        [SerializeField] private float _nextTimeAttackTimer;
        [SerializeField] private float _nextTimeAttackBase = 2f;

        private Enemy _main;

        private void Awake()
        {
            _main = GetComponent<Enemy>();
        }

        private void FixedUpdate()
        {
            if (_main.IsFighting)
            {
                if (CanFollow)
                {
                    StartFollow();
                }
            }
            else
            {
                if (IsReturnable)
                {
                    StartFollow();
                }
            }

            CheckForJump();
            
            FlipX();
        }

        private void StartFollow()
        {
            if (Target == null) Target = DefaultPosition;
            Distance = Vector2.Distance(Target.position, transform.position);
            HasCatchedTarget = Distance < NearRadius;

            if (!HasCatchedTarget)
            {
                var force = new Vector2(FaceDirection() * AddForce, transform.position.y);
                _main.Rigidbody2D.AddForce(force, ForceMode2D.Force);
                
                if (Distance <= NearRadius + SlowLerpDistance && Distance > NearRadius)
                {
                    _main.Rigidbody2D.velocity = new Vector2(Mathf.Lerp(_main.Rigidbody2D.velocity.x, 0, 2f), _main.Rigidbody2D.velocity.y);
                }
                else
                {
                    _main.Rigidbody2D.velocity = new Vector2(Mathf.Clamp(_main.Rigidbody2D.velocity.x, -_main.MaxSpeed, _main.MaxSpeed), _main.Rigidbody2D.velocity.y);
                }
            }
            else
            {
                if (_main.IsFighting)
                {
                    BasicAttack();
                }
            }
        }

        public void BasicAttack()
        {
            if (_nextTimeAttackTimer > 0)
            {
                _nextTimeAttackTimer -= Time.deltaTime;
                _canAttack = false;
            }
            else
            {
                _canAttack = true;
            }

            if (_canAttack)
            {
                CatchEvent.Invoke();
                _nextTimeAttackTimer = _nextTimeAttackBase;
            }
        }

        private void CheckForJump()
        {
            CheckForGround();

            if (_nextTimeJumpTimer > 0)
            {
                _nextTimeJumpTimer -= Time.deltaTime;
                _canJump = false;
            }
            else
            {
                _canJump = true;
            }

            var xDistance = Mathf.Abs(transform.position.x - Target.position.x);
            if (!HasCatchedTarget)
            {
                if (xDistance < 2f && VerticalRange() && _isGrounded || CheckForWall() && _isGrounded)
                {
                    if (_canJump)
                    {
                        Jump(JumpForce);
                    }
                }
            }
        }

        public void Jump(float jumpForce)
        {
            var force = new Vector2(transform.position.x * FaceDirection(), JumpForce);
            _main.Rigidbody2D.AddForce(force, ForceMode2D.Impulse);

            _nextTimeJumpTimer = _nextTimeJumpBase;
        }

        private void CheckForGround()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, _layerMask);
            _isGrounded = hit.collider != null;
        }

        private bool CheckForWall()
        {
            var hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f, _layerMask);
            var hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, _layerMask);

            return hitLeft.collider != null || hitRight.collider != null;
        }

        private int FaceDirection()
        {
            var direction = 0;

            if (Target.transform.position.x >= transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            return direction;
        }

        private bool VerticalRange()
        {
            return (transform.position.y + _main.BoxCollider2D.size.y / 2 - Target.position.y) < 0;
        }

        private void FlipX()
        {
            if (_main.Rigidbody2D.velocity.x > 0.01f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (_main.Rigidbody2D.velocity.x < -0.01f)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }
}
