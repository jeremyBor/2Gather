using UnityEngine;
using UnityEditor;

namespace Base.Loacalization
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(AllFonts))]
    public class FontLinkerParameterInspector : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("basesFontsRef"), true);

            Rect rect = new Rect(position.x,
                position.y + EditorGUI.GetPropertyHeight(property.FindPropertyRelative("basesFontsRef")), position.width,
                position.height);

            for (int i = 0; i < property.FindPropertyRelative("languagueFonts").arraySize; ++i)
            {
                property.FindPropertyRelative("languagueFonts").GetArrayElementAtIndex(i)
                    .FindPropertyRelative("replaceFontRef").arraySize = property.FindPropertyRelative("basesFontsRef").arraySize;
            }

            EditorGUI.PropertyField(rect, property.FindPropertyRelative("languagueFonts"), true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("basesFontsRef")) +
                   EditorGUI.GetPropertyHeight(property.FindPropertyRelative("languagueFonts"));
        }
    }
#endif
}