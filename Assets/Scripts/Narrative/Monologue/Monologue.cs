using UnityEngine;

namespace BOYAREngine.Narrative
{
    public class Monologue : MonoBehaviour
    {
        public MonologueManager MonologueManager;
        public bool IsPlayerMonologue;
        [Space]
        public string Id;
        public int Count;
        public bool IsOnce;

        private bool _isOnce = true;

        private void Awake()
        {
            if (IsPlayerMonologue)
            {
                MonologueManager = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MonologueManager;
            }
        }

        private void OnTriggerEnter2D(Object other)
        {
            if (other.name != "Low Collider") return;

            if (IsOnce)
            {
                if (_isOnce)
                {
                    StartMonologue();
                }

                _isOnce = false;
            }
            else
            {
                StartMonologue();
            }
        }

        private void StartMonologue()
        {
            MonologueManager.StartMonologue(new Note(Id, Count));
        }
    }
}

