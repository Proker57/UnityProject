using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BOYAREngine
{
    public class AnswerHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private DialogueManager _dialogueManager;

        private Text _text;
        private Color _enterColor;
        private Color _exitColor;

        private void Awake()
        {
            _text = GetComponent<Text>();

            _enterColor = new Color32(154, 238 , 73 , 255); // Green
            _exitColor = new Color32(255, 255, 255, 255);  
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _text.color = _enterColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _text.color = _exitColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _text.color = _exitColor;
            _dialogueManager.ChooseEvent(int.Parse(name), _dialogueManager.QuestionNumber);
        }
    }
}
