﻿using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
        private const string StringTableCollectionName = "DialogueTest";

        [SerializeField] private GameObject _pressE;
        private DialogueManager _dialogueManager;
        private List<DialogueNode> _dialogueNodes;
        private Player _player;

        private string _name = "Dialogue Test--";
        private string _narrative = "Narrative 1--";
        private string _narrative2 = "narrative 2--";
        private string _narrative3 = "Question one?--";
        private string _narrative4 = "Question two?--";
        private string _narrative5 = "Question three?--";
        private string _narrativeYes = "Yes--";
        private string _narrativeNo = "No--";

        private bool _isEnter;
        private bool _isDialogueStarted = false;

        [Space]
        [SerializeField] private InputAction _use;
        [SerializeField] private InputActionAsset _controls;

        private void Awake()
        {
            _dialogueManager = DialogueManager.Instance;
            _dialogueNodes = new List<DialogueNode>();

            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            LoadStrings();
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
            _pressE.SetActive(true);
            _isEnter = true;
        }

        private void OnTriggerExit2D(Component objectCollider)
        {
            if (objectCollider.tag != "Player") return;
            _isEnter = false;
            _pressE.SetActive(false);
            _isDialogueStarted = false;
            _dialogueManager.FinishDialogue();
        }

        private void AnswerAction(int index, int questionNumber)
        {
            switch (questionNumber)
            {
                case 1:
                    switch (index)
                    {
                        case 1:
                            PlayerEvents.GiveExp(100);
                            break;
                        case 2:
                            PlayerEvents.GiveExp(200);
                            break;
                        case 3:
                            PlayerEvents.GiveExp(_player.Stats.MaxExp - _player.Stats.Exp);
                            break;
                    }
                    break;
                case 2:
                    if (index == 2) _dialogueManager.FinishDialogue();
                    break;
                case 3:
                    if (index == 1) SceneLoader.SwitchScene("TestLevel002");
                    break;
            }
        }

        private void Use_started(InputAction.CallbackContext ctx)
        {
            if (!_isEnter || _isDialogueStarted != false) return;
            _pressE.SetActive(false);
            _isDialogueStarted = true;
            _dialogueManager.StartDialogue(_dialogueNodes);
        }

        private void OnEnable()
        {
            //Inputs.Instance.Input.PlayerInGame.Use.started += _ => Use_started();
            _dialogueManager.ChooseEvent += AnswerAction;

            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
            
        }

        private void OnDisable()
        {
            //Inputs.Instance.Input.PlayerInGame.Use.started += _ => Use_started();
            _dialogueManager.ChooseEvent -= AnswerAction;

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
                _dialogueNodes.Clear();

                var stringTable = loadingOperation.Result;
                _name = GetLocalizedString(stringTable, "name");
                _narrative = GetLocalizedString(stringTable, "narrative");
                _narrative2 = GetLocalizedString(stringTable, "narrative2");
                _narrative3 = GetLocalizedString(stringTable, "narrative3");
                _narrative4 = GetLocalizedString(stringTable, "narrative4");
                _narrative5 = GetLocalizedString(stringTable, "narrative5");
                _narrativeYes = GetLocalizedString(stringTable, "narrative_yes");
                _narrativeNo = GetLocalizedString(stringTable, "narrative_no");

                _dialogueNodes.Add(new DialogueNode(_name, _narrative));
                _dialogueNodes.Add(new DialogueNode(_name, _narrative2));
                _dialogueNodes.Add(new DialogueNode(_name, _narrative3, new AnswerNode("100", "200", "Level UP")));
                _dialogueNodes.Add(new DialogueNode(_name, _narrative4, new AnswerNode(_narrativeYes, _narrativeNo)));
                _dialogueNodes.Add(new DialogueNode(_name, _narrative5, new AnswerNode(_narrativeYes, _narrativeNo)));
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
