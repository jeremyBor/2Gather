using Base.StateManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "IntroState.asset", menuName = "GameStates/States/IntroState", order = 1)]
public class IntroState : AGameState
{
    public List<string> introKeys;
    public override void OnEnterTop(IState a_previousState)
    {
        base.OnEnterTop(a_previousState);

        WinPanel wp = UIManager.Instance.ShowPanel<WinPanel>();
        wp.SetDialoges(introKeys);
        RegisterForEvent();
    }

    public override void OnExitTop(IState a_nextState)
    {
        base.OnExitTop(a_nextState);
        UnRegisterForEvent();
        UIManager.Instance.HidePanel<WinPanel>();
    }

    private void RegisterForEvent()
    {
        ButtonEvents.instance.AddListener(OnStartButtonPressed, EGenericButtonEvent.Interact);
    }

    private void UnRegisterForEvent()
    {
        ButtonEvents.instance.RemoveListener(OnStartButtonPressed, EGenericButtonEvent.Interact);
    }

    private void OnStartButtonPressed(ButtonEvent a_buttonEvent)
    {
        if (a_buttonEvent.ButtonDown)
            PushGameState(GenericGameStateConsts.Game);
    }
}
