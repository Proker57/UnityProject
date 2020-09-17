using System.Globalization;
using System.Linq;
using System.Text;

using UnityEngine;

namespace NoSuchStudio.Localization {
    /// <summary>
    /// Contains helper methods for handling locales and languages.
    /// </summary>
    public static class CultureUtil {
        public static void PrintAllCultures() {
            StringBuilder sb = new StringBuilder();
            CultureInfo.GetCultures(CultureTypes.AllCultures).ToList().ForEach(ci => {
                sb.Append(ci.EnglishName);
                //sb.Append(ci.ToStringExt());
                sb.Append("\n");
            });
            Debug.Log(sb.ToString());
        }

        /// <summary>
        /// Get system's active culture info.
        /// </summary>
        /// <returns>system's active culture info.</returns>
        public static CultureInfo SystemCultureInfo() {
            return CultureInfo.CurrentCulture;
        }

        /// <summary>
        /// Get the english name of the current system culture info.
        /// </summary>
        /// <returns>the english name of the current system culture info.</returns>
        public static string SystemLanguage() {
            return SystemCultureInfo().EnglishName;
        }

        public static string ToStringExt(this CultureInfo cultureInfo) {
            return string.Format("CI: name: {0}, nativename: {1}, englishname: {2}, ISO: {3}, RTL: {4}",
                cultureInfo.Name,
                cultureInfo.NativeName,
                cultureInfo.EnglishName,
                cultureInfo.ThreeLetterISOLanguageName,
                cultureInfo.TextInfo.IsRightToLeft);
        }
    }
}
