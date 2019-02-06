using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        var target_object = GetComponent<Target_Object>();
        var speed = GetComponent<Speed>();
        float angle = GetComponent<Chase_Turn_Angle>().angle*Time.deltaTime;
        if (speed.direction.magnitude>0)
        {
            Vector3 offset = target_object.target.transform.position - transform.position;
            float angle_offset = Vector3.SignedAngle(speed.direction, offset,  new Vector3(0,0,1));
            if (Mathf.Abs(angle_offset) >= angle)
            {
                if (angle_offset > 0)
                {
                    speed.direction = Rotate_Vector(new Vector2(speed.direction.x, speed.direction.y), angle);
                }
                else
                {
                    speed.direction = Rotate_Vector(new Vector2(speed.direction.x, speed.direction.y), -angle);
                }
            }
            else
            {
                Vector3 offset_v = target_object.target.transform.position - transform.position;
                speed.direction = offset_v;
                speed.direction.Normalize();
            }
        }
        else
        {
            Vector3 offset = target_object.target.transform.position - transform.position;
            speed.direction = offset;
            speed.direction.Normalize();
        }
    }

    private Vector3 Rotate_Vector(Vector2 v,float angle)
    {
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        v.x = (cos * v.x) - (sin * v.y);
        v.y = (sin * v.x) + (cos * v.y);
        return new Vector3(v.x,v.y,0);
    }

}
