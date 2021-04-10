using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;
        public bool IsDialogueStarted;
        public bool IsQuestionNode;

        public delegate void ChooseEventDelegate(int answerIndex, int questionNumber, string dialogId);
        public ChooseEventDelegate ChooseEvent;

        public int QuestionNumber = 0;

        [SerializeField] private Text _name;
        [SerializeField] private Text _narrative;
        [Space]
        [SerializeField] private Button[] _buttons;
        private Text[] _answers;

        [Space]
        [SerializeField] private Button _nextButton;
        [SerializeField] private GameObject _dialogueWindow;
        [SerializeField] private GameObject _answerWindow;
        [SerializeField] private List<DialogueNode> _dialogueNodes;

        [Header("Cinemachine")]
        public CinemachineVirtualCamera Camera;
        public float ZoomInSize = 2.3f;

        public string DialogueId;
        private int _pageIndex = 0;

        [Header("Active buttons")]
        [SerializeField] private GameObject _nextButtonGameObject;
        [SerializeField] private GameObject _firstAnswerGameObject;
        private EventSystem _eventSystem;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            _answers = new Text[_buttons.Length];
            for (var i = 0; i < _buttons.Length; i++)
            {
                _answers[i] = _buttons[i].transform.GetChild(0).GetComponent<Text>();
            }

            EventSystem.current.firstSelectedGameObject = _nextButtonGameObject;

            _dialogueNodes = new List<DialogueNode>();
        }

        public void StartDialogue(List<DialogueNode> listNodes, string dialogueId)
        {
            EnableEvents();

            EventSystem.current.SetSelectedGameObject(_nextButtonGameObject);

            _dialogueNodes = listNodes;
            DialogueId = dialogueId;
            _dialogueWindow.SetActive(true);
            IsQuestionNode = listNodes[0].IsQuestion;
            _nextButton.gameObject.SetActive(!IsQuestionNode);
            _answerWindow.SetActive(IsQuestionNode);
            IsDialogueStarted = true;

            CameraZoom(true);

            InputToggles.DialogueInputs(true);

            SetStrings();
            QuestionNode();
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

        private void ChooseAnswer(int answerIndex, int questionNumber, string dialogueId)
        {
            NextNode();
        }

        public void FinishDialogue()
        {
            _pageIndex = 0;
            QuestionNumber = 0;
            DialogueId = null;
            IsDialogueStarted = false;
            _dialogueWindow.SetActive(false);
            IsQuestionNode = false;
            _dialogueNodes = new List<DialogueNode>();

            foreach (var answer in _answers)
            {
                answer.text = null;
            }

            CameraZoom(false);

            InputToggles.DialogueInputs(false);

            DisableEvents();
        }

        private void QuestionNode()
        {
            if (!_dialogueNodes[_pageIndex].IsQuestion) return;
            IsQuestionNode = true;

            EventSystem.current.SetSelectedGameObject(_firstAnswerGameObject);

            _nextButton.gameObject.SetActive(false);
            _answerWindow.SetActive(true);

            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            for (var i = 0; i < _dialogueNodes[_pageIndex].AnswerNode.Answers.Length; i++)
            {
                _buttons[i].gameObject.SetActive(true);
                _answers[i].text = _dialogueNodes[_pageIndex].AnswerNode.Answers[i];
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
            if (IsQuestionNode) ChooseEvent?.Invoke(1, QuestionNumber, DialogueId);

        }

        private void Second_pressed()
        {
            if (IsQuestionNode) ChooseEvent?.Invoke(2, QuestionNumber, DialogueId);
        }

        private void Third_pressed()
        {
            if (IsQuestionNode) ChooseEvent?.Invoke(3, QuestionNumber, DialogueId);
        }

        private void CameraZoom(bool zoomIn)
        {
            Camera = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();

            Camera.m_Lens.OrthographicSize = zoomIn ? ZoomInSize : 3f;
        }

        private void EnableEvents()
        {
            Inputs.Instance.Input.Dialogue.Next.performed += _ => Next_pressed();
            Inputs.Instance.Input.Dialogue.First.performed += _ => First_pressed();
            Inputs.Instance.Input.Dialogue.Second.performed += _ => Second_pressed();
            Inputs.Instance.Input.Dialogue.Third.performed += _ => Third_pressed();

            ChooseEvent += ChooseAnswer;
        }

        private void DisableEvents()
        {
            Inputs.Instance.Input.Dialogue.Next.performed -= _ => Next_pressed();
            Inputs.Instance.Input.Dialogue.First.performed -= _ => First_pressed();
            Inputs.Instance.Input.Dialogue.Second.performed -= _ => Second_pressed();
            Inputs.Instance.Input.Dialogue.Third.performed -= _ => Third_pressed();

            ChooseEvent -= ChooseAnswer;
        }
    }
}
