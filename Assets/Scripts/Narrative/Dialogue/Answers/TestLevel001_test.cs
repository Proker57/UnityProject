using UnityEngine;

namespace BOYAREngine.Narrative
{
    public class TestLevel001_test : QuestEventBase
    {
        private Player _player;

        internal override void Awake()
        {
            base.Awake();
            _player = GameController.Instance.Player;
        }

        internal override void AnswerAction(int index, int questionNumber, string dialogueId)
        {
            if (_dialogueManager.DialogueId != GetType().Name) return;
            switch (questionNumber)
            {
                case 1:
                    switch (index)
                    {
                        case 1:
                            PlayerEvents.GiveExp(100);
                            break;
                        case 2:
                            PlayerEvents.GiveExp(200);
                            break;
                        case 3:
                            PlayerEvents.GiveExp(_player.Stats.MaxExp - _player.Stats.Exp);
                            break;
                    }
                    break;
                case 2:
                    if (index == 2) _dialogueManager.FinishDialogue();
                    break;
                case 3:
                    if (index == 1) SceneLoader.SwitchScene("TestLevel002");
                    break;
            }
        }
    }
}

