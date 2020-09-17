using System.Collections.Generic;
using System.Linq;

namespace NoSuchStudio.Common {
    public static class ToStringExts {

        public static string ToStringExt<K, V>(this KeyValuePair<K, V> kvp) {
            //Debug.Log("here " + kvp.Key + " / " + kvp.Value);
            return string.Format("[{0}] => {1}", kvp.Key, kvp.Value);
        }

        public static string ToStringExt<K1, K2, V>(this KeyValuePair<K1, Dictionary<K2, V>> kvp) {
            return string.Format("[{0}] => {1}", kvp.Key, kvp.Value.ToStringExt());
        }

        public static string ToStringExt<K, K2, V>(this Dictionary<K, Dictionary<K2, V>> dic) {
            return string.Join(", ", dic.Select((kvp) => kvp.ToStringExt()));
        }

        public static string ToStringExt<K1, V>(this KeyValuePair<K1, List<V>> kvp) {
            return string.Format("[{0}] => {1}", kvp.Key, kvp.Value.ToStringExt());
        }

        public static string ToStringExt<K, V>(this Dictionary<K, List<V>> dic) {
            return string.Join(", ", dic.Select((kvp) => kvp.ToStringExt()));
        }

        public static string ToStringExt<K, V>(this Dictionary<K, V> dic) {
            return string.Join(", ", dic.Select((kvp) => kvp.ToStringExt()));
        }

        public static string ToStringExt<T>(this List<T> list) => "[" + string.Join(", ", list) + "]";

        public static string ToStringExt<T>(this List<List<T>> listOfLists) => "[" + string.Join(", ", listOfLists.Select(l => l.ToStringExt())) + "]";
    }
}