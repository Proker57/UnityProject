using UnityEngine;
using UnityEngine.Events;

namespace BOYAREngine.Enemies.AI
{
    public class AIBase : MonoBehaviour
    {
        [Header("Init")]
        public Transform Target;
        public Transform DefaultPosition;
        [Space]
        public Transform AttackPoint;

        [Header("Movement")]
        public float MoveForce = 400f;
        public float JumpForce = 200f;
        public float SlowLerpDistance = .3f;
        public float SlowLerpTime = 1f;
        public bool IsLimitedVelocity = true;
        [Space]
        public float Distance;

        [Header("AI Logic")]
        public bool CanFollow = true;
        public bool IsReturnable = true;
        public bool HasCatchedTarget;

        [Header("Jump")]
        public bool IsGrounded;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private float _nextTimeJumpBase = 1f;
        private float _nextTimeJumpTimer;
        private bool _canJump = true;

        [Header("Melee")]
        public UnityEvent CatchEvent;
        public float NearRadius = .5f;
        [SerializeField] private float _nextCatchActionBase = 2f;
        private float _nextCatchActionTimer;
        private bool _canDoCatchAction;

        [Header("Range action")]
        public UnityEvent RangeEvent;
        [SerializeField] private float _nextRangeActionBase = 5f;
        private float _nextRangeActionTimer;
        private bool _canDoRangeAction;

        public bool CanFlip = true;

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
                    StartFight();
                }
            }
            else
            {
                if (IsReturnable)
                {
                    StartFight();
                }
            }

            CheckForJump();
            
            FlipX();
        }

        private void StartFight()
        {
            if (Target == null) Target = DefaultPosition;
            Distance = Vector2.Distance(Target.position, transform.position);
            HasCatchedTarget = Distance < NearRadius;

            if (!HasCatchedTarget)
            {
                FollowPlayer();
            }
            else
            {
                if (_main.IsFighting)
                {
                    Catch();
                }
            }
        }

        public void FollowPlayer()
        {
            RangeActionCountdown();
            if (_canDoRangeAction)
            {
                RangeEvent.Invoke();
                _nextRangeActionTimer = _nextRangeActionBase;
            }

            var force = new Vector2(FaceDirection() * MoveForce, transform.position.y);
            _main.Rigidbody2D.AddForce(force, ForceMode2D.Force);

            if (!IsLimitedVelocity) return;
            if (Distance <= NearRadius + SlowLerpDistance && Distance > NearRadius)
            {
                _main.Rigidbody2D.velocity = new Vector2(Mathf.Lerp(_main.Rigidbody2D.velocity.x, 0, SlowLerpTime), _main.Rigidbody2D.velocity.y);
            }
            else
            {
                _main.Rigidbody2D.velocity = new Vector2(Mathf.Clamp(_main.Rigidbody2D.velocity.x, -_main.MaxSpeed, _main.MaxSpeed), _main.Rigidbody2D.velocity.y);
            }
        }

        public void Catch()
        {
            CatchActionCountdown();
            if (!_canDoCatchAction) return;
            CatchEvent.Invoke();
            _nextCatchActionTimer = _nextCatchActionBase;
        }

        private void CheckForJump()
        {
            CheckForGround();

            JumpCountdown();

            var xDistance = Mathf.Abs(transform.position.x - Target.position.x);
            if (HasCatchedTarget) return;
            if ((!(xDistance < 2f) || !VerticalRange() || !IsGrounded) && (!CheckForWall() || !IsGrounded)) return; // if no walls and is not grounded
            if (_canJump) Jump(JumpForce);
        }

        public void Jump(float jumpForce)
        {
            var force = new Vector2(transform.position.x * FaceDirection(), JumpForce);
            _main.Rigidbody2D.AddForce(force, ForceMode2D.Impulse);

            _nextTimeJumpTimer = _nextTimeJumpBase;
        }

        public void Jump(float jumpDirection, float jumpForce)
        {
            var force = new Vector2(jumpDirection, JumpForce);
            _main.Rigidbody2D.AddForce(force, ForceMode2D.Impulse);

            _nextTimeJumpTimer = _nextTimeJumpBase;
        }

        private void CheckForGround()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, _layerMask);
            IsGrounded = hit.collider != null;
        }

        private bool CheckForWall()
        {
            var hitLeft = Physics2D.Raycast(transform.position, Vector2.left, .6f, _layerMask);
            var hitRight = Physics2D.Raycast(transform.position, Vector2.right, .6f, _layerMask);

            return hitLeft.collider != null || hitRight.collider != null;
        }

        public int FaceDirection()
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

        private void CatchActionCountdown()
        {
            if (_nextCatchActionTimer > 0)
            {
                _nextCatchActionTimer -= Time.deltaTime;
                _canDoCatchAction = false;
            }
            else
            {
                _canDoCatchAction = true;
            }
        }

        private void RangeActionCountdown()
        {
            if (_nextRangeActionTimer > 0)
            {
                _nextRangeActionTimer -= Time.deltaTime;
                _canDoRangeAction = false;
            }
            else
            {
                _canDoRangeAction = true;
            }
        }

        private void JumpCountdown()
        {
            if (_nextTimeJumpTimer > 0)
            {
                _nextTimeJumpTimer -= Time.deltaTime;
                _canJump = false;
            }
            else
            {
                _canJump = true;
            }
        }


        public void FlipX()
        {
            if (!CanFlip) return;
            if (_main.Rigidbody2D.velocity.x < -0.01f)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (_main.Rigidbody2D.velocity.x > 0.01f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
}
