using UnityEngine;
using UnityEngine.UI;

namespace NoSuchStudio.Localization.Localizers {
    /// <summary>
    /// Localizes <see cref="Sprite"/> field of a <see cref="Image"/> component by providing a mapping from
    /// language to <see cref="Sprite"/> resources.
    /// </summary>
    [RequireComponent(typeof(Image))]
    [AddComponentMenu(LocalizationService.ComponentMenuPath + "/Image Sprite Localizer (Mapped)")]
    public class ImageSpriteMappedLocalizer : AssetMapComponentLocalizer<ImageSpriteMappedLocalizer, Image, Sprite, LocalizedAssetDataSprite> {
        public override void UpdateComponent() {
            string lang = LocalizationService.CurrentLocale;
            if (string.IsNullOrEmpty(lang)) return;
            _component.sprite = _assets.ContainsKey(lang) && _assets[lang] != null ? _assets[lang] : _defaultAsset;
        }
    }
}
