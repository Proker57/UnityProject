using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _narrative;
        [SerializeField] private Button _nextButton;
        [SerializeField] private GameObject _dialogueWindow;
        private List<DialogueNode> _dialogueNodes;

        private int _index = 0;

        private void Awake()
        {
            _dialogueNodes = new List<DialogueNode>();
        }

        public void StartDialogue(List<DialogueNode> listNodes)
        {
            _dialogueWindow.SetActive(true);

            _dialogueNodes = listNodes;

            SetStrings();
        }

        public void NextNode()
        {
            _index++;

            if (_index < _dialogueNodes.Count)
            {
                SetStrings();
            }
            else
            {
                FinishDialogue();
            }
        }

        private void SetStrings()
        {
            _name.text = _dialogueNodes[_index].Name;
            _narrative.text = _dialogueNodes[_index].Narrative;
        }

        public void FinishDialogue()
        {
            //_dialogueNodes = new List<DialogueNode>();
            _index = 0;
            _dialogueWindow.SetActive(false);
        }
    }
}
