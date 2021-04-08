namespace BOYAREngine.Quests
{
    [System.Serializable]
    public sealed class TestLevel001_test : Task
    {
        public int KillEnemyCurrent = 0;
        public int KillEnemyNeed = 1;

        private string _questName;

        public TestLevel001_test()
        {
            _questName = GetType().Name;

            Id = _questName;
            Name = "Test Quest";
            Description = "Quest description";
            IsFinished = false;

            LoadStrings();

            OnEnable();
        }

        internal override void Finish()
        {
            PlayerEvents.GiveCurrency(100);
            QuestManager.Instance.UpdateQuestList(Id);
            OnDisable();
        }

        private void OnKillEnemy()
        {
            KillEnemyCurrent++;

            if (KillEnemyCurrent < KillEnemyNeed) return;
            IsFinished = true;
            QuestManager.Instance.UpdateCells();
        }

        private void OnEnable()
        {
            KillEvents.KIllEnemy += OnKillEnemy;
        }

        public void OnDisable()
        {
            KillEvents.KIllEnemy -= OnKillEnemy;
        }

        
    }
}
