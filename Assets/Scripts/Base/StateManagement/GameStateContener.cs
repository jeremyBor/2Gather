using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.StateManagement
{
    [CreateAssetMenu(fileName = "GameStateContener.asset", menuName = "GameStates/GameStateContener", order = 1)]
    public class GameStateContener : ScriptableObject
    {
        [SerializeField]
        private List<GameSateDictionaryField> _gameStates;
        
        [SerializeField]
        private GameStateIntKey _startStateId;
        public int StartStateId => _startStateId.intKey;

        /// <summary>
        /// This function return a AGameState present in the GameStateContener Dictionary
        /// </summary>
        /// <param name="_stateId">The id of the desired state</param>
        /// <returns>the AGameState requested</returns>
        public AGameState GetGameState(int _stateId)
        {
            AGameState returnValue = null;

            for (int i = 0; i < _gameStates.Count; ++i)
            {
                if (_gameStates[i].Key == _stateId)
                    return _gameStates[i].Value;
            }

            if (returnValue == null)
            {
                Debug.LogError("State wit Id " + _stateId + " isn't in the gameState Dictionary");
            }
            return returnValue;
        }

        public void InitStates()
        {
            foreach (GameSateDictionaryField state in _gameStates)
            {
                state.Value.Init();
            }
        }
    }

    [System.Serializable]
    public struct GameSateDictionaryField
    {
        public int Key;
        public AGameState Value;

        public GameSateDictionaryField(int a_key, AGameState a_value)
        {
            Key = a_key;
            Value = a_value;
        }
    }

    [Serializable]
    public class GameStateContenerAssetReference : ScriptableObjectReference<GameStateContener>
    {
        public GameStateContenerAssetReference(string guid) : base(guid)
        {
        }
    }
}