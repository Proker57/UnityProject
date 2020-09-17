using System;
using System.Linq;
using UnityEngine;

namespace NoSuchStudio.Localization {
    /// <summary>
    /// Base class for translation sources that are backed by a file.
    /// This class reads the translations from the backing file and registers them with
    /// <see cref="LocalizationService"/>. 
    /// </summary>
    [ExecuteInEditMode]
    public abstract class FileTranslationSource : BaseTranslationSource {
        /// <summary>
        /// The backing text asset.
        /// </summary>
        [SerializeField] protected TextAsset _textAsset;
        public TextAsset textAsset {
            get { return _textAsset; }
            set {
                Disconnect<LocalizationService>();
                _textAsset = value;
                ImportTranslations();
                NormalizeLocaleNames();
                Connect<LocalizationService>();
            }
        }

        /// <summary>
        /// Subclasses should implement this method to read the file contents and populate
        /// the <see cref="BaseTranslationSource._translations"/> field.
        /// </summary>
        protected abstract void ImportTranslations();
        public void Reload() {
            Disconnect<LocalizationService>();
            ImportTranslations();
            NormalizeLocaleNames();
            Connect<LocalizationService>();
        }

        /// <summary>
        /// Goes through all the locales loaded by this FileTranslationSource and tries to use
        /// the name from the current locale database.
        /// </summary>
        protected void NormalizeLocaleNames() {
            if (!LocalizationService.IsReady) return;
            var localeDB = LocalizationService.Instance.localeDatabase;
            _translations.Keys.ToList().ForEach(phrase => {
                var curDic = _translations[phrase];
                curDic.Keys.ToList().ForEach(ln => {
                    Locale l = localeDB[ln];
                    if (l == null) return;
                    var curVal = curDic[ln];
                    curDic.Remove(ln);
                    curDic[l.NormalizedName] = curVal;
                });
            });
        }

        protected virtual void Start() {
            Reload();
        }

#if UNITY_EDITOR
        [NonSerialized] TextAsset _cachedTextAsset;
        protected override void Reset() {
            _textAsset = null;
            base.Reset();
        }

        public virtual void OnValidate() {
            Init();
            if (_cachedTextAsset != _textAsset) {
                Reload();
            }
            _cachedTextAsset = _textAsset;
        }
#endif
    }
}
