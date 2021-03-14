using UnityEngine;

namespace BOYAREngine.Quests
{
    [System.Serializable]
    public class Task
    {
        internal const string StringTableCollectionName = "Quests";

        [SerializeField] internal string Id;
        [SerializeField] internal string Name;
        [SerializeField] internal string Description;
        [SerializeField] internal bool IsFinished;

        internal virtual void Finish() { }
        internal virtual void LoadStrings() { }
    }
}
