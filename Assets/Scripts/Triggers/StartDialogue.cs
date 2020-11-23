using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine
{
    public class StartDialogue : MonoBehaviour
    {
        private const string StringTableCollectionName = "DialogueTest";

        private DialogueManager _dialogueManager;
        private List<DialogueNode> _dialogueNodes;

        private string _name = "Dialogue Test";
        private string _narrative = "Narrative 1";
        private string _narrative2 = "narrative 2";
        private string _narrative3 = "Question one?";
        private string _narrative4 = "Question two?";
        private string _narrative5 = "Question three?";

        private void Awake()
        {
            _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            _dialogueNodes = new List<DialogueNode>();

            StartCoroutine(LoadStrings());
        }

        private void OnTriggerEnter2D(Component objectCollider)
        {
            if (objectCollider.tag == "Player")
            {
                _dialogueManager.StartDialogue(_dialogueNodes);
            }
        }

        private void OnTriggerExit2D(Component objectCollider)
        {
            if (objectCollider.tag == "Player")
            {
                _dialogueManager.FinishDialogue();
            }
        }

        private void AnswerAction(int index)
        {
            switch (index)
            {
                case 1:
                    Debug.Log("First answer");
                    break;
                case 2:
                    Debug.Log("Second answer");
                    break;
                case 3:
                    Debug.Log("Third answer");
                    break;
                default:
                    Debug.Log("Default switch case");
                    break;
            }
        }

        private void OnEnable()
        {
            _dialogueManager.ChooseEvent += AnswerAction;
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        private void OnSelectedLocaleChanged(Locale obj)
        {
            StartCoroutine(LoadStrings());
        }

        private IEnumerator LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            yield return loadingOperation;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                _dialogueNodes.Clear();

                var stringTable = loadingOperation.Result;
                _name = GetLocalizedString(stringTable, "name");
                _narrative = GetLocalizedString(stringTable, "narrative");
                _narrative2 = GetLocalizedString(stringTable, "narrative2");

                _dialogueNodes.Add(new DialogueNode(name, _narrative));
                _dialogueNodes.Add(new DialogueNode(name, _narrative2));
                _dialogueNodes.Add(new DialogueNode(name, _narrative3, new AnswerNode("1", "2", "3")));
                _dialogueNodes.Add(new DialogueNode(name, _narrative4, new AnswerNode("1", "2")));
                _dialogueNodes.Add(new DialogueNode(name, _narrative5, new AnswerNode("1")));
            }
            else
            {
                Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException);
            }
        }

        private static string GetLocalizedString(StringTable table, string entryName)
        {
            var entry = table.GetEntry(entryName);
            return entry.GetLocalizedString();
        }
    }
}
