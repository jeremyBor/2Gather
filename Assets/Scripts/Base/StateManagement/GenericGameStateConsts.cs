using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.StateManagement
{
    public static class GenericGameStateConsts
    {
        [GameStateId(stateName = "Menu")]
        public const int Menu = 101;
        [GameStateId(stateName = "Game")]
        public const int Game = 102;

    }
}