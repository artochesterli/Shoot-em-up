using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Playing,
    Over
}

public class GameStateManager : MonoBehaviour
{

    public static GameState CurrentState=GameState.Playing;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.AddHandler<GameStateChanged>(OnGameStateChanged);
    }

    private void OnDestroy()
    {
        EventManager.instance.RemoveHandler<GameStateChanged>(OnGameStateChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == GameState.Over && Input.GetKeyDown(KeyCode.R))
        {
            ChangeGameState(GameState.Playing);
        }
    }

    public static void ChangeGameState(GameState state)
    {
        CurrentState = state;
        EventManager.instance.Fire(new GameStateChanged(CurrentState));
    }

    private void OnGameStateChanged(GameStateChanged change)
    {
        if (change.State == GameState.Playing)
        {
            GameObject ob=(GameObject) Instantiate(Resources.Load("Prefabs/Avatar"), Vector3.zero, new Quaternion(0, 0, 0, 0));
            Main.Avatar = ob;
        }
    }
}
