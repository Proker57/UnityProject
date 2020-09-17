using System;
using System.Collections.Generic;

using UnityEngine;

namespace NoSuchStudio.Localization {
    /// <summary>
    /// To be used with <see cref="AssetMapComponentLocalizer{LT, CT, AT, LAD}"/>.
    /// </summary>
    /// <remarks>
    /// Use a non-generic subclass of this class for localizing different field types.
    /// </remarks>
    /// <example>
    /// <code>
    /// public class SpriteLocalizedAssetData : LocalizedAssetData&lt;Sprite&gt; {
    ///   public SpriteLocalizedAssetData(string name, Sprite data) : base(name, data) { }
    /// }
    /// </code>
    /// </example>
    /// <typeparam name="T">Type of the asset. It should be Serializable by Unity.</typeparam>
    [Serializable]
    public class LocalizedAssetData<T> {
        [SerializeField][LocaleProperty(false, LocalePropertyDrawMode.MenuDropdown)] private string _locale;
        public string locale {
            get { return _locale; }
        }
        [SerializeField] private T _data;
        public T data {
            get { return _data; }
        }
        public LocalizedAssetData(string name, T data) {
            this._locale = name;
            this._data = data;
        }
    }

    /// <summary>
    /// Base class for component localizers that have a map from language to an asset value.
    /// </summary>
    /// <remarks>
    /// The last generic type parameter (LAD) can be removed in 2020.1 since Unity will start
    /// serializing generic classes.
    /// </remarks>
    /// <typeparam name="LT">Type of the localizer component that localizes CT.</typeparam>
    /// <typeparam name="CT">
    /// Type of the component that is being localized. For example if localizing the <see cref="UnityEngine.Sprite"/> of
    /// an <see cref="UnityEngine.UI.Image"/> component, CT = <see cref="UnityEngine.UI.Image"/>
    /// </typeparam>
    /// <typeparam name="AT">
    /// Type of the field on CT that is being localized. For example if localizing the <see cref="UnityEngine.Sprite"/> of
    /// an <see cref="UnityEngine.UI.Image"/>, AT = <see cref="UnityEngine.Sprite"/>.
    /// </typeparam>
    /// <typeparam name="LAD">
    /// This type parameter can be removed in Unity 2020.1 since Unity will start Serializing generic types.
    /// </typeparam>
    public abstract class AssetMapComponentLocalizer<LT, CT, AT, LAD> : ComponentLocalizer<LT, CT>
        where LT : AssetMapComponentLocalizer<LT, CT, AT, LAD>
        where CT : Component
        where LAD : LocalizedAssetData<AT> {
        [SerializeField] protected AT _defaultAsset;
        [SerializeField] protected List<LAD> _assetList;
        [NonSerialized] protected Dictionary<string, AT> _assets;

        protected override void Init() {
            base.Init();
            _assets = _assets ?? new Dictionary<string, AT>();
            _assets.Clear();
            if (_assetList != null) {
                _assetList.ForEach(ld => {
                    if (string.IsNullOrEmpty(ld.locale)) return;
                    _assets[ld.locale] = ld.data;
                });
            }
        }
    }
}
