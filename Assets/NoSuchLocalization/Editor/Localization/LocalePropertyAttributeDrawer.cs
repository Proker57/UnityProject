using UnityEditor;
using UnityEngine;

namespace NoSuchStudio.Localization.Editor {
    [CustomPropertyDrawer(typeof(LocalePropertyAttribute))]
    public class LocalePropertyAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var localeAttr = attribute as LocalePropertyAttribute;
            bool isStringProperty = property.propertyType == SerializedPropertyType.String;
            bool isGenericProperty = property.propertyType == SerializedPropertyType.Generic;
            if (!(isStringProperty || isGenericProperty)) {
                EditorGUI.LabelField(position, property.displayName, "LocaleAttribute can only be used with Locale and string fields.");
            } else {
                LocalePropertyDrawer.DrawLocaleField(
                    localeAttr.filterToAvailable,
                    position,
                    property,
                    label,
                    localeAttr.drawMode
                    );
            }
        }
    }
}