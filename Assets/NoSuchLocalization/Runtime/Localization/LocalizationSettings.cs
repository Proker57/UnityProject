using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using UnityEngine;

namespace NoSuchStudio.Localization {
    public static class LocalizationSettings {

        static LocalizationSettings() {

        }
        public static void OnJsonError(object target, ErrorEventArgs args) {
            Debug.LogWarningFormat("Json error: {0}", args.ErrorContext.Error);
            args.ErrorContext.Handled = true;
        }

        private static JsonSerializerSettings _jsonSettings;
        public static JsonSerializerSettings jsonSettings {
            get {
                _jsonSettings = _jsonSettings ?? CreateJsonSettings();
                return _jsonSettings;
            }
        }

        private static JsonSerializerSettings CreateJsonSettings() {
            var ret = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ret.Converters.Add(new StringEnumConverter());
            ret.Error = new EventHandler<ErrorEventArgs>(OnJsonError);
            return ret;
        }
    }
}
