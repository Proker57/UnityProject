using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BOYAREngine
{
    [System.Serializable]
    public class JailBar : Melee
    {
        public JailBar()
        {
            Sprite = "Images/Weapons/OnPlayer/" + GetType().Name;
            SpriteUi = "Images/Weapons/UI/" + GetType().Name;

            Name = "Jail Bar";
            Type = "Bar";
            Description = "Just a bar from window";

            Damage = 5;
            AttackSpeed = 1f;
            Radius = 0.7f;
            SellCost = 5;

            PushForce = 10;

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
            AttackSpeed = 0.7f;
            return Damage;
        }

        internal override int ThirdAttack()
        {
            Debug.Log("Third attack: " + (Damage + 40) + "_ Attack Speed: " + AttackSpeed);
            AttackSpeed = 1f;
            return (Damage + 15);
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

                var splitString = GetLocalizedString(stringTable, GetType().Name).Split('\n');

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
