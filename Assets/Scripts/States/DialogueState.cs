using Base.StateManagement;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "DialogueState.asset", menuName = "GameStates/States/DialogueState", order = 1)]
public class DialogueState : AGameState
{
    public override void OnEnterTop(IState a_previousState)
    {
        base.OnEnterTop(a_previousState);

        WinPanel wp = UIManager.Instance.ShowPanel<WinPanel>();
        wp.SetDialoges(LevelManager.Instance._currentLevel._keys);
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
            PushGameState(GameSpecificGameStateIdConsts.Score);
    }
}
