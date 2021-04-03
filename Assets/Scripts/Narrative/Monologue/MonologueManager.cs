using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine.Narrative
{
    public class MonologueManager : MonoBehaviour
    {
        [SerializeField] private GameObject _narrativeObject;
        [SerializeField] private Text _text;

        [SerializeField] private int _index;
        private bool _isLoaded;
        private bool _isReady;

        public Note Note;

        private void OnAddMonologue(Note note)
        {
            Note = note;
            _isReady = true;
        }

        private void OnLoadedMonologue()
        {
            _isLoaded = true;
        }

        private void Update()
        {
            if (!_isLoaded || !_isReady) return;
            StartMonologue();
            _isReady = false;
        }

        public void StartMonologue()
        {
            _index = 0;
            _narrativeObject.SetActive(true);

            StartCoroutine(NextPage(Note.WaitTimer[_index]));
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
            _narrativeObject.SetActive(false);
        }

        private void OnEnable()
        {
            MonologueEvents.AddMonologue += OnAddMonologue;
            MonologueEvents.LoadedMonologue += OnLoadedMonologue;
        }

        private void OnDisable()
        {
            MonologueEvents.AddMonologue -= OnAddMonologue;
            MonologueEvents.LoadedMonologue -= OnLoadedMonologue;
        }
    }
}
