using UnityEngine;

namespace BOYAREngine.Enemies.AI
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(AIBase))]
    public class AIBaseActions : MonoBehaviour
    {
        [Header("Attack")]
        [SerializeField] private float attackRadius = 1f;
        [SerializeField] private LayerMask _playerLayer;

        [Header("Jump forces")]
        public float XForce = 100f;
        public float YForce = 100f;

        private Enemy _main;
        private AIBase _aiBase;

        [SerializeField] private Tweeks _tweeks;

        private void Awake()
        {
            _main = GetComponent<Enemy>();
            _aiBase = GetComponent<AIBase>();
        }

        public void Attack()
        {
            _main.Animator.SetTrigger("BasicAttack");

            var hit = Physics2D.OverlapCircleAll(_main._aiBase.AttackPoint.position, attackRadius, _playerLayer);
            if (hit != null)
            {
                PlayerEvents.Damage(_main.AttackDamage);
            }
        }

        public void JumpBack()
        {
            // TODO JumpBack Animation
            //_main.Animator.SetTrigger("BasicAttack");

            _aiBase.CanFollow = false;
            _aiBase.CanFlip = false;

            _aiBase.Jump(-_aiBase.FaceDirection() * XForce, YForce);

            Invoke("Recover", _tweeks.JumpBackRecoverTime);
        }

        public void GetHit()
        {
            _aiBase.CanFollow = false;
            _aiBase.CanFlip = false;

            _aiBase.Jump(-_aiBase.FaceDirection() * _tweeks.HitXForce, _tweeks.HitYForce);

            Invoke("Recover", _tweeks.HitRecoverTime);
        }

        public void Jump(float xForce, float yForce)
        {
            _aiBase.Jump(xForce, yForce);
        }

        public void Dash()
        {
            _aiBase.IsLimitedVelocity = false;
            var force = new Vector2(_aiBase.FaceDirection() * _tweeks.DashXForce, _tweeks.DashYForce);
            _main.Rigidbody2D.AddForce(force, ForceMode2D.Impulse);

            Invoke("LimitVelocityRecoverTime", _tweeks.DashLimitVelocityRecoverTime);
            
        }

        public void StopMoving()
        {
            _main.MaxSpeed = 0.0f;

            Invoke("MaxSpeedRecover", _tweeks.WaitTime);
        }


















        private void LimitVelocityRecoverTime()
        {
            _aiBase.IsLimitedVelocity = true;
        }

        private void Recover()
        {
            _aiBase.CanFollow = true;
            _aiBase.CanFlip = true;
        }

        private void MaxSpeedRecover()
        {
            _main.MaxSpeed = _main.MaxSpeedBase;
        }
    }

    [System.Serializable]
    public class Tweeks
    {
        [Header("Jump Back")]
        public float JumpBackRecoverTime = 1f;

        [Header("Hit")]
        public float HitXForce = 80f;
        public float HitYForce = 70f;
        public float HitRecoverTime = 1f;

        [Header("Dash")]
        public float DashXForce = 50f;
        public float DashYForce = 10f;
        public float DashLimitVelocityRecoverTime = 0.4f;

        [Header("Stop Movement")]
        public float WaitTime = 1f;
    }
}
