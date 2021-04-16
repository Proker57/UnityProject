using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BOYAREngine.Narrative
{
    public class MonologueManager : MonoBehaviour
    {
        [SerializeField] private GameObject _narrativeObject;
        [SerializeField] private Text _text;

        public UnityEvent OnFinishEvent;

        [SerializeField] private int _index;
        private bool _isLoaded;
        private bool _isReady;

        public Note Note;

        private void Update()
        {
            if (Note == null) return;
            _isLoaded = Note.IsLoaded;
            if (!_isLoaded || !_isReady) return;
            StartCoroutine(NextPage(Note.WaitTimer[_index]));
            _isReady = false;
        }

        public void StartMonologue(Note note)
        {
            Note = note;
            _isReady = true;

            _index = 0;
            _narrativeObject.SetActive(true);
        }

        private IEnumerator NextPage(float waitTime)
        {
            _text.text = Note.Text[_index];
            _index++;
            yield return new WaitForSeconds(waitTime);

            if (_index < Note.Count)
            {
                StartCoroutine(NextPage(Note.WaitTimer[_index]));
            }
            else
            {
                FinishMonologue();
            }
        }

        private void FinishMonologue()
        {
            Note = null;
            _text.text = null;
            _isLoaded = false;
            _isReady = false;
            _index = 0;
            StopAllCoroutines();
            OnFinishEvent?.Invoke();
            _narrativeObject.SetActive(false);
        }

        private void OnEnable()
        {
            MonologueEvents.Stop += FinishMonologue;
        }

        private void OnDisable()
        {
            MonologueEvents.Stop -= FinishMonologue;
        }
    }
}
