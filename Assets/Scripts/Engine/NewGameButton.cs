using UnityEngine;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] private string _nextLevelName;

    public void ChangeScene()
    {
        SceneLoader.SwitchScene(_nextLevelName);
    }
}
