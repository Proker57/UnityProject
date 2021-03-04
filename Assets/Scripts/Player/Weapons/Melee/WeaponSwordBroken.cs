using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine
{
    [System.Serializable]
    public class WeaponSwordBroken : Melee
    {
        private WeaponManager _weaponManager;

        public WeaponSwordBroken()
        {
            SpriteUi = Resources.Load<Sprite>("Images/Weapons/UI/BrokenSwordUI");
            Sprite = Resources.Load<Sprite>("Images/Weapons/OnPlayer/BrokenSword");

            Type = "Sword";
            Name = "Sword Broken";
            Description = "Old broken sword";

            Damage = 10;
            AttackSpeed = 0.8f;
            Radius = 0.8f;
            SellCost = 30;

            MaxComboNumber = 3;

            _weaponManager = WeaponManager.Instance;

            LoadStrings();
        }

        internal override void FirstAttack()
        {
            Debug.Log("First attack");

            var hit = Physics2D.OverlapCircleAll(_weaponManager.AttackPoint.transform.position, Radius, _weaponManager.DamageLayers);
            foreach (var enemies in hit)
            {
                enemies.GetComponent<IDamageable>().GetDamage(Damage);
            }
        }

        internal override void SecondAttack()
        {
            Debug.Log("Second attack");

            var hit = Physics2D.OverlapCircleAll(_weaponManager.AttackPoint.transform.position, Radius, _weaponManager.DamageLayers);
            foreach (var enemies in hit)
            {
                enemies.GetComponent<IDamageable>().GetDamage(Damage);
            }
        }

        internal override void ThirdAttack()
        {
            Debug.Log("Third attack");

            var hit = Physics2D.OverlapCircleAll(_weaponManager.AttackPoint.transform.position, Radius, _weaponManager.DamageLayers);
            foreach (var enemies in hit)
            {
                enemies.GetComponent<IDamageable>().GetDamage(Damage + 40);
            }
        }

        internal override void SecondaryAttack()
        {
            Debug.Log("Secondary attack");
        }

        internal sealed override async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;

                Type = GetLocalizedString(stringTable, "type_sword");
                Name = GetLocalizedString(stringTable, "sword_broken_name");
                Description = GetLocalizedString(stringTable, "sword_broken_description");
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
