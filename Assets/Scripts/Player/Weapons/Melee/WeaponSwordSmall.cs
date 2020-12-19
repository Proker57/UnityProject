using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine
{
    [System.Serializable]
    public class WeaponSwordSmall : Melee
    {
        public WeaponSwordSmall()
        {
            SpriteUi = Resources.Load<Sprite>("Images/Weapons/UI/SwordSmallUI");
            Sprite = Resources.Load<Sprite>("Images/Weapons/SwordSmall");

            Name = "Sword Small";
            Description = "Small sword";

            Damage = 15;
            AttackSpeed = 0.8f;
            SellCost = 100;

            LoadStrings();
        }

        internal override void PrimaryAttack()
        {
            if (!WeaponManager.Instance.IsAbleToAttack) return;
            var hit = Physics2D.OverlapCircleAll(
                WeaponManager.Instance.AttackPoint.position,
                WeaponManager.Instance.Radius,
                WeaponManager.Instance.DamageLayers);
            foreach (var enemies in hit)
            {
                enemies.GetComponent<Damageable>().GetDamage(Damage);
            }

            Debug.Log("Sword small: Primary attack");
        }

        internal override void SecondaryAttack()
        {
            Debug.Log("Sword small: Secondary attack");
        }

        internal sealed override async void LoadStrings()
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(StringTableCollectionName);
            await loadingOperation.Task;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                var stringTable = loadingOperation.Result;

                Name = GetLocalizedString(stringTable, "sword_small_name");
                Description = GetLocalizedString(stringTable, "sword_small_description");
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
