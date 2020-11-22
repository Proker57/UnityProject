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

        private List<DialogueNode> _dialogue;
        private DialogueManager _dialogueManager;

        private string _name = "Dialogue Test";
        private string _narrative = "You made it! Congratulations! Now eat shit and die!";
        private string _narrative2 = "Hohoho, it's the second page. You really love to talk with people. Suck a dick";

        private void Awake()
        {
            _dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            StartCoroutine(LoadStrings());
        }

        private void Start()
        {

            _dialogue = new List<DialogueNode>();
            _dialogue.Add(new DialogueNode(_name, _narrative));
            _dialogue.Add(new DialogueNode(_name, _narrative2));
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                _dialogueManager.StartDialogue(_dialogue);
                print("Start dialogue");
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                _dialogueManager.FinishDialogue();
                print("Finish dialogue");
            }
        }

        private void OnEnable()
        {
            StartCoroutine(LoadStrings());
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
                var stringTable = loadingOperation.Result;
                _name = _name.Replace(_name, GetLocalizedString(stringTable, "name"));
                //_name = GetLocalizedString(stringTable, "name");
                _narrative = GetLocalizedString(stringTable, "narrative");
                _narrative2 = GetLocalizedString(stringTable, "narrative2");
            }
            else
            {
                Debug.LogError("Could not load String Table\n" + loadingOperation.OperationException.ToString());
            }
        }

        private string GetLocalizedString(StringTable table, string entryName)
        {
            var entry = table.GetEntry(entryName);
            return entry.GetLocalizedString();
        }
    }
}
