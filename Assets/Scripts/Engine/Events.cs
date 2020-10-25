using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BOYAREngine
{
    public class Events
    {
        public delegate void SaveDelegate();
        public static SaveDelegate Save;

        public delegate void LoadDelegate();
        public static LoadDelegate Load;

        public delegate void DashDelegate(float time);
        public static DashDelegate Dash;
        public delegate void DashReadyDelegate();
        public static DashReadyDelegate DashReady;
    }
}

