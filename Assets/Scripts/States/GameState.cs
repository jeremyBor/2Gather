using Base.StateManagement;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "GameState.asset", menuName = "GameStates/States/GameState", order = 1)]
public class GameState : AGameState
{
    public override void OnEnterTop(IState a_previousState)
    {
        base.OnEnterTop(a_previousState);
    }

    public override void OnExitTop(IState a_nextState)
    {
        base.OnExitTop(a_nextState);
    }

    private void RegisterForEvent()
    {
        Level.Instance.levelEnd.OnLevelEnd += OnLevelEnd;
    }

    private void UnRegisterForEvent()
    {
        Level.Instance.levelEnd.OnLevelEnd -= OnLevelEnd;
    }

    void OnLevelEnd ()
    {

    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        Level.Instance.DoUpdate();
    }

    public override void DoFixedUpdate()
    {
        base.DoFixedUpdate();
        Level.Instance._playerInstance.DoFixedUpdate();
    }

    private void OnPauseButtonPressed(ButtonEvent a_buttonEvent)
    {
       // if(a_buttonEvent.ButtonDown)
        //PushGameState(GenericGameStateConsts.Pause);
    }
}
