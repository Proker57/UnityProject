using UnityEngine;
using UnityEngine.InputSystem;

namespace BOYAREngine
{
    public class ToggleQuestsPanel : MonoBehaviour
    {
        public GameObject Panel;
        [Space]
        [SerializeField] private InputAction _quests;
        [SerializeField] private InputActionAsset _controls;

        public bool IsActive;

        private void Start()
        {
            var iam = _controls.FindActionMap("PlayerInGame");
            _quests = iam.FindAction("Quest");
            _quests.performed += Quests_started;
        }

        public void TogglePanel()
        {
            IsActive = !IsActive;

            Panel.SetActive(IsActive);
        }

        public void EnterPointer()
        {
            Attack.Instance.IsAbleToAttack = false;
        }

        public void ExitPointer()
        {
            Attack.Instance.IsAbleToAttack = true;
        }

        public void Quests_started(InputAction.CallbackContext ctx)
        {
            TogglePanel();
        }
    }
}