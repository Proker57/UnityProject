
using System;

using TMPro;

using UnityEngine;

namespace NoSuchStudio.Localization.Localizers {
    /// <summary>
    /// Localizes <see cref="TMP_Text.font"/> field of a <see cref="TextMeshProUGUI"/> component by providing a mapping from
    /// language to <see cref="TMP_FontAsset"/> resources.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    [AddComponentMenu(LocalizationService.ComponentMenuPath + "/Text Mesh Pro Font Localizer (Mapped)")]
    public class TMProFontMappedLocalizer : AssetMapComponentLocalizer<TMProFontMappedLocalizer, TextMeshProUGUI, TMP_FontAsset, LocalizedAssetDataFont> {
        public override void UpdateComponent() {
            string locale = LocalizationService.CurrentLocale;
            if (string.IsNullOrEmpty(locale)) return;
            _component.font = _assets.ContainsKey(locale) && _assets[locale] != null ? _assets[locale] : _defaultAsset;
        }
    }
}
