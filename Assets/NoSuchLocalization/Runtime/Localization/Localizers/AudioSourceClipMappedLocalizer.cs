using UnityEngine;

namespace NoSuchStudio.Localization.Localizers {
    /// <summary>
    /// Localizes <see cref="AudioClip"/> field of a <see cref="AudioSource"/> component by providing a mapping from
    /// language to <see cref="AudioClip"/> resources.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu(LocalizationService.ComponentMenuPath + "/Audio Source Clip Localizer (Mapped)")]
    public class AudioSourceClipMappedLocalizer : AssetMapComponentLocalizer<AudioSourceClipMappedLocalizer, AudioSource, AudioClip, LocalizedAssetDataAudioClip> {
        public override void UpdateComponent() {
            string lang = LocalizationService.CurrentLanguage;
            if (string.IsNullOrEmpty(lang)) return;
            _component.clip = _assets.ContainsKey(lang) && _assets[lang] != null ? _assets[lang] : _defaultAsset;
        }
    }
}
