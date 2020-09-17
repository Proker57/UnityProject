using UnityEngine;
using UnityEditor;

namespace NoSuchStudio.Common.Editor {
    public class AboutWindow : EditorWindow {

        public const string DocumentatoinURL = "https://nosuchstudio.com/nosuchlocalization/";
        public const string SupportForumURL = "https://forum.unity.com/threads/no-such-localization-asset-forum.880888/";
        public const string DiscordURL = "https://discord.gg/bd5As5n";
        public const string StoreLinkProURL = "http://u3d.as/1Lav";
        public const string StoreLinkLiteURL = "http://u3d.as/1Ky2";
        public const string Version = "1.4";
        public const string ProLite = "Lite";

        GUIContent guiThankyou;
        GUIContent guiReview;
        GUIContent guiVersion;

        GUIStyle styleLabel;
        GUIStyle styleThankyou;
        GUIStyle styleLink;
        GUIStyle styleVersion;

        [MenuItem("No Such Studio/Localization/About")]
        public static void AboutClicked() {
            ShowWindow();
        }

        protected static void ShowWindow() {
            var about = CreateInstance<AboutWindow>();
            about.ShowUtility();
            about.titleContent = new GUIContent("About \"No Such Localization\"");
            about.PrepareContent();
        }

        private void PrepareContent() {
            styleLabel = new GUIStyle(EditorStyles.label) {
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };

            guiThankyou = new GUIContent("Thank you for using <b>No Such Localization</b>!");
            styleThankyou = new GUIStyle(EditorStyles.label) {
                alignment = TextAnchor.MiddleCenter,
                richText = true,
                fontSize = 16
            };

            styleLink = new GUIStyle(EditorStyles.linkLabel) {
                alignment = TextAnchor.MiddleCenter,
                stretchWidth = true,
                richText = true
            };

            guiReview = EditorGUIUtility.IconContent("Favorite Icon", "Leave us a review.");
            guiReview.text = "Leave us a review.";

            guiVersion = new GUIContent($"You are using <b>{ProLite}</b> <color=green>v{Version}</color>");
            styleVersion = new GUIStyle(EditorStyles.label) {
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };
        }

        void OnGUI() {
            minSize = maxSize = new Vector2(350, 350);
            // Thankyou Note
            // Version note
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(guiThankyou, styleThankyou);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(guiVersion, styleVersion);
            EditorGUILayout.Space(30);
            // Documentation
            EditorGUILayout.LabelField("<b>Learn how to use the asset.</b>\nManual, tutorial videos and API documentation.", styleLabel, GUILayout.Height(30));
            if (GUILayout.Button("Learn", styleLink)) {
                Application.OpenURL(DocumentatoinURL);
            }
            EditorGUILayout.Space(10);
            // Support Forum
            EditorGUILayout.LabelField("<b>Need help? We got your back!</b>\nSometimes the documentation doesn't have the answer.", styleLabel, GUILayout.Height(30));
            if (GUILayout.Button("Support Forum", styleLink)) {
                Application.OpenURL(SupportForumURL);
            }
            EditorGUILayout.Space(10);
            // Discord
            EditorGUILayout.LabelField("<b>Join the community.</b>\nTalk to the developers and other members of the community.", styleLabel, GUILayout.Height(30));
            if (GUILayout.Button("Discord", styleLink)) {
                Application.OpenURL(DiscordURL);
            }
            EditorGUILayout.Space(10);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(guiReview, styleLink, GUILayout.Height(30))) {
                Application.OpenURL(StoreLinkLiteURL);
            }

        }

    }
}