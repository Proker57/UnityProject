using System;
using System.Collections.Generic;

using UnityEngine;

namespace NoSuchStudio.Common {
    /// <summary>
    /// Helper functions.
    /// </summary>
    public static class Helpers {
        /// <summary>
        /// Whether we are in edit mode.
        /// </summary>
        public static bool IsEditMode {
            get { return (Application.isEditor && !Application.isPlaying); }
        }

        /// <summary>
        /// Detect device type based on screen size.
        /// </summary>
        /// <returns>Returns true if the screen size is more than 6 inches in diameter.</returns>
        public static bool IsTablet() {
            // Compute screen size
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            double size = Mathf.Sqrt(screenWidth * screenWidth + screenHeight * screenHeight);
            // Tablet devices should have a screen size greater than 6 inches
            return size >= 6;
        }

        /// <summary>
        /// Select a random element from a list.
        /// </summary>
        /// <typeparam name="T">Type of array.</typeparam>
        /// <param name="list">List to random select from.</param>
        /// <returns></returns>
        public static T Random<T>(this List<T> list) {
            if (list.Count == 0) return default;

            int i = UnityEngine.Random.Range(0, list.Count);
            return list[i];
        }

        /// <summary>
        /// return c unique random integers in range [min, max).
        /// </summary>
        /// <param name="c">number of unique random numbers to generate.</param>
        /// <param name="min">min value of generated numbers (inclusive).</param>
        /// <param name="max">max value of generated numbers (exclusive).</param>
        /// <returns>A list of c random numbers in range [min, max).</returns>
        /// <exception cref="ApplicationException">thrown when c is larger than half of the specified range for random numbers.</exception>
        public static List<int> UniqueRandom(int c, int min, int max) {
            if (c >= (max - min - 1) / 2) {
                throw new ApplicationException(string.Format("UniqueRandom inefficient for c: {0}, max: {1}", c, max));
            }
            List<int> ret = new List<int>(c);
            HashSet<int> curSet = new HashSet<int>();
            while (ret.Count < c) {
                int rand = UnityEngine.Random.Range(min, max);
                if (!curSet.Contains(rand)) {
                    curSet.Add(rand);
                    ret.Add(rand);
                }
            }
            return ret;
        }
    }
}
