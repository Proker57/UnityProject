using UnityEngine;

public class NewGameButton : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private string _nextLevelName;
#pragma warning restore 649

    public void ChangeScene()
    {
        SceneLoader.SwitchScene(_nextLevelName);
    }
}
