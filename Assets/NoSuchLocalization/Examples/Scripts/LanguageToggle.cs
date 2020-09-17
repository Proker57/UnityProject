using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using NoSuchStudio.Localization;

[RequireComponent(typeof(Toggle))]
public class LanguageToggle : MonoBehaviour
{
    [SerializeField] private string _language;

    void Start() {
        if (GetComponent<Toggle>().isOn) {
            OnToggleClick(true);
        }
    }

    public void OnToggleClick(bool b) {
        if (b) {
            LocalizationService.CurrentLanguage = _language;
        }
    }
}
