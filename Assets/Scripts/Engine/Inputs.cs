using UnityEngine;

namespace BOYAREngine
{
    public class Inputs : MonoBehaviour
    {
        public static Inputs Instance;

        public PlayerInput Input;
        public UnityEngine.InputSystem.PlayerInput PlayerInput;

        private void Awake()
        {
            Instance = this;

            Input = new PlayerInput();
            PlayerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();

            Input.Dialogue.Disable();
        }

        private void OnEnable()
        {
            Input.Enable();
        }

        private void OnDisable()
        {
            Input.Disable();
        }
    }
}