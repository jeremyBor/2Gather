using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Base.StateManagement
{
    public abstract class AGameState : ScriptableObject, IState
    {
        protected GameStateManager _gameStateManagerInstance;

        /// <summary>
        /// This function is called at the Start of the appliction by the GameManager
        /// just after the have been loaded
        /// </summary>
        public virtual void Init()
        {
            _gameStateManagerInstance = GameStateManager.Instance;
        }

        /// <summary>
        /// Function called every frame when the state is on top of the StateMachine stack.
        /// </summary>
        public virtual void DoUpdate() { }

        public virtual void DoFixedUpdate() { }

        public virtual void DoLateUpdate() { }

        /// <summary>
        /// Function called when the state become the top state in the StateMachine stack.
        /// </summary>
        /// <param name="a_previousState">The previous state refferance</param>
        public virtual void OnEnterTop(IState a_previousState) { }

        /// <summary>
        /// Function called when the state is Popped out of the StateMachine stack or
        /// when an other state is pushed over this one.
        /// </summary>
        /// <param name="a_nextState">The next state refferance</param>
        public virtual void OnExitTop(IState a_nextState) { }

        /// <summary>
        /// Function called just before this state is popped out the StateMachine stack.
        /// It's the last chanse the do something. 
        /// The state machine will wait that this asyncone function is finished before pop this state.
        /// </summary>
        /// <returns>The task to wait</returns>
        public virtual void OnPopCalled(Action a_onActionFinished)
        {
            a_onActionFinished?.Invoke();
        }

        /// <summary>
        /// Function called just before this state an other state is pushed over this one.
        /// The state machine will wait that this asyncone function is finished before push the new state.
        /// </summary>
        /// <returns>the task to wait</returns>
        public virtual void OnPushOverCalled(Action a_onActionFinished)
        {
            a_onActionFinished?.Invoke();
        }

        public virtual void OnPushed() { }

        public virtual void PushGameState(int a_gameStateId)
        {
            _gameStateManagerInstance.PushGameState(a_gameStateId);
        }

        public virtual void PopGameState()
        {
            _gameStateManagerInstance.PopGameState();
        }
    }
}