using BOYAREngine.Quests;
using UnityEngine;

namespace BOYAREngine
{
    public class AddQuestTest : MonoBehaviour
    {
        public bool IsUsed = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name != "Low Collider") return;

            if (IsUsed) return;
            QuestEvents.NewQuest(new QuestTestFirst());
            IsUsed = true;
        }
    }
}
