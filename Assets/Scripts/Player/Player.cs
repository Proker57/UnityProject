using UnityEngine;

namespace BOYAREngine
{
    public class Player : MonoBehaviour
    {
        public PlayerInput Input;
        public Rigidbody2D Rigidbody2D;
        public Jump Jump;
        public Crouch Crouch;
        public BoxCollider2D LowCollider;
        public CapsuleCollider2D HighCollider;
        public Animator Animator;
        public Dash Dash;
        public Movement Movement;
        public Stats Stats;
        public WeaponManager WeaponManager;
        public ItemManager ItemManager;
        public BasicAttack BasicAttack;

        private void Awake()
        {
            Input = new PlayerInput();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Jump = GetComponent<Jump>();
            Crouch = GetComponent<Crouch>();
            LowCollider = GetComponentInChildren<BoxCollider2D>();
            HighCollider = GetComponent<CapsuleCollider2D>();
            Animator = GetComponent<Animator>();
            Dash = GetComponent<Dash>();
            Movement = GetComponent<Movement>();
            Stats = GetComponent<Stats>();
            WeaponManager = GetComponent<WeaponManager>();
            ItemManager = GetComponent<ItemManager>();
            BasicAttack = GetComponent<BasicAttack>();

            GameController.HasPlayer = true;
        }

        private void OnEnable()
        {
            Events.PlayerOnScene(true);

            Input.Enable();
            Jump.enabled = true;
            Crouch.enabled = true;
            LowCollider.enabled = true;
            HighCollider.enabled = true;
            Animator.enabled = true;
            Dash.enabled = true;
            Movement.enabled = true;
            Stats.enabled = true;
            WeaponManager.enabled = true;
            ItemManager.enabled = true;
            BasicAttack.enabled = true;
        }

        private void OnDisable()
        {
            Events.PlayerOnScene?.Invoke(false);

            Input.Disable();
            Jump.enabled = false;
            Crouch.enabled = false;
            LowCollider.enabled = false;
            HighCollider.enabled = false;
            Animator.enabled = false;
            Dash.enabled = false;
            Movement.enabled = false;
            Stats.enabled = false;
            WeaponManager.enabled = false;
            ItemManager.enabled = false;
            BasicAttack.enabled = false;
        }

        private void OnDestroy()
        {
            GameController.HasPlayer = false;
        }
    }
}
