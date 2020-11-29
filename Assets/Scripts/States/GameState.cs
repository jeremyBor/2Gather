using Base.StateManagement;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Hellmade.Sound;

[CreateAssetMenu(fileName = "GameState.asset", menuName = "GameStates/States/GameState", order = 1)]
public class GameState : AGameState
{
    public AudioClip gameMusic = null;
    public AudioClip menuMusic = null;
    public override void OnEnterTop(IState a_previousState)
    {
        base.OnEnterTop(a_previousState);
        RegisterForEvent();
        
        EazySoundManager.PlayMusic(gameMusic, 1f, true, true);
    }

    public override void OnExitTop(IState a_nextState)
    {
        base.OnExitTop(a_nextState);
        UnRegisterForEvent();
        EazySoundManager.StopAllSounds();
        EazySoundManager.PlayMusic(menuMusic, 1f, true, false);
    }

    private void RegisterForEvent()
    {
        LevelManager.Instance.OnLevelLoaded += RegisterLevelEnd;
        if(LevelManager.Instance.isLevelLoaded)
        {
            RegisterLevelEnd();
        }
    }

    void RegisterLevelEnd()
    {
        LevelManager.Instance.OnLevelLoaded -= RegisterLevelEnd;
        LevelManager.Instance._currentLevel.levelEnd.OnLevelEnd += OnLevelEnd;
    }

    private void UnRegisterForEvent()
    {
        LevelManager.Instance._currentLevel.levelEnd.OnLevelEnd -= OnLevelEnd;
        
    }

    void OnLevelEnd ()
    {
        PushGameState(GameSpecificGameStateIdConsts.Dialogue);
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        LevelManager.Instance._currentLevel.DoUpdate();
    }

    public override void DoFixedUpdate()
    {
        base.DoFixedUpdate();
        LevelManager.Instance._playerInstance.DoFixedUpdate();
    }

    private void OnPauseButtonPressed(ButtonEvent a_buttonEvent)
    {
       // if(a_buttonEvent.ButtonDown)
        //PushGameState(GenericGameStateConsts.Pause);
    }
}
