using Base.StateManagement;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Hellmade.Sound;

[CreateAssetMenu(fileName = "ScoreState.asset", menuName = "GameStates/States/ScoreState", order = 1)]
public class ScoreState : AGameState
{
    public AudioClip menuMusic = null;
    public override void OnEnterTop(IState a_previousState)
    {
        base.OnEnterTop(a_previousState);

        UIManager.Instance.ShowPanel<TransitionPanel>();
        RegisterForEvent();
        EazySoundManager.PlayMusic(menuMusic, 1f, true, false);
    }

    public override void OnExitTop(IState a_nextState)
    {
        base.OnExitTop(a_nextState);
        UnRegisterForEvent();
        UIManager.Instance.HidePanel<TransitionPanel>();
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
        {
            LevelManager.Instance.NextLevel();
            PushGameState(GenericGameStateConsts.Game);
        }
    }
}
