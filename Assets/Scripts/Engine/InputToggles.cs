namespace BOYAREngine
{
    public class InputToggles
    {
        public static void DialogueInputs(bool isOn)
        {
            if (isOn)
            {
                Inputs.Instance.Input.PlayerInGame.Disable();
                Inputs.Instance.Input.Global.Disable();
                Inputs.Instance.Input.Dialogue.Enable();
            }
            else
            {
                Inputs.Instance.Input.PlayerInGame.Enable();
                Inputs.Instance.Input.Global.Enable();
                Inputs.Instance.Input.Dialogue.Disable();
            }
        }
    }
}
