namespace BOYAREngine
{
    public class AnswerNode
    {
        public string Answer1 = null;
        public string Answer2 = null;
        public string Answer3 = null;
        public int AnswerCount;

        public AnswerNode(string answer1)
        {
            Answer1 = answer1;

            AnswerCount = 1;
        }

        public AnswerNode(string answer1, string answer2)
        {
            Answer1 = answer1;
            Answer2 = answer2;

            AnswerCount = 2;
        }

        public AnswerNode(string answer1, string answer2, string answer3)
        {
            Answer1 = answer1;
            Answer2 = answer2;
            Answer3 = answer3;

            AnswerCount = 3;
        }
    }
}
