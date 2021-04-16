using BOYAREngine.Narrative;
using UnityEngine;

namespace BOYAREngine
{
    public class MonologueEvents : MonoBehaviour
    {
        public delegate void AddMonologueDelegate(Note note);
        public static AddMonologueDelegate AddMonologue;

        public delegate void LoadedMonologueDelegate();
        public static LoadedMonologueDelegate LoadedMonologue;

        public delegate void StopDelegate();
        public static StopDelegate Stop;
    }
}

