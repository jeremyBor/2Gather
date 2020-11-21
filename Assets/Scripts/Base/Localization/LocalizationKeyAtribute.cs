using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Base.Loacalization
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(LocalizationKeyAtribute))]
    public class LeanPhraseNameDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect left = position;
            left.xMax -= 40;

            Rect right = position;
            right.xMin = left.xMax + 2;

            EditorGUI.PropertyField(left, property);

            if (GUI.Button(right, "List") == true)
            {
                GenericMenu menu = new GenericMenu();

                for (var i = 0; i < Localization.Instance.GetKeys().Count; i++)
                {
                    string key = Localization.Instance.GetKeys()[i];

                    if (property.stringValue != string.Empty)
                    {
                        if (key.ToUpper().Contains(property.stringValue.ToUpper()))
                        {
                            menu.AddItem(new GUIContent(key), property.stringValue == key, () =>
                            {
                                Debug.Log(property.serializedObject.isEditingMultipleObjects);
                                property.stringValue = key;
                                property.serializedObject.ApplyModifiedProperties();
                            });
                        }
                    }
                    else
                    {
                        menu.AddItem(new GUIContent(key), property.stringValue == key, () =>
                        {
                            property.stringValue = key;
                            property.serializedObject.ApplyModifiedProperties();
                        });
                    }
                }

                if (menu.GetItemCount() > 0)
                {
                    menu.DropDown(right);
                }
            }
        }
    }
#endif

    public class LocalizationKeyAtribute : PropertyAttribute
    {
    }
}