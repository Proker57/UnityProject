using UnityEngine;

namespace BOYAREngine.Narrative
{
    public class ButtonEvent : MonoBehaviour
    {
        public void AnswerEvent(int index)
        {
            DialogueManager.Instance.ChooseEvent?.Invoke(index, DialogueManager.Instance.QuestionNumber, DialogueManager.Instance.DialogueId);
        }
    }
}

