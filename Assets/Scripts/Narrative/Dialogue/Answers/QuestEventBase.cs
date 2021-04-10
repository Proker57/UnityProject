using UnityEngine;

namespace BOYAREngine.Narrative
{
    public class QuestEventBase : MonoBehaviour
    {
        internal DialogueManager _dialogueManager;

        internal virtual void Awake()
        {
            _dialogueManager = DialogueManager.Instance;
        }

        internal virtual void AnswerAction(int index, int questionNumber, string dialogueId) { }

        public void OnEnable()
        {
            _dialogueManager.ChooseEvent += AnswerAction;
        }

        public void OnDisable()
        {
            _dialogueManager.ChooseEvent -= AnswerAction;
        }
    }

}
