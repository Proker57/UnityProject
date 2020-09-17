using NoSuchStudio.Common;
using NoSuchStudio.Common.Service;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NoSuchStudio.Localization {
    /// <summary>
    /// Base class for components that localize other components.
    /// </summary>
    /// <typeparam name="LT">Type of the component that localizes CT.</typeparam>
    /// <typeparam name="CT">Type of the component that is being localized.</typeparam>
    [ExecuteInEditMode]
    public abstract class ComponentLocalizer<LT, CT> : NoSuchMonoBehaviour, ILocalizationServiceComponent
        where CT : Component
        where LT : ComponentLocalizer<LT, CT> {
        [NonSerialized] protected CT _component;
        [NonSerialized] protected bool _readyToConnect;

#if UNITY_EDITOR
        protected virtual void OnValidate() {
            Init();
            ((ILocalizationServiceComponent)this).Disconnect<LocalizationService>();
            ((ILocalizationServiceComponent)this).Connect<LocalizationService>();
        }
#endif

        protected virtual void Init() {
            _connected = _connected ?? new Dictionary<Type, bool>();
            if (_component == null) _component = GetComponent<CT>();
        }
        protected virtual void Awake() {
            Init();
        }

        /// <summary>
        /// Called when the component need to update due to a change in the translation service
        /// i.e. a language change or service coming online.
        /// </summary>
        public abstract void UpdateComponent();

        public virtual MonoBehaviour mono {
            get { return this; }
        }

        [NonSerialized] protected Dictionary<Type, bool> _connected;
        public virtual bool IsConnected<ST>() where ST : Service<ST> {
            return _connected.ContainsKey(typeof(ST)) ? _connected[typeof(ST)] : false;
        }

        bool IServiceComponent<LocalizationService>.IsConnected<ST>() {
            return IsConnected<LocalizationService>();
        }

        void IServiceComponent<LocalizationService>.Connect<ST>() {
            if (!_readyToConnect) return; // will connect later when ready
            if (!LocalizationService.IsReady) return; // will connect when service is ready
            if (IsConnected<LocalizationService>()) ((ILocalizationServiceComponent)this).Disconnect<LocalizationService>();
            RegisterToLocalization();
            _connected[typeof(LocalizationService)] = true;
            UpdateComponent();
        }

        void IServiceComponent<LocalizationService>.Disconnect<ST>() {
            if (!_readyToConnect) return;
            if (!LocalizationService.IsReady) return;
            UnregisterFromLocalization();
            _connected[typeof(LocalizationService)] = false;
        }

        public virtual void Reconnect<ST>() where ST : Service<ST> {
            if (!_readyToConnect) return;
            if (!Service<ST>.IsReady) return;
            if (typeof(ST) == typeof(LocalizationService)) {
                UnregisterFromLocalization();
                RegisterToLocalization();
                _connected[typeof(LocalizationService)] = true;
            }
        }

        protected virtual void OnLocaleChange(Locale locale) {
            UpdateComponent();
        }

        protected virtual void RegisterToLocalization() {
            LocalizationService.AddLocaleChangeListener(OnLocaleChange);
        }

        protected virtual void UnregisterFromLocalization() {
            LocalizationService.RemoveLocaleChangeListener(OnLocaleChange);
        }

        protected virtual void OnEnable() {
            _readyToConnect = true;
            ((ILocalizationServiceComponent)this).Connect<LocalizationService>();
        }

        protected virtual void OnDisable() {
            ((ILocalizationServiceComponent)this).Disconnect<LocalizationService>();
            _readyToConnect = false;
        }
    }
}
