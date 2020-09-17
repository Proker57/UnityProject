using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NoSuchStudio.Common.Service {
    /// <summary>
    /// A Service is a singleton MonoBehaviour with certain capibilities in Unity.
    /// </summary>
    /// <remarks>
    /// This class serves as a singleton for Unity.
    /// A Service:
    /// - Ensures there is at most one Instance of the MonoBehaviour Active at any given point in
    ///   time. If another Intance becomes Active, the old one will go offline.
    /// - Provides a static API surface through the currently Active Instance. Individual services
    ///   can provide fallback methods in cases there is no Instance for the service.
    /// - Will find all Components that use it in the Scene initially and connects them. 
    ///   <see cref="IServiceComponent{T}"/> 
    /// </remarks>
    public abstract class Service<T> : NoSuchMonoBehaviour
        where T : Service<T> {

        private static bool alreadyLogged;
        /// <summary>
        /// The global instance of this <see cref="Common.Service"/>. For normal usage,
        /// use <see cref="RegisterInstance(T)"/> and <see cref="UnregisterInstance(T)"/>.
        /// </summary>
        protected static T gInstance;
        /// <summary>
        /// The property for accessing the global instance of this <see cref="Common.Service"/>. 
        /// For normal usage, use <see cref="RegisterInstance(T)"/>
        /// and <see cref="UnregisterInstance(T)"/>.
        /// </summary>
        public static T Instance {
            get {
                if (gInstance == null) {
                    if (!alreadyLogged) LogLog<T>("[Static] instance not set, searching...");
                    List<T> tmpList = FindObjectsOfType<T>().ToList();
                    tmpList.Find(tmp => {
                        if (tmp == null) {
                            if (!alreadyLogged) LogLog<T>(tmp, $"[Static] no instances found. Create one and enable it to use this service.");
                            alreadyLogged = true;
                            return false;
                        } else if (!EditorUtilities.IsInMainStage(tmp.gameObject)) {
                            if (!alreadyLogged) LogLog<T>(tmp, $"[Static] instance {tmp.name} not in scene.");
                            alreadyLogged = true;
                            return false;
                        } else if (!tmp.InstanceReady) {
                            if (!alreadyLogged) LogLog<T>(tmp, "[Static] found `{0}` but not ready (is it enabled and active?), check it in editor and fix any setup problems.", tmp.name);
                            alreadyLogged = true;
                            return false;
                        } else {
                            LogLog<T>(tmp, "[Static] instance found: `{0}`.", tmp.name);
                            Instance = tmp;
                            return true;
                        }
                    });
                }
                return gInstance;
            }
            protected set {
                alreadyLogged = false;
                if (value != null && !EditorUtilities.IsInMainStage(value.gameObject)) {
                    LogWarn<T>(gInstance, "[Static] new instance isn't in scene. Not registering.");
                    return;
                }

                if (gInstance != null) {
                    LogLog<T>(gInstance, "[Static] replacing previous instance.");
                    gInstance.OnServiceUnregister();
                }

                gInstance = value;
                    
                if (gInstance == null) {
                    LogLog<T>("[Static] global instance set to null.");
                } else {
                    LogLog<T>(gInstance, "[Static] registering global instance to {0}.", gInstance.name);
                    gInstance.OnServiceRegister();
                }
            }
        }

        protected bool _instanceReady; // is this instance of service ready to be registered?
        public bool InstanceReady {
            get { return _instanceReady; }
        }

        protected virtual void OnEnable() {
            _instanceReady = true;
            RegisterInstance((T)this);
        }

        protected virtual void OnDisable() {
            UnregisterInstance((T)this);
            _instanceReady = false;
        }

        /// <summary>
        /// <returns>
        /// Returns true if this Service has an Active Instance and can be used.
        /// </returns>
        /// </summary>
        public static bool IsReady {
            get { return Instance != null; }
        }

        /// <summary>
        /// Callback for when an instance becomes the global instance.
        /// </summary>
        public virtual void OnServiceRegister() { }

        /// <summary>
        /// Callback for when an instance is no longer the global instance.
        /// </summary>
        public virtual void OnServiceUnregister() { }

        /// <summary>
        /// Checks if the current instance is the global instance.
        /// </summary>
        public bool IsInstance {
            get { return Instance == this; }
        }

        /// <summary>
        /// Service instances call this OnEnable to become the global instance.
        /// </summary>
        protected static void RegisterInstance(T instance) {
            Instance = instance;
        }

        /// <summary>
        /// Service instances call this OnDisable to release the global instance.
        /// </summary>
        protected static void UnregisterInstance(T instance) {
            if (gInstance == instance) {
                Instance = null;
            } else {
                LogWarn<T>("instance different than current, NOP.");
            }
        }

        /// <summary>
        /// Same effect as disabling and enabling the service instance.
        /// </summary>
        public void ReRegisterService() {
            if (_instanceReady) {
                UnregisterInstance((T)this);
                RegisterInstance((T)this);
            }
        }

        private static T CreateInstance() {
            GameObject instance = new GameObject(typeof(T).Name);
            return instance.AddComponent<T>();
        }
    }
}
