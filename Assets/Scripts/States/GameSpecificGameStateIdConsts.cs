using Base.StateManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSpecificGameStateIdConsts
{
    [GameStateId(stateName = "Dialogue")]
    public const int Dialogue = 1;
    [GameStateId(stateName = "Score")]
    public const int Score = 2;
    [GameStateId(stateName = "Intro")]
    public const int Intro = 2;
}