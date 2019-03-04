using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event { }

public class EnemyDied :Event
{
    public int EnemyValue;
    public EnemyDied(int value)
    {
        EnemyValue = value;
    }
}

public class GameStateChanged : Event
{
    public GameState State;
    public GameStateChanged(GameState state)
    {
        State = state;
    }
}

