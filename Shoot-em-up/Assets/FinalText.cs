using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalText : MonoBehaviour
{

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
        updatescore();
    }

    private void updatescore()
    {
        GetComponent<Text>().text = "TotalScore: " +  ScoreText.score.ToString()+"\r\nPress R to restart";
    }

    private void OnGameStateChanged(GameStateChanged change)
    {
        if (change.State == GameState.Over)
        {
            GetComponent<Text>().enabled = true;
        }
        else
        {
            GetComponent<Text>().enabled = false;
        }
    }
}
