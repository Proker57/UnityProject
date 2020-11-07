using UnityEngine;

namespace BOYAREngine
{
    public class Inputs : MonoBehaviour
    {
        public PlayerInput Input;

        private void Awake()
        {
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