﻿using UnityEngine;
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
            SpriteUi = Resources.Load<Sprite>("Images/Weapons/UI/BrokenSwordUI");
            Sprite = Resources.Load<Sprite>("Images/Weapons/OnPlayer/BrokenSword");

            Type = "Sword";
            Name = "Sword Broken";
            Description = "Old broken sword";

            Damage = 10;
            AttackSpeed = 0.8f;
            Radius = 8f;
            SellCost = 30;

            LoadStrings();
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
