using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace BOYAREngine
{
    public class Stats : MonoBehaviour
    {
        public PlayerData PlayerData = new PlayerData();
    }

    [System.Serializable]
    public class PlayerData
    {
        public int Health = 10;
    }
}



