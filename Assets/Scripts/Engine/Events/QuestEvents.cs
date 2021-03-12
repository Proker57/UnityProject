using BOYAREngine.Quests;

namespace BOYAREngine
{
    public class QuestEvents
    {
        public delegate void NewQuestDelegate(Task task);
        public static NewQuestDelegate NewQuest;
    }
}

