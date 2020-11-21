using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Base.StateManagement
{
    public class StackStateMachine
    {
        private Stack<IState> _states;
        private bool _isASateActive;

#if UNITY_EDITOR
        public IState[] ToArray() => _states.ToArray();
#endif

        #region Public functions
        public void Init()
        {
            _isASateActive = false;
            _states = new Stack<IState>();
        }

        /// <summary>
        /// Function called once per frame
        /// </summary>
        public void DoUpdate()
        {
            if (_isASateActive)
            {
                _states.Peek().DoUpdate();
            }
        }

        public void DoLateUpdate()
        {
            if (_isASateActive)
            {
                _states.Peek().DoLateUpdate();
            }
        }

        public void DoFixedUpdate()
        {
            if (_isASateActive)
            {
                _states.Peek().DoFixedUpdate();
            }
        }

        public bool ContainState(IState a_state)
        {
            return _states.Contains(a_state);
        }

        public IState CurrentSate
        {
            get
            {
                return _states.Count>0 ? _states.Peek() : null;
            }
        }

        public T GetStackedState<T>() where T:IState
        {
            foreach(IState state in _states)
            {
                if (state is T)
                {
                    return (T)state;
                }
            }
            Debug.LogWarning("this type of state isn't in the stack");
            return default(T);
        }

        /// <summary>
        /// Pop the top state of the stack after giving him a last chance to do somthing 
        /// </summary>
        public void PopState()
        {
            if (_states.Count > 0)
            {
                var sateEnum = _states.GetEnumerator();
                sateEnum.MoveNext();
                IState oldState = sateEnum.Current;
                sateEnum.MoveNext();
                IState nextState = sateEnum.Current;
                ExitTopState(oldState, nextState);

                oldState.OnPopCalled(() => {
                    _states.Pop();

                    EnterTopState(nextState, oldState);
                });
               
            }
        }

        /// <summary>
        /// Push a new state on top of the stack after giving a chanse to the previous ont to do something
        /// </summary>
        /// <param name="a_state"></param>
        public void PushState(IState a_state)
        {
            IState oldState = null;
            if (_states.Count > 0)
            {
                oldState = _states.Peek();
                ExitTopState(oldState, a_state);
                oldState.OnPushOverCalled(() =>
                {
                    _states.Push(a_state);

                    a_state.OnPushed();

                    EnterTopState(a_state, oldState);
                });
            }
            else
            {
                _states.Push(a_state);

                a_state.OnPushed();

                EnterTopState(a_state, oldState);
            }
        }

        /// <summary>
        /// Recursivli pop all state until the top sate is the given state
        /// </summary>
        /// <param name="a_state"></param>
        public void PopToState(IState a_state)
        {
            if (ContainState(a_state))
            {
                IState oldState = _states.Peek();
                ExitTopState(oldState, a_state);
                _states.Peek().OnPopCalled(() => RecursivePop(a_state, oldState));
            }
            else
            {
                Debug.LogError("Can't recursily pop to a state who isn't in the satck");
            }
        }
        #endregion

        #region Privates functions
        private void EnterTopState(IState a_state, IState a_previousSate)
        {
            _isASateActive = true;
            a_state.OnEnterTop(a_previousSate);
        }

        private void ExitTopState(IState a_state, IState a_nextSate)
        {
            _isASateActive = false;
            a_state.OnExitTop(a_nextSate);
        }

        private void RecursivePop(IState a_targetState, IState a_oldState)
        {
            _states.Pop();
            if (_states.Peek() == a_targetState)
            {
                EnterTopState(a_targetState, a_oldState);
            }
            else
            {
                _states.Peek().OnPopCalled(() => RecursivePop(a_targetState, a_oldState));
            }
        }
        #endregion
    }
}