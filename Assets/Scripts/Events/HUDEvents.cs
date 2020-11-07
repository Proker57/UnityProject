using UnityEngine;

namespace BOYAREngine
{
    public class HUDEvents : MonoBehaviour
    {
        public delegate void DashCheckIsActiveDelegate(bool boolean);
        public static DashCheckIsActiveDelegate DashCheckIsActive;

        public delegate void JumpCheckIsActiveDelegate(bool boolean);
        public static JumpCheckIsActiveDelegate JumpCheckIsActive;
    }
}