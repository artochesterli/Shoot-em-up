using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_Manager : MonoBehaviour
{

    public static GameObject EnemyManager;
    public int Wave_Enemy_Number_Low;
    public int Wave_Enemy_Number_High;
    public float Wave_Interval;

    private List<GameObject> Enemy_list=new List<GameObject>();
    private List<int> Enemy_border_count=new List<int>();
    private List<string> Enemy_name_list = new List<string>();
    private float Interval_time_count;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.AddHandler<GameStateChanged>(OnGameStateChanged);
        EnemyManager = gameObject;
        Enemy_name_list.Add("enemy1");
        Enemy_name_list.Add("enemy2");
        Enemy_name_list.Add("enemy3");
        for(int i = 0; i < 4; i++)
        {
            Enemy_border_count.Add(0);
        }
        Interval_time_count = 0;
        StartCoroutine(Generate_Enemy());
    }


    private void OnDestroy()
    {
        EventManager.instance.RemoveHandler<GameStateChanged>(OnGameStateChanged);
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator Generate_Enemy()
    {
        while (true)
        {
            if (Enemy_list.Count == 0)
            {
                
                Interval_time_count = 0;
                Generate_Enemy_Wave();
            }
            if (Interval_time_count > Wave_Interval)
            {
                Interval_time_count = 0;
                Generate_Enemy_Wave();
            }
            Interval_time_count += Time.deltaTime;
            yield return null;
        }
    }

    public void Destroy_Enemy(int index)
    {
        Destroy(Enemy_list[index].gameObject);
        Enemy_list.RemoveAt(index);
        for(int i = index; i < Enemy_list.Count; i++)
        {
            Enemy_list[i].GetComponent<Enemy>().index--;
        }
    }

    private void Create_Enemy(string name,Vector3 pos)
    {
        GameObject enemy= (GameObject)Instantiate(Resources.Load("Prefabs/" + name), pos, new Quaternion(0, 0, 0, 0));
        Enemy_list.Add(enemy);
        enemy.GetComponent<Enemy>().index = Enemy_list.Count - 1;
    }



    private void Generate_Enemy_Wave()
    {
        if (GameStateManager.CurrentState == GameState.Playing)
        {
            for (int i = 0; i < Enemy_border_count.Count; i++)
            {
                Enemy_border_count[i] = 0;
            }
            int enemy_number = UnityEngine.Random.Range(Wave_Enemy_Number_Low, Wave_Enemy_Number_High + 1);
            for (int i = 0; i < enemy_number; i++)
            {
                Vector3 pos = Calculate_next_pos();
                string name = Enemy_name_list[UnityEngine.Random.Range(0, Enemy_name_list.Count)];
                Create_Enemy(name, pos);
            }
        }
    }

    private Vector3 Calculate_next_pos()
    {
        int border = select_next_border();
        float bound_y = Camera.main.orthographicSize;
        float bound_x = Camera.main.orthographicSize * Camera.main.pixelWidth / Camera.main.pixelHeight;

        Vector3 pos=new Vector3(0,0,0);
        if (border == 0)
        {
            pos = new Vector3(bound_x, UnityEngine.Random.Range(-bound_y, bound_y), 0);
        }
        else if (border == 1)
        {
            pos = new Vector3(UnityEngine.Random.Range(-bound_x, bound_x), bound_y, 0);
        }
        else if (border == 2)
        {
            pos = new Vector3(-bound_x, UnityEngine.Random.Range(-bound_y, bound_y), 0);
        }
        else
        {
            pos = new Vector3(UnityEngine.Random.Range(-bound_x, -bound_x), bound_y, 0);
        }
        Enemy_border_count[border]++;
        return pos;
    }

    private int select_next_border()
    {
        List<int> min_border = new List<int>();
        int min = Int32.MaxValue;
        for(int i = 0; i < Enemy_border_count.Count; i++)
        {
            if (Enemy_border_count[i] < min)
            {
                min = Enemy_border_count[i];
            }
        }
        for (int i = 0; i < Enemy_border_count.Count; i++)
        {
            if (Enemy_border_count[i] == min)
            {
                min_border.Add(i);
            }
        }
        int index = UnityEngine.Random.Range(0, min_border.Count);
        return min_border[index];
    }

    private void OnGameStateChanged(GameStateChanged change)
    {
        if (change.State == GameState.Playing)
        {
            for(int i = 0; i < Enemy_list.Count; i++)
            {
                Destroy(Enemy_list[i]);
            }
        }
    }
}
