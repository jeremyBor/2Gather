using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Base.StateManagement
{
#if UNITY_EDITOR
    public static class GameStateIdAttributeFieldInfo
    {
        private static List<FieldInfo> fieldInfos = new List<FieldInfo>();

        public static List<FieldInfo> FieldInfos
        {
            get
            {
                if (fieldInfos.Count == 0)
                {
                    OnScriptsReloaded();
                }
                return fieldInfos;
            }
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            fieldInfos = new List<FieldInfo>();

            for (int i = 0; i < assemblies.Length; ++i)
            {
                FieldInfo[] fields = assemblies[i].GetTypes()
                              .SelectMany(t => t.GetFields())
                              .Where(f => f.GetCustomAttributes(typeof(GameStateIdAttribute), false).Length > 0)
                              .ToArray();
                foreach (FieldInfo field in fields)
                {
                    if (field.GetValue(null) is int)
                    {
                        fieldInfos.Add(field);
                    }
                }
            }
        }

        public static string[] GetAllStatesNames()
        {
            List<string> satesNames = new List<string>();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                satesNames.Add(fieldInfo.GetCustomAttribute<GameStateIdAttribute>(false).stateName);
            }
            return satesNames.ToArray();
        }
    }

    [CustomPropertyDrawer(typeof(GameStateIntKey))]
    public class GameStateKeyValueDrawer : PropertyDrawer
    {
        private int selectedValue;
        
        protected void Init(SerializedProperty property)
        {
            for (int i = 0; i < GameStateIdAttributeFieldInfo.FieldInfos.Count; ++i)
            {
                if (property.FindPropertyRelative("intKey").intValue == (int)GameStateIdAttributeFieldInfo.FieldInfos[i].GetValue(null))
                {
                    selectedValue = i;
                    break;
                }
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);

            Rect rect = position;

            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(position, label);
            }
            if (CheckForDuplicateKeys())
            {
                EditorGUI.HelpBox(rect, "There some GameStateIdAttribute who have the same value", MessageType.Error);
            }
            else
            {
                selectedValue = EditorGUI.Popup(rect, selectedValue, GameStateIdAttributeFieldInfo.GetAllStatesNames());

                property.FindPropertyRelative("intKey").intValue = (int)GameStateIdAttributeFieldInfo.FieldInfos[selectedValue].GetValue(null);
            }
        }

        
        private bool CheckForDuplicateKeys()
        {
            List<int> values = new List<int>();
            for (int i = 0; i < GameStateIdAttributeFieldInfo.FieldInfos.Count; ++i)
            {
                int tmp = (int)GameStateIdAttributeFieldInfo.FieldInfos[i].GetValue(null);
                if (values.Contains(tmp))
                {
                    return true;
                }
                else
                {
                    values.Add(tmp);
                }
            }
            return false;
        }
    }
#endif

    [System.Serializable]
    public struct GameStateIntKey
    {
        public int intKey;

        public GameStateIntKey(int i)
        {
            intKey = i;
        }

        public static implicit operator int(GameStateIntKey b)
        {
            return b.intKey;
        }

        public static implicit operator GameStateIntKey(int b)
        {
            return new GameStateIntKey(b);
        }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class GameStateIdAttribute : Attribute
    {
        public string stateName;
    }
}