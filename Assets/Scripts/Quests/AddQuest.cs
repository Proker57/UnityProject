using System;
using BOYAREngine.Quests;
using UnityEngine;

namespace BOYAREngine
{
    public class AddQuest : MonoBehaviour, ISaveable
    {
        private const string Namespace = "BOYAREngine.Quests.";

        public string QuestName = "QuestName";

        private string _id;
        private string _activatorType;
        private bool _isUsed;

        private void Awake()
        {
            _id = QuestName;
            _activatorType = string.Concat(Namespace + _id);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name != "Low Collider") return;

            if (!_isUsed)
            {
                QuestEvents.NewQuest((Task)Activator.CreateInstance(
                    Type.GetType(_activatorType) ?? throw new InvalidOperationException()));
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
