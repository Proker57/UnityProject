using UnityEngine;

namespace BOYAREngine
{
    public class LoadLevelEvents : MonoBehaviour
    {
        public delegate void LevelLoadedDelegate(string levelName);
        public static LevelLoadedDelegate LevelLoaded;
    }
}

