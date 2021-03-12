using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace BOYAREngine.Quests
{
    public class QuestManager : MonoBehaviour, ISaveable
    {
        public static QuestManager Instance = null;

        public List<Task> Tasks = new List<Task>();

        public GameObject[] Cells;

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

        private void OnNewQuest(Task task)
        {
            Tasks.Add(task);

            UpdateCells();
        }

        private void UpdateCells()
        {
            var index = 0;

            foreach (var cell in Cells)
            {
                cell.transform.GetChild(0).GetComponent<Text>().text = "null";
                cell.transform.GetChild(1).GetComponent<Text>().text = "null";
                cell.transform.GetChild(2).GetComponent<Toggle>().isOn = false;

                cell.SetActive(false);
            }

            foreach (var task in Tasks)
            {
                Cells[index].SetActive(true);
                Cells[index].transform.GetChild(0).GetComponent<Text>().text = task.Name;
                Cells[index].transform.GetChild(1).GetComponent<Text>().text = task.Description;
                Cells[index].transform.GetChild(2).GetComponent<Toggle>().isOn = task.IsFinished;

                index++;
            }
        }

        private void OnEnable()
        {
            QuestEvents.NewQuest += OnNewQuest;

            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            QuestEvents.NewQuest -= OnNewQuest;

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
        }
    }

    [System.Serializable]
    public class QuestManagerData
    {
        public List<Task> Tasks;
    }
}
