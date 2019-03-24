using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemyTask : Task
{
    private GameObject ob;
    private int level;
    private int number;
    private float interval;

    private float timecount;

    public GenerateEnemyTask(GameObject o,int L, int n , float In)
    {
        ob = o;
        level = L;
        number = n;
        interval = In;
    }

    protected override void Init()
    {
        timecount = 0;
    }

    internal override void TaskUpdate()
    {
        timecount += Time.deltaTime;
        if (timecount >= interval)
        {
            timecount = 0;
            for (int i = 0; i < number; i++)
            {
                GameObject enemy= (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/enemy" + level.ToString()), ob.transform.position, new Quaternion(0, 0, 0, 0));
                EventManager.instance.Fire(new EnemyGenerated(enemy));
            }
        }
    }
}
