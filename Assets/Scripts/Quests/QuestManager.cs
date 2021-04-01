using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace BOYAREngine.Quests
{
    public class QuestManager : MonoBehaviour, ISaveable
    {
        public static QuestManager Instance;

        public List<Task> Tasks = new List<Task>();

        public GameObject[] Cells;
        public Text[] Texts;
        public Text[] Descriptions;
        public Toggle[] Toggles;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }
        }

        public void UpdateQuestList(string id)
        {
            foreach (var task in Tasks)
            {
                if (task.Id != id) continue;
                Tasks.Remove(task);
                break;
            }

            UpdateCells();
        }

        public void UpdateCells()
        {
            for (var i = 0; i < Cells.Length; i++)
            {
                Texts[i].text = "null text";
                Descriptions[i].text = "null description";
                Toggles[i].isOn = false;
                Cells[i].SetActive(false);
            }

            for (var i = 0; i < Tasks.Count; i++)
            {
                Cells[i].SetActive(true);
                Texts[i].text = Tasks[i].Name;
                Descriptions[i].text = Tasks[i].Description;
                Toggles[i].isOn = Tasks[i].IsFinished;
            }
        }

        private void OnNewQuest(Task task)
        {
            Tasks.Add(task);

            UpdateCells();
        }

        private void OnNewGame()
        {
            Tasks = new List<Task>();
            UpdateCells();
        }

        private void OnEnable()
        {
            QuestEvents.NewQuest += OnNewQuest;
            Events.NewGame += OnNewGame;

            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            QuestEvents.NewQuest -= OnNewQuest;
            Events.NewGame -= OnNewGame;

            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        private void OnSelectedLocaleChanged(Locale obj)
        {
            foreach (var task in Tasks)
            {
                task.LoadStrings();
            }
        }

        public object CaptureState()
        {
            return new QuestManagerData()
            {
                Tasks = Tasks
            };
        }

        public void RestoreState(object state)
        {
            var questManagerData = (QuestManagerData) state;

            Tasks = questManagerData.Tasks;

            UpdateCells();
        }
    }

    [System.Serializable]
    public class QuestManagerData
    {
        public List<Task> Tasks;
    }
}
