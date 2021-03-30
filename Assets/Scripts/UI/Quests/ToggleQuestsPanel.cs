using UnityEngine;

namespace BOYAREngine
{
    public class ToggleQuestsPanel : MonoBehaviour
    {
        public GameObject Panel;

        public bool IsActive;

        public void TogglePanel()
        {
            IsActive = !IsActive;

            Panel.SetActive(IsActive);
        }

        public void EnterPointer()
        {
            WeaponManager.Instance.IsAbleToAttack = false;
        }

        public void ExitPointer()
        {
            WeaponManager.Instance.IsAbleToAttack = true;
        }

        private void Quests_started()
        {
            TogglePanel();
        }

        private void OnEnable()
        {
            Events.PlayerOnScene += AssignPlayer;
        }

        private void OnDisable()
        {
            Events.PlayerOnScene -= AssignPlayer;

            Inputs.Instance.Input.HUD.Quests.started -= _ => Quests_started();
        }

        private void AssignPlayer(bool isActive)
        {
            Inputs.Instance.Input.HUD.Quests.started += _ => Quests_started();
        }

    }
}