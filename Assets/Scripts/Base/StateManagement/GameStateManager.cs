using Base.StateManagement;
using Base.Tools;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Base
{
    public class GameStateManager : MonoBehaviourSingleton<GameStateManager>
    {
        private GameStateManager() : base(true) { }

        private void Awake()
        {
            InitStateMachine();
        }
        
        private void Update()
        {
            _gameStateMachine.DoUpdate();
        }

        private void LateUpdate()
        {
            _gameStateMachine.DoLateUpdate();
        }

        private void FixedUpdate()
        {
            _gameStateMachine.DoFixedUpdate();
        }

        #region StateManagement
        [Header("StateManagement")]
        private StackStateMachine _gameStateMachine;
        public StackStateMachine GameStateMashine => _gameStateMachine;

        [SerializeField]
        private GameStateContener _gameStateContener;
        public GameStateContener GameStateContener => _gameStateContener;

        private void InitStateMachine()
        {
            _gameStateMachine = new StackStateMachine();
            _gameStateMachine.Init();
            _gameStateContener.InitStates();
            _gameStateMachine.PushState(_gameStateContener.GetGameState(_gameStateContener.StartStateId));
        }

        public void PushGameState(int a_gameStateId)
        {
            AGameState sate = GameStateContener.GetGameState(a_gameStateId);
            GameStateMashine.PushState(sate);
        }

        public void PopGameState()
        {
            GameStateMashine.PopState();
        }

        public void PopToGameState(int a_gameStateId)
        {
            AGameState sate = GameStateContener.GetGameState(a_gameStateId);
            GameStateMashine.PopToState(sate);
        }

        public bool IsStackContainGameState<T>(T a_gameState) where T:IState
        {
            return GameStateMashine.ContainState(a_gameState);
        }

        public AGameState GetGameState(int a_gameStateId) 
        {
            return GameStateContener.GetGameState(a_gameStateId);
        }
        #endregion
    }

    #region Editor debugger
#if UNITY_EDITOR
    [CustomEditor(typeof(GameStateManager))]
    public class StateMachineEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameStateManager stateManager = target as GameStateManager;
            
            if (EditorApplication.isPlaying)
            {
                GUILayout.BeginVertical(GUI.skin.button);
                EditorGUILayout.LabelField("States Stack", EditorStyles.boldLabel);

                IState[] states = stateManager.GameStateMashine.ToArray();

                for (int i = states.Length-1; i >= 0; --i)
                {
                    string labelString = states[i].GetType().ToString();
                    if (i == 0)
                    {
                        labelString += " (current)";
                    }
                    EditorGUILayout.LabelField(labelString);
                }
                GUILayout.EndVertical();
            }
            
        }
    }
#endif
    #endregion
}