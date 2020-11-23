using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _narrative;
        [SerializeField] private Text _answer1;
        [SerializeField] private Text _answer2;
        [SerializeField] private Text _answer3;
        [SerializeField] private Button _nextButton;
        [SerializeField] private GameObject _dialogueWindow;
        [SerializeField] private GameObject _answerWindow;
        private List<DialogueNode> _dialogueNodes;
        private List<AnswerNode> _answerNodes;

        private int _pageIndex = 0;
        private int _answerIndex = -1;

        private void Awake()
        {
            _dialogueNodes = new List<DialogueNode>();
            _answerNodes = new List<AnswerNode>();
        }

        public void StartDialogue(List<DialogueNode> listNodes)
        {
            _dialogueWindow.SetActive(true);

            _dialogueNodes = listNodes;

            SetStrings();
        }

        public void StartDialogue(List<DialogueNode> listNodes, List<AnswerNode> answerNodes)
        {
            _dialogueWindow.SetActive(true);

            _dialogueNodes = listNodes;
            _answerNodes = answerNodes;

            SetStrings();
        }

        public void NextNode()
        {
            _pageIndex++;

            if (_pageIndex < _dialogueNodes.Count)
            {
                SetStrings();

                if (_dialogueNodes[_pageIndex].IsQuestion)
                {
                    _answerWindow.SetActive(true);

                    ++_answerIndex;

                    switch (_dialogueNodes[_pageIndex].Answers[_answerIndex].AnswerCount)
                    {
                        case 1:
                            _answer1.gameObject.SetActive(true);
                            _answer2.gameObject.SetActive(false);
                            _answer3.gameObject.SetActive(false);

                            _answer1.text = _answerNodes[_answerIndex].Answer1;
                            break;
                        case 2:
                            _answer1.gameObject.SetActive(true);
                            _answer2.gameObject.SetActive(true);
                            _answer3.gameObject.SetActive(false);

                            _answer1.text = _answerNodes[_answerIndex].Answer1;
                            _answer2.text = _answerNodes[_answerIndex].Answer2;
                            break;
                        case 3:
                            _answer1.gameObject.SetActive(true);
                            _answer2.gameObject.SetActive(true);
                            _answer3.gameObject.SetActive(true);

                            _answer1.text = _answerNodes[_answerIndex].Answer1;
                            _answer2.text = _answerNodes[_answerIndex].Answer2;
                            _answer3.text = _answerNodes[_answerIndex].Answer3;
                            break;
                        default:
                            _answer1.gameObject.SetActive(false);
                            _answer2.gameObject.SetActive(false);
                            _answer3.gameObject.SetActive(false);
                            break;
                    }
                }
                else
                {
                    _answerWindow.SetActive(false);
                }
            }
            else
            {
                FinishDialogue();
            }
        }

        private void SetStrings()
        {
            _name.text = _dialogueNodes[_pageIndex].Name;
            _narrative.text = _dialogueNodes[_pageIndex].Narrative;
        }

        public void FinishDialogue()
        {
            _pageIndex = 0;
            _answerIndex = 0;
            _dialogueWindow.SetActive(false);
        }

        public void SetAnswerIndex(int answerIndex)
        {
            _answerIndex = answerIndex;
        }
    }
}
