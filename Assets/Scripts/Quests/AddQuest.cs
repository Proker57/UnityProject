using System;
using BOYAREngine.Quests;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BOYAREngine
{
    public class AddQuest : MonoBehaviour
    {
        public string QuestName = "QuestName";

        private string _id;
        private bool _isUsed;

        private void Awake()
        {
            _id = string.Concat("BOYAREngine.Quests.Quest_" + SceneManager.GetActiveScene().name + "_" + QuestName);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name != "Low Collider") return;

            if (!_isUsed)
            {
                QuestEvents.NewQuest((Task)Activator.CreateInstance(
                    Type.GetType(_id) ?? throw new InvalidOperationException()));
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
