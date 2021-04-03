using UnityEngine;

namespace BOYAREngine.Narrative
{
    public class Monologue : MonoBehaviour
    {
        public string Id;
        public int Count;
        public bool IsOnce;
        private bool _isReady = true;

        private void OnTriggerEnter2D(Object other)
        {
            if (other.name != "Low Collider") return;

            if (IsOnce)
            {
                if (_isReady) MonologueEvents.AddMonologue?.Invoke(new Note(Id, Count));
                _isReady = false;
            }
            else
            {
                MonologueEvents.AddMonologue?.Invoke(new Note(Id, Count));
            }
        }
    }
}

