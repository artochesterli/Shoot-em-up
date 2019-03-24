using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBulletTask : Task
{
    private GameObject ob;
    private bool chase;
    private float interval;

    private float timecount;
    public ShootBulletTask(GameObject o,bool c,float i)
    {
        ob = o;
        chase = c;
        interval = i;
    }

    protected override void Init()
    {
        timecount = 0;
    }

    internal override void TaskUpdate()
    {
        if (Main.Avatar == null)
        {
            SetStatus(TaskStatus.Success);
        }
        timecount += Time.deltaTime;
        if (timecount > interval)
        {
            timecount = 0;
            GameObject bullet= (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Bullet_Enemy"), ob.transform.position, new Quaternion(0, 0, 0, 0));
            Vector3 offset = Main.Avatar.transform.position - ob.transform.position;
            offset.Normalize();
            bullet.GetComponent<Speed>().direction = offset;
            if (chase)
            {
                bullet.GetComponent<Chase_Turn_Angle>().angle = 30;
            }
            else
            {
                bullet.GetComponent<Chase_Turn_Angle>().angle = 0;
            }
        }
    }

}
