using System.Collections.Generic;

namespace BOYAREngine
{
    public class DialogueNode
    {
        public string Name;
        public string Narrative;
        public bool IsQuestion;

        public List<AnswerNode> Answers = new List<AnswerNode>();

        public DialogueNode(string name, string narrative)
        {
            Name = name;
            Narrative = narrative;
            IsQuestion = false;
        }

        public DialogueNode(string name, string narrative, List<AnswerNode> answers)
        {
            Name = name;
            Narrative = narrative;
            IsQuestion = true;
            Answers = answers;
        }
    }
}
