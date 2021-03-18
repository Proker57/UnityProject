using UnityEngine;

namespace BOYAREngine.UI
{
    public class UIToggle : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void CheckLevelForToggleUI(string levelName)
        {
            if (levelName.Equals("Main") || levelName.Equals("MainMenu"))
            {
                _canvas.enabled = false;
            }
            else
            {
                _canvas.enabled = true;
            }
        }

        private void OnEnable()
        {
            LoadLevelEvents.LevelLoaded += CheckLevelForToggleUI;
        }
    }
}

