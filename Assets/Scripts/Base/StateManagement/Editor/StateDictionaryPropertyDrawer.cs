using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
namespace Base.StateManagement
{
    [CustomEditor(typeof(GameStateContener))]
    public class StateDictionaryPropertyDrawer : Editor
    {
        bool gameSatesVisible = true;
        GameStateContener gameStateContener;

        protected void OnEnable()
        {
            gameStateContener = target as GameStateContener;
        }
        
        public override void OnInspectorGUI()
        {
            SerializedProperty gameStateProperty = serializedObject.FindProperty("_gameStates");
            serializedObject.Update();
            gameStateProperty.arraySize = GameStateIdAttributeFieldInfo.FieldInfos.Count;
            string[] gameStatesNames = GameStateIdAttributeFieldInfo.GetAllStatesNames();


            GUILayout.BeginVertical(GUI.skin.button);
            GUILayout.Label("Game Sates");

            for (int i = 0; i < gameStatesNames.Length; ++i)
            {
                gameStateProperty.GetArrayElementAtIndex(i).FindPropertyRelative("Key").intValue =
                    (int)GameStateIdAttributeFieldInfo.FieldInfos[i].GetValue(null);
                SerializedProperty stateValue = gameStateProperty.GetArrayElementAtIndex(i).FindPropertyRelative("Value");
                GUIContent guiLabel = new GUIContent(gameStatesNames[i]);
                EditorGUILayout.PropertyField(stateValue, guiLabel);
            }
            GUILayout.EndVertical();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_startStateId"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif