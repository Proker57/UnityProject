using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace BOYAREngine
{
    public class Stats : MonoBehaviour
    {
        public PlayerData PlayerData = new PlayerData();

        private void SaveStats()
        {
            PlayerData.XPosition = transform.position.x;
            PlayerData.YPosition = transform.position.y;
        }

        private void LoadStats()
        {
            var position = new Vector2(PlayerData.XPosition, PlayerData.YPosition);
            transform.position = position;
        }

        private void OnEnable()
        {
            Events.Save += SaveStats;
            Events.Load += LoadStats;
        }

        private void OnDisable()
        {
            Events.Save -= SaveStats;
            Events.Load -= LoadStats;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public int Health = 10;
        public float XPosition;
        public float YPosition;
    }
}



