namespace BOYAREngine
{
    [System.Serializable]
    public class DialogueNode
    {
        public string Name;
        public string Narrative;
        public bool IsQuestion;

        public AnswerNode AnswerNode;

        public DialogueNode(string name, string narrative)
        {
            Name = name;
            Narrative = narrative;
            IsQuestion = false;
        }

        public DialogueNode(string name, string narrative, AnswerNode answer)
        {
            Name = name;
            Narrative = narrative;
            IsQuestion = true;
            AnswerNode = answer;
        }

        public void LoadStrings(string name, string narrative)
        {
            Name = name;
            Narrative = narrative;
        }
    }
}
