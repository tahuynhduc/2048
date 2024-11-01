using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
   
    public static Action IsEndGame = delegate { };
    public static Action StartGame = delegate { };
    public static Action SetUpGameplay = delegate { };
    
    public static void OnSetupGameplay()
    {
        SetUpGameplay.Invoke();
    }
    public static void OnEndGame()
    {
        IsEndGame.Invoke();
    }
    
    public static void OnStartGame()
    {
        StartGame.Invoke();
    }
}