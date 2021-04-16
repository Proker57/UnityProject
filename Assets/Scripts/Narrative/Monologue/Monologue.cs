using UnityEngine;

namespace BOYAREngine.Narrative
{
    public class Monologue : MonoBehaviour
    {
        public MonologueManager MonologueManager;
        public bool IsPlayerMonologue;
        [Space]
        public string Id;
        [SerializeField] private bool _isOnce;
        [SerializeField] private bool _canBeInteruppted;

        [System.Serializable]
        struct Cooldown
        {
            public bool HasCooldown;
            public float WaitTime;
            [HideInInspector] public bool IsReady;
        }

        [System.Serializable]
        struct RichText
        {
            public bool IsRichText;
            public float WaitTime;
        }

        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private RichText _richText;

        private bool _isOncePlayed = true;

        private void Start()
        {
            if (IsPlayerMonologue)
            {
                MonologueManager = GameController.Instance.Player.MonologueManager;
            }

            _cooldown.IsReady = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name != "Low Collider") return;

            Init();
        }

        private void Init()
        {
            if (_isOnce)
            {
                if (_isOncePlayed)
                {
                    StartMonologue();
                }

                _isOncePlayed = false;
            }
            else
            {
                StartMonologue();
            }
        }

        private void StartMonologue()
        {
            if (_canBeInteruppted || IsPlayerMonologue)
            {
                MonologueEvents.Stop?.Invoke();
            }

            if (_cooldown.HasCooldown)
            {
                if (_cooldown.IsReady)
                {
                    MonologueManager.StartMonologue(new Note(Id));
                    if (_richText.IsRichText)
                    {
                        MonologueManager.Note.WaitMultiplier = _richText.WaitTime;
                    }
                    Invoke("WaitForReadyCoroutine", _cooldown.WaitTime);
                    _cooldown.IsReady = false;
                }
            }
            else
            {
                MonologueManager.StartMonologue(new Note(Id));
            }
        }

        private void WaitForReadyCoroutine()
        {
            _cooldown.IsReady = true;
        }
    }
}

