namespace BOYAREngine
{
    public class InputToggles
    {
        public static void DialogueInputs(bool isOn)
        {
            Inputs.Instance.PlayerInput.SwitchCurrentActionMap(isOn ? "Dialogue" : "PlayerInGame");
        }

        public static void Inventory(bool isOn)
        {
            Inputs.Instance.PlayerInput.SwitchCurrentActionMap(isOn ? "Inventory" : "PlayerInGame");
        }

        public static void Game()
        {
//            Inputs.Instance.Input.PlayerInGame.Enable();
//            Inputs.Instance.Input.Global.Enable();
//            Inputs.Instance.Input.HUD.Disable();
//            Inputs.Instance.Input.Dialogue.Disable();
        }

        public static void Pause()
        {
//            Inputs.Instance.Input.PlayerInGame.Disable();
//            Inputs.Instance.Input.Global.Enable();
//            Inputs.Instance.Input.HUD.Disable();
//            Inputs.Instance.Input.Dialogue.Disable();
        }
    }
}

