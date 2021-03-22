using System;
using System.Text;
using BOYAREngine.Quests;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BOYAREngine
{
    public class AddQuest : MonoBehaviour, ISaveable
    {
        public string QuestName = "QuestName";

        private string _namespace = "BOYAREngine.Quests.";
        private string _id;
        private string activatorType;
        private bool _isUsed;

        private void Awake()
        {
            _id = "Quest_" + SceneManager.GetActiveScene().name + "_" + QuestName;
            activatorType = string.Concat(_namespace + _id);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name != "Low Collider") return;

            if (!_isUsed)
            {
                QuestEvents.NewQuest((Task)Activator.CreateInstance(
                    Type.GetType(activatorType) ?? throw new InvalidOperationException()));
                _isUsed = true;
            }

            foreach (var task in QuestManager.Instance.Tasks.ToArray())
            {
                if (task.Id != _id) continue;
                if (task.IsFinished) task.Finish();
            }
        }

        public object CaptureState()
        {
            return new AddQuestData
            {
                IsUsed = _isUsed
            };
        }

        public void RestoreState(object state)
        {
            var data = (AddQuestData) state;

            _isUsed = data.IsUsed;
        }
    }

    [System.Serializable]
    public class AddQuestData
    {
        public bool IsUsed;
    }
}
