﻿using Base.StateManagement;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Hellmade.Sound;

[CreateAssetMenu(fileName = "MenuState.asset", menuName = "GameStates/States/MenuState", order = 1)]
public class MenuState : AGameState
{
    public AudioClip menuMusic = null;
    public override void OnEnterTop(IState a_previousState)
    {
        base.OnEnterTop(a_previousState);

        UIManager.Instance.ShowPanel<MainMenuPanel>();
        RegisterForEvent();

        EazySoundManager.PlayMusic(menuMusic, 1f, true, false);
    }

    public override void OnExitTop(IState a_nextState)
    {
        base.OnExitTop(a_nextState);
        UnRegisterForEvent();
        UIManager.Instance.HidePanel<MainMenuPanel>();
    }

    private void RegisterForEvent()
    {
        ButtonEvents.instance.AddListener(OnStartButtonPressed, EGenericButtonEvent.Start);
    }

    private void UnRegisterForEvent()
    {
        ButtonEvents.instance.RemoveListener(OnStartButtonPressed, EGenericButtonEvent.Start);
    }

    private void OnStartButtonPressed(ButtonEvent a_buttonEvent)
    {
        if(a_buttonEvent.ButtonDown)
        PushGameState(GameSpecificGameStateIdConsts.Intro);
    }
}
