﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public static int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        Service.GameEventManager.AddHandler<EnemyDied>(OnEnemyDied);
        Service.GameEventManager.AddHandler<GameStateChanged>(OnGameStateChanged);

        //EventManager.instance.AddHandler<EnemyDied>(OnEnemyDied);
        //EventManager.instance.AddHandler<GameStateChanged>(OnGameStateChanged);
    }

    private void OnDestroy()
    {
        Service.GameEventManager.RemoveHandler<EnemyDied>(OnEnemyDied);
        Service.GameEventManager.RemoveHandler<GameStateChanged>(OnGameStateChanged);

        //EventManager.instance.RemoveHandler<EnemyDied>(OnEnemyDied);
        //EventManager.instance.RemoveHandler<GameStateChanged>(OnGameStateChanged);
    }

    // Update is called once per frame
    void Update()
    {
        updatescore();
    }

    private void updatescore()
    {
        GetComponent<Text>().text = "Score: " + score.ToString();
    }

    private void OnEnemyDied(EnemyDied e)
    {
        if (Service.StateManager.CurrentState == GameState.Playing)
        {
            score += e.EnemyValue;
        }
    }

    private void OnGameStateChanged(GameStateChanged change)
    {
        if (change.State == GameState.Over)
        {
            GetComponent<Text>().enabled = false;
        }
        else
        {
            GetComponent<Text>().enabled = true;
            score = 0;
        }
    }
}
