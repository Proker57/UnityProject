using UnityEngine;

namespace BOYAREngine.Narrative
{
    public class TestLevel001_test1 : QuestEventBase
    {
        internal override void AnswerAction(int index, int questionNumber, string dialogueId)
        {
            if (_dialogueManager.DialogueId != GetType().Name) return;
            switch (questionNumber)
            {
                case 1:
                    switch (index)
                    {
                        case 1:
                            Debug.Log("Sosi1");
                            break;
                        case 2:
                            Debug.Log("Sosi2");
                            break;
                        case 3:
                            Debug.Log("Sosi3");
                            break;
                    }
                    break;
            }
        }
    }
}

