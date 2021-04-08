using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using BOYAREngine.Engine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine
{
    public class StartDialogue : MonoBehaviour
    {
        private const string StringTableCollectionName = "Dialogue";

        public string DialogueID;

        [Header("s0...n: splitString[n] - String from Narrative Table")]
        [Header("c0...n: common[n] - String from Common Answers")]
        [SerializeField] private List<DialogueNode> _dialogueNodes;
        [SerializeField] private List<DialogueNode> _dialogueNodesOrigin;

        private DialogueManager _dialogueManager;
        private Player _player;

        private bool _isEnter;
        private bool _isDialogueStarted = false;

        [Space]
        [SerializeField] private InputAction _use;
        [SerializeField] private InputActionAsset _controls;
        [Space]
        [SerializeField] private GameObject _panel;

        private void Awake()
        {
            _dialogueManager = DialogueManager.Instance;
            _dialogueNodesOrigin = CloneList.DeepCopy(_dialogueNodes);

            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            LoadStrings();

            gameObject.AddComponent(Type.GetType("BOYAREngine.Narrative." + DialogueID));
        }

        private void Start()
        {
            var iam = _controls.FindActionMap("PlayerInGame");
            _use = iam.FindAction("Use");
            _use.started += Use_started;
        }

        private void OnTriggerEnter2D(Component objectCollider)
        {
            if (objectCollider.tag != "Player") return;
            _panel.SetActive(true);
            _isEnter = true;
        }

        private void OnTriggerExit2D(Component objectCollider)
        {
            if (objectCollider.tag != "Player") return;
            _isEnter = false;
            _panel.SetActive(false);
            _isDialogueStarted = false;
            _dialogueManager.FinishDialogue();
        }

        private void Use_started(InputAction.CallbackContext ctx)
        {
            if (!_isEnter || _isDialogueStarted != false) return;
            _panel.SetActive(false);
            _isDialogueStarted = true;
            _dialogueManager.StartDialogue(_dialogueNodes, DialogueID);
        }

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        private void OnSelectedLocaleChanged(Locale obj)
        {
            LoadStrings();
        }

        private async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                //_dialogueNodes.Clear();

                var stringTable = loadingOperation.Result;

                var splitString = GetLocalizedString(stringTable, DialogueID).Split('\n');
                var common = GetLocalizedString(stringTable, "common").Split('\n');

                _dialogueNodes = CloneList.DeepCopy(_dialogueNodesOrigin);

                foreach (var dialogueNode in _dialogueNodes)
                {
                    dialogueNode.LoadStrings(splitString[int.Parse(dialogueNode.Name)], splitString[int.Parse(dialogueNode.Narrative)]);

                    if (!dialogueNode.IsQuestion) continue;

                    for (var i = 0; i < dialogueNode.AnswerNode.Answers.Length; i++)
                    {
                        if (Regex.IsMatch(dialogueNode.AnswerNode.Answers[i], @"^c[0-9]$") && dialogueNode.AnswerNode.Answers[i] != null)                    // c = common
                        {
                            dialogueNode.AnswerNode.Answers[i] = (common[int.Parse(new string(dialogueNode.AnswerNode.Answers[i].Where(char.IsDigit).ToArray()))]);
                        }
                        else if (Regex.IsMatch(dialogueNode.AnswerNode.Answers[i], @"^s[0-100]$"))                                                          // s = split
                        {
                            //dialogueNode.AnswerNode.LoadStrings(splitString[int.Parse(new string(dialogueNode.AnswerNode.Answers[i].Where(char.IsDigit).ToArray()))]);
                            dialogueNode.AnswerNode.Answers[i] = (splitString[int.Parse(new string(dialogueNode.AnswerNode.Answers[i].Where(char.IsDigit).ToArray()))]);
                        }
                    }
                }
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
