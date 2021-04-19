using UnityEngine;

namespace BOYAREngine.Narrative
{
    public class Jail_bar : QuestEventBase
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
                            Debug.Log("Yes");
                            WeaponEvents.WeaponPickUp(new JailBar());
                            gameObject.SetActive(false);
                            break;
                        case 2:
                            Debug.Log("No");
                            break;
                    }
                    break;
            }
        }
    }
}

