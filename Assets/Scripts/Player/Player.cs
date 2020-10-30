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

        private GameController _gameController;

        private void Awake()
        {
            _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

            Input = Inputs.Input;
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Jump = GetComponent<Jump>();
            Crouch = GetComponent<Crouch>();
            LowCollider = GetComponentInChildren<BoxCollider2D>();
            HighCollider = GetComponent<CapsuleCollider2D>();
            Animator = GetComponent<Animator>();
            Dash = GetComponent<Dash>();
            Movement = GetComponent<Movement>();
            Stats = GetComponent<Stats>();

        }

        private void OnEnable()
        {
            //_gameController.IsPlayerActive = true;
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
        }

        private void OnDisable()
        {
            //_gameController.IsPlayerActive = false;
            Events.PlayerOnScene(false);

            Input.Disable();
            Jump.enabled = false;
            Crouch.enabled = false;
            LowCollider.enabled = false;
            HighCollider.enabled = false;
            Animator.enabled = false;
            Dash.enabled = false;
            Movement.enabled = false;
            Stats.enabled = false;
        }
    }
}
