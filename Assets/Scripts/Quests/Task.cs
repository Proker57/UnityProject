using UnityEngine;

namespace BOYAREngine.Quests
{
    [System.Serializable]
    public class Task
    {
        internal const string StringTableCollectionName = "Quests";

        [SerializeField] internal string Name;
        [SerializeField] internal string Description;
        [SerializeField] internal bool IsFinished;

        internal virtual void LoadStrings() { }
    }
}
