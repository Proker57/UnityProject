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
            Sprite = "Images/Weapons/OnPlayer/SwordSmall";
            SpriteUi = "Images/Weapons/UI/SwordSmallUI";

            Name = "Sword Small";
            Type = "Sword";
            Description = "Small sword";

            Damage = 15;
            AttackSpeed = 1f;
            Radius = 10f;
            SellCost = 100;

            PushForce = 30;

            AttackSpeedBase = AttackSpeed;
            LoadStrings();
        }

        internal override int FirstAttack()
        {
            Debug.Log("First attack: " + Damage + "_ Attack Speed: " + AttackSpeed);
            AttackSpeed = 1.1f;
            return Damage;
        }

        internal override int SecondAttack()
        {
            Debug.Log("Second attack: " + Damage + "_ Attack Speed: " + AttackSpeed);
            AttackSpeed = 2.1f;
            return Damage;
        }

        internal override int ThirdAttack()
        {
            Debug.Log("Third attack: " + (Damage + 40) + "_ Attack Speed: " + AttackSpeed);
            AttackSpeed = 1f;
            return (Damage + 40);
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

                var splitString = GetLocalizedString(stringTable, "small_sword").Split('\n');

                Name = splitString[0];
                Description = splitString[1];
                Type = splitString[2];
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
