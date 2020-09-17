using System;
using TMPro;
using UnityEngine;

namespace NoSuchStudio.Localization.Localizers {
    [Serializable]
    public class LocalizedAssetDataFont : LocalizedAssetData<TMP_FontAsset> {
        public LocalizedAssetDataFont(string name, TMP_FontAsset data) : base(name, data) { }
    }

    [Serializable]
    public class LocalizedAssetDataSprite : LocalizedAssetData<Sprite> {
        public LocalizedAssetDataSprite(string name, Sprite data) : base(name, data) { }
    }

    [Serializable]
    public class LocalizedAssetDataAudioClip : LocalizedAssetData<AudioClip> {
        public LocalizedAssetDataAudioClip(string name, AudioClip data) : base(name, data) { }
    }
}
