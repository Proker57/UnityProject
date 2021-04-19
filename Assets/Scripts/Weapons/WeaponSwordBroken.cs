using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine
{
    [System.Serializable]
    public class WeaponSwordBroken : Melee
    {
        public WeaponSwordBroken()
        {
            Sprite = "Images/Weapons/OnPlayer/BrokenSword";
            SpriteUi = "Images/Weapons/UI/BrokenSwordUI";

            Type = "Sword";
            Name = "Sword Broken";
            Description = "Old broken sword";

            Damage = 10;
            AttackSpeed = 0.8f;
            Radius = 0.8f;
            SellCost = 30;

            PushForce = 20f;

            AttackSpeedBase = AttackSpeed;

            LoadStrings();
        }

        internal override int FirstAttack()
        {
            Debug.Log("First attack: " + Damage + "_ Attack Speed: " + AttackSpeed);
            AttackSpeed = 1f;
            return Damage;
        }

        internal override int SecondAttack()
        {
            Debug.Log("Second attack: " + Damage + "_ Attack Speed: " + AttackSpeed);
            AttackSpeed = 2f;
            return Damage;
        }

        internal override int ThirdAttack()
        {
            Debug.Log("Third attack: " + (Damage + 40) + "_ Attack Speed: " + AttackSpeed);
            AttackSpeed = 0.8f;
            return (Damage + 40);
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

                var splitString = GetLocalizedString(stringTable, "broken_sword").Split('\n');

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
