using System;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance = null;
        public bool IsDialogueStarted;
        public bool IsQuestionNode = false;

        public delegate void ChooseEventDelegate(int answerIndex, int questionNumber);
        public ChooseEventDelegate ChooseEvent;

        public int QuestionNumber = 0;

        [SerializeField] private Text _name;
        [SerializeField] private Text _narrative;
        [SerializeField] private Text _answer1;
        [SerializeField] private Text _answer2;
        [SerializeField] private Text _answer3;
        [SerializeField] private Button _nextButton;
        [SerializeField] private GameObject _dialogueWindow;
        [SerializeField] private GameObject _answerWindow;
        private List<DialogueNode> _dialogueNodes;

        [Header("Cinemachine")]
        public CinemachineVirtualCamera Camera;
        public float ZoomInSize = 2.3f;

        private int _pageIndex = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            _dialogueNodes = new List<DialogueNode>();
        }

        public void StartDialogue(List<DialogueNode> listNodes)
        {
            IsQuestionNode = false;
            _dialogueWindow.SetActive(true);
            _nextButton.gameObject.SetActive(true);
            _answerWindow.SetActive(false);
            IsDialogueStarted = true;

            CameraZoom(true);

            _dialogueNodes = listNodes;

            InputToggles.DialogueInputs(true);

            SetStrings();
        }

        public void NextNode()
        {
            _pageIndex++;

            IsQuestionNode = false;

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

        private void ChooseAnswer(int answerIndex, int questionNumber)
        {
            NextNode();
        }

        public void FinishDialogue()
        {
            _pageIndex = 0;
            QuestionNumber = 0;
            IsDialogueStarted = false;
            _dialogueWindow.SetActive(false);

            CameraZoom(false);

            InputToggles.DialogueInputs(false);
        }

        private void QuestionNode()
        {
            if (!_dialogueNodes[_pageIndex].IsQuestion) return;
            
            IsQuestionNode = true;

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

            QuestionNumber++;
        }

        private void SetStrings()
        {
            _name.text = _dialogueNodes[_pageIndex].Name;
            _narrative.text = _dialogueNodes[_pageIndex].Narrative;
        }

        private void Next_pressed()
        {
            if (!IsQuestionNode) NextNode();
        }

        private void First_pressed()
        {
            if (IsQuestionNode) ChooseEvent(1, QuestionNumber);

        }

        private void Second_pressed()
        {
            if (IsQuestionNode) ChooseEvent(2, QuestionNumber);
        }

        private void Third_pressed()
        {
            if (IsQuestionNode) ChooseEvent(3, QuestionNumber);
        }

        private void CameraZoom(bool zoomIn)
        {
            Camera = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();

            Camera.m_Lens.OrthographicSize = zoomIn ? ZoomInSize : 3f;
        }

        private void OnEnable()
        {
            Inputs.Instance.Input.Dialogue.Next.performed += _ => Next_pressed();
            Inputs.Instance.Input.Dialogue.First.performed += _ => First_pressed();
            Inputs.Instance.Input.Dialogue.Second.performed += _ => Second_pressed();
            Inputs.Instance.Input.Dialogue.Third.performed += _ => Third_pressed();

            ChooseEvent += ChooseAnswer;
        }

        private void OnDisable()
        {
            Inputs.Instance.Input.Dialogue.Next.performed -= _ => Next_pressed();
            Inputs.Instance.Input.Dialogue.First.performed -= _ => First_pressed();
            Inputs.Instance.Input.Dialogue.Second.performed -= _ => Second_pressed();
            Inputs.Instance.Input.Dialogue.Third.performed -= _ => Third_pressed();

            ChooseEvent -= ChooseAnswer;
        }
    }
}
