using UnityEngine;

namespace BOYAREngine
{
    public class InputToggles
    {
        public static void DialogueInputs(bool isOn)
        {
            if (isOn)
            {
//                Inputs.Instance.Input.PlayerInGame.Disable();
//                Inputs.Instance.Input.Global.Disable();
//                Inputs.Instance.Input.HUD.Disable();
//                Inputs.Instance.Input.Dialogue.Enable();

                //Inputs.Instance.PlayerInput.SwitchCurrentActionMap("Dialogue");

            }
            else
            {
//                Inputs.Instance.Input.PlayerInGame.Enable();
//                Inputs.Instance.Input.Global.Enable();
//                Inputs.Instance.Input.HUD.Enable();
//                Inputs.Instance.Input.Dialogue.Disable();

                //Inputs.Instance.PlayerInput.SwitchCurrentActionMap("PlayerInGame");
            }
        }

        public static void DisableAll()
        {
            Inputs.Instance.Input.PlayerInGame.Disable();
            Inputs.Instance.Input.Global.Disable();
            Inputs.Instance.Input.HUD.Disable();
            Inputs.Instance.Input.Dialogue.Disable();
        }

        public static void Game()
        {
            Inputs.Instance.Input.PlayerInGame.Enable();
            Inputs.Instance.Input.Global.Enable();
            Inputs.Instance.Input.HUD.Disable();
            Inputs.Instance.Input.Dialogue.Disable();
        }

        public static void Pause()
        {
            Inputs.Instance.Input.PlayerInGame.Disable();
            Inputs.Instance.Input.Global.Enable();
            Inputs.Instance.Input.HUD.Disable();
            Inputs.Instance.Input.Dialogue.Disable();
        }
    }
}

