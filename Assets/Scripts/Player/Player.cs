using BOYAREngine.Narrative;
using UnityEngine;

namespace BOYAREngine
{
    public class Player : MonoBehaviour
    {
        public BoxCollider2D LowCollider;
        public CapsuleCollider2D HighCollider;
        public ParticleSystem DustFx;
        public ParticleSystem WetFx;

        [HideInInspector] public PlayerInput Input;
        [HideInInspector] public Rigidbody2D Rigidbody2D;
        [HideInInspector] public Jump Jump;
        [HideInInspector] public Crouch Crouch;
        [HideInInspector] public Animator Animator;
        [HideInInspector] public Dash Dash;
        [HideInInspector] public Movement Movement;
        [HideInInspector] public Stats Stats;
        [HideInInspector] public WeaponManager WeaponManager;
        [HideInInspector] public Attack Attack;
        [HideInInspector] public ItemManager ItemManager;
        [HideInInspector] public MonologueManager MonologueManager;

        private void Awake()
        {
            Input = Inputs.Instance.Input;
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Jump = GetComponent<Jump>();
            Crouch = GetComponent<Crouch>();
            Animator = GetComponent<Animator>();
            Dash = GetComponent<Dash>();
            Movement = GetComponent<Movement>();
            Stats = GetComponent<Stats>();
            WeaponManager = GetComponent<WeaponManager>();
            Attack = GetComponent<Attack>();
            ItemManager = GetComponent<ItemManager>();
            MonologueManager = GetComponentInChildren<MonologueManager>();

            GameController.HasPlayer = true;
        }

        private void OnEnable()
        {
            Events.PlayerOnScene(true);
            GameController.Instance.PlayerInput.SwitchCurrentActionMap("PlayerInGame");

            LowCollider.enabled = true;
            HighCollider.enabled = true;

            Jump.enabled = true;
            Crouch.enabled = true;
            Animator.enabled = true;
            Dash.enabled = true;
            Movement.enabled = true;
            Stats.enabled = true;
            WeaponManager.enabled = true;
            Attack.enabled = true;
            ItemManager.enabled = true;
            MonologueManager.enabled = true;

            Input.HUD.Enable();
        }

        private void OnDisable()
        {
            Events.PlayerOnScene?.Invoke(false);

            LowCollider.enabled = false;
            HighCollider.enabled = false;

            Jump.enabled = false;
            Crouch.enabled = false;
            Animator.enabled = false;
            Dash.enabled = false;
            Movement.enabled = false;
            Stats.enabled = false;
            WeaponManager.enabled = false;
            Attack.enabled = false;
            ItemManager.enabled = false;
            MonologueManager.enabled = false;

            Input.PlayerInGame.Disable();
            Input.HUD.Disable();
        }

        private void OnDestroy()
        {
            GameController.HasPlayer = false;
        }
    }
}
