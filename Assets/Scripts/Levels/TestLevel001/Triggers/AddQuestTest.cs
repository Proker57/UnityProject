using BOYAREngine.Quests;
using UnityEngine;

namespace BOYAREngine
{
    public class AddQuestTest : MonoBehaviour
    {
        private bool _isUsed = false;
        private bool _isFinished = false;
        private string _id = "quest_TestLevel001_test_0";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name != "Low Collider") return;

            if (!_isUsed)
            {
                QuestEvents.NewQuest(new QuestTestFirst());
                _isUsed = true;
            }

            foreach (var task in QuestManager.Instance.Tasks.ToArray())
            {
                if (task.Id != _id) continue;
                if (task.IsFinished) task.Finish();
            }
        }
    }
}
