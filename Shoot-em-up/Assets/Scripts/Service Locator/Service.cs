using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Service
{
    public static EventManager GameEventManager = new EventManager();
    public static GameStateManager StateManager = new GameStateManager(GameState.Playing);

}
