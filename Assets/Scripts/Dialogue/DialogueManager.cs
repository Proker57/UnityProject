using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class DialogueManager : MonoBehaviour
    {
        public bool IsDialogueStarted;
        public delegate void ChooseEventDelegate(int index);
        public ChooseEventDelegate ChooseEvent;

        [SerializeField] private Text _name;
        [SerializeField] private Text _narrative;
        [SerializeField] private Text _answer1;
        [SerializeField] private Text _answer2;
        [SerializeField] private Text _answer3;
        [SerializeField] private Button _nextButton;
        [SerializeField] private GameObject _dialogueWindow;
        [SerializeField] private GameObject _answerWindow;
        private List<DialogueNode> _dialogueNodes;

        private int _pageIndex = 0;
        private int _answerIndex;

        private void Awake()
        {
            _dialogueNodes = new List<DialogueNode>();
        }

        public void StartDialogue(List<DialogueNode> listNodes)
        {
            _dialogueWindow.SetActive(true);
            IsDialogueStarted = true;

            _dialogueNodes = listNodes;

            SetStrings();
        }

        public void NextNode()
        {
            _pageIndex++;

            _answerWindow.SetActive(false);
            _nextButton.gameObject.SetActive(true);

            if (_pageIndex < _dialogueNodes.Count)
            {
                SetStrings();

                QuestionNode();
            }
            else
            {
                FinishDialogue();
            }
        }


        private void ChooseAnswer(int index)
        {
            _answerIndex = index;

            NextNode();
        }

        public void FinishDialogue()
        {
            _pageIndex = 0;
            _answerIndex = 0;
            IsDialogueStarted = false;
            _dialogueWindow.SetActive(false);
        }

        private void QuestionNode()
        {
            if (!_dialogueNodes[_pageIndex].IsQuestion) return;

            _nextButton.gameObject.SetActive(false);
            _answerWindow.SetActive(true);

            switch (_dialogueNodes[_pageIndex].AnswerNode.AnswerCount)
            {
                case 1:
                    _answer1.gameObject.SetActive(true);
                    _answer2.gameObject.SetActive(false);
                    _answer3.gameObject.SetActive(false);

                    _answer1.text = _dialogueNodes[_pageIndex].AnswerNode.Answer1;
                    break;
                case 2:
                    _answer1.gameObject.SetActive(true);
                    _answer2.gameObject.SetActive(true);
                    _answer3.gameObject.SetActive(false);

                    _answer1.text = _dialogueNodes[_pageIndex].AnswerNode.Answer1;
                    _answer2.text = _dialogueNodes[_pageIndex].AnswerNode.Answer2;
                    break;
                case 3:
                    _answer1.gameObject.SetActive(true);
                    _answer2.gameObject.SetActive(true);
                    _answer3.gameObject.SetActive(true);

                    _answer1.text = _dialogueNodes[_pageIndex].AnswerNode.Answer1;
                    _answer2.text = _dialogueNodes[_pageIndex].AnswerNode.Answer2;
                    _answer3.text = _dialogueNodes[_pageIndex].AnswerNode.Answer3;
                    break;
                default:
                    _answer1.gameObject.SetActive(false);
                    _answer2.gameObject.SetActive(false);
                    _answer3.gameObject.SetActive(false);
                    break;
            }
        }

        private void SetStrings()
        {
            _name.text = _dialogueNodes[_pageIndex].Name;
            _narrative.text = _dialogueNodes[_pageIndex].Narrative;
        }

        private void OnEnable()
        {
            ChooseEvent += ChooseAnswer;
        }

        private void OnDisable()
        {
            ChooseEvent -= ChooseAnswer;
        }
    }
}
