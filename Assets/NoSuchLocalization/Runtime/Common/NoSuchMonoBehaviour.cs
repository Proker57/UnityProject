using System;

using UnityEngine;

namespace NoSuchStudio.Common {
    /// <summary>
    /// Base class for MonoBehaviours that have helper functions from <see cref="UnityObjectLoggerExt"/> and <see cref="MonoBehaviourRunDelayedExt"/>
    /// included in them.
    /// </summary>
    public abstract class NoSuchMonoBehaviour : MonoBehaviour {
        
        public Logger logger {
            get {
                Type thisType = GetType();
                return UnityObjectLoggerExt.GetLoggerByType(thisType).logger;
            }
        }
        public LoggerConfig loggerConfig {
            get {
                Type thisType = GetType();
                return UnityObjectLoggerExt.GetLoggerByType(thisType).loggerConfig;
            }
        }
        
        protected void LogLog(string format, params object[] args) {
            UnityObjectLoggerExt.LogLog(this, format, args);
        }
        protected void LogWarn(string format, params object[] args) {
            UnityObjectLoggerExt.LogWarn(this, format, args);
        }
        protected void LogError(string format, params object[] args) {
            UnityObjectLoggerExt.LogError(this, format, args);
        }

        public static void LogLog<T>(string format, params object[] args) {
            UnityObjectLoggerExt.LogLog<T>(format, args);
        }
        public static void LogWarn<T>(string format, params object[] args) {
            UnityObjectLoggerExt.LogWarn<T>(format, args);
        }
        public static void LogError<T>(string format, params object[] args) {
            UnityObjectLoggerExt.LogError<T>(format, args);
        }

        public static void LogLog<T>(UnityEngine.Object unityObj, string format, params object[] args) {
            UnityObjectLoggerExt.LogLog<T>(unityObj, format, args);
        }
        public static void LogWarn<T>(UnityEngine.Object unityObj, string format, params object[] args) {
            UnityObjectLoggerExt.LogWarn<T>(unityObj, format, args);
        }
        public static void LogError<T>(UnityEngine.Object unityObj, string format, params object[] args) {
            UnityObjectLoggerExt.LogError<T>(unityObj, format, args);
        }

        protected Coroutine RunDelayed(float delay, Action a) {
            return MonoBehaviourRunDelayedExt.RunDelayed(this, delay, a);
        }

        protected Coroutine RunDelayedRealtime(float delay, Action a) {
            return MonoBehaviourRunDelayedExt.RunDelayedRealtime(this, delay, a);
        }
    }
}