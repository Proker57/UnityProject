using UnityEngine;

namespace BOYAREngine
{
    public class Inputs : MonoBehaviour
    {
        public static Inputs Instance;

        public PlayerInput Input;

        private void Awake()
        {
            Instance = this;

            Input = new PlayerInput();
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