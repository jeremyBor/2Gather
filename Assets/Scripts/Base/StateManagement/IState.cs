using System;
using System.Threading.Tasks;

namespace Base.StateManagement
{
    public interface IState
    {
        void OnPushed();
        void OnEnterTop(IState a_previousState);
        void DoUpdate();
        void DoLateUpdate();
        void DoFixedUpdate();
        void OnExitTop(IState a_nextState);
        void OnPopCalled(Action a_onActionFinished);
        void OnPushOverCalled(Action a_onActionFinished);
    }
}