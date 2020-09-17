using System.Collections.Generic;
using TMPro;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NoSuchStudio.Localization {
    /// <summary>
    /// Utility methods for handling locales and common localization patterns.
    /// </summary>
    public static class LocalizationUtils {
        public static TextAlignment RTLReverse(this TextAlignment textAlignment) {
            switch (textAlignment) {
                case TextAlignment.Left:
                    return TextAlignment.Right;
                case TextAlignment.Right:
                    return TextAlignment.Left;
            }
            return textAlignment;
        }

        public static TextAlignmentOptions RTLReverse(this TextAlignmentOptions tao) {
            switch (tao) {
                case TextAlignmentOptions.Right:
                    return TextAlignmentOptions.Left;
                case TextAlignmentOptions.Left:
                    return TextAlignmentOptions.Right;

                case TextAlignmentOptions.TopRight:
                    return TextAlignmentOptions.TopLeft;
                case TextAlignmentOptions.TopLeft:
                    return TextAlignmentOptions.TopRight;

                case TextAlignmentOptions.BottomLeft:
                    return TextAlignmentOptions.BottomRight;
                case TextAlignmentOptions.BottomRight:
                    return TextAlignmentOptions.BottomLeft;

                case TextAlignmentOptions.MidlineRight:
                    return TextAlignmentOptions.MidlineLeft;
                case TextAlignmentOptions.MidlineLeft:
                    return TextAlignmentOptions.MidlineRight;

                case TextAlignmentOptions.BaselineRight:
                    return TextAlignmentOptions.BaselineLeft;
                case TextAlignmentOptions.BaselineLeft:
                    return TextAlignmentOptions.BaselineRight;

                case TextAlignmentOptions.CaplineRight:
                    return TextAlignmentOptions.CaplineLeft;
                case TextAlignmentOptions.CaplineLeft:
                    return TextAlignmentOptions.CaplineRight;
            }
            return tao;
        }

        public static TextAlignmentOptions RTL(this TextAlignmentOptions tao) {
            switch (tao) {
                case TextAlignmentOptions.Left:
                    return TextAlignmentOptions.Right;

                case TextAlignmentOptions.TopLeft:
                    return TextAlignmentOptions.TopRight;

                case TextAlignmentOptions.BottomLeft:
                    return TextAlignmentOptions.BottomRight;

                case TextAlignmentOptions.MidlineLeft:
                    return TextAlignmentOptions.MidlineRight;

                case TextAlignmentOptions.BaselineLeft:
                    return TextAlignmentOptions.BaselineRight;

                case TextAlignmentOptions.CaplineLeft:
                    return TextAlignmentOptions.CaplineRight;
            }
            return tao;
        }

        public static TextAlignmentOptions LTR(this TextAlignmentOptions tao) {
            switch (tao) {
                case TextAlignmentOptions.Right:
                    return TextAlignmentOptions.Left;

                case TextAlignmentOptions.TopRight:
                    return TextAlignmentOptions.TopLeft;

                case TextAlignmentOptions.BottomRight:
                    return TextAlignmentOptions.BottomLeft;

                case TextAlignmentOptions.MidlineRight:
                    return TextAlignmentOptions.MidlineLeft;

                case TextAlignmentOptions.BaselineRight:
                    return TextAlignmentOptions.BaselineLeft;

                case TextAlignmentOptions.CaplineRight:
                    return TextAlignmentOptions.CaplineLeft;
            }
            return tao;
        }

        public static TextAnchor RTLReverse(this TextAnchor textAnchor) {
            switch (textAnchor) {
                case TextAnchor.LowerLeft:
                    return TextAnchor.LowerRight;
                case TextAnchor.LowerRight:
                    return TextAnchor.LowerLeft;
                case TextAnchor.MiddleLeft:
                    return TextAnchor.MiddleRight;
                case TextAnchor.MiddleRight:
                    return TextAnchor.MiddleLeft;
                case TextAnchor.UpperLeft:
                    return TextAnchor.UpperRight;
                case TextAnchor.UpperRight:
                    return TextAnchor.UpperLeft;
            }
            return textAnchor;
        }

        public static TextAnchor RTL(this TextAnchor textAnchor) {
            switch (textAnchor) {
                case TextAnchor.LowerLeft:
                    return TextAnchor.LowerRight;

                case TextAnchor.MiddleLeft:
                    return TextAnchor.MiddleRight;

                case TextAnchor.UpperLeft:
                    return TextAnchor.UpperRight;

            }
            return textAnchor;
        }

        public static TextAnchor LTR(this TextAnchor textAnchor) {
            switch (textAnchor) {

                case TextAnchor.LowerRight:
                    return TextAnchor.LowerLeft;

                case TextAnchor.MiddleRight:
                    return TextAnchor.MiddleLeft;

                case TextAnchor.UpperRight:
                    return TextAnchor.UpperLeft;

            }
            return textAnchor;
        }
    }
}
