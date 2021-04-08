using UnityEngine;
using UnityEngine.Events;

namespace BOYAREngine
{
    [System.Serializable]
    public class AnswerNode
    {
        public string[] Answers = new string[3];

        public AnswerNode(string answer1, string answer2, string answer3)
        {
            Answers[0] = answer1;
            Answers[1] = answer2;
            Answers[2] = answer3;
        }

        public void LoadStrings(string answer)
        {
            Answers[0] = answer;
            Answers[1] = answer;
            Answers[2] = answer;
        }
    }
}
