using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Playing,
    Over
}

public class GameStateManager
{
    public GameState CurrentState;
    public GameStateManager(GameState G)
    {
        CurrentState = G;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Service.GameEventManager.AddHandler<GameStateChanged>(OnGameStateChanged);
        //EventManager.instance.AddHandler<GameStateChanged>(OnGameStateChanged);
    }

    private void OnDestroy()
    {
        Service.GameEventManager.RemoveHandler<GameStateChanged>(OnGameStateChanged);
        //EventManager.instance.RemoveHandler<GameStateChanged>(OnGameStateChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == GameState.Over && Input.GetKeyDown(KeyCode.R))
        {
            ChangeGameState(GameState.Playing);
        }
    }

    public void ChangeGameState(GameState state)
    {
        CurrentState = state;
        Service.GameEventManager.Fire(new GameStateChanged(CurrentState));
        //EventManager.instance.Fire(new GameStateChanged(CurrentState));
    }

    private void OnGameStateChanged(GameStateChanged change)
    {
        if (change.State == GameState.Playing)
        {
            GameObject ob=(GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Avatar"), Vector3.zero, new Quaternion(0, 0, 0, 0));
            Main.Avatar = ob;
        }
    }
}
