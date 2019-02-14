using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Random_Move : MonoBehaviour
{
    public float radius_low;
    public float radius_high;
    // Start is called before the first frame update
    void Start()
    {
        Set_Random_Target_Position();
    }

    // Update is called once per frame
    void Update()
    {
        var target = GetComponent<Target_Position>();
        var speed = GetComponent<Speed>();
        bool arrived = false;
        if(Vector3.Dot(speed.direction, target.pos - transform.position) < 0)
        {
            arrived = true;

        }
        
        if (arrived)
        {
            Set_Random_Target_Position();
        }
    }

    private void Set_Random_Target_Position()
    {
        var target_pos = GetComponent<Target_Position>();
        var speed = GetComponent<Speed>();
        Vector3 offset = Random.insideUnitCircle * (radius_high - radius_low);
        offset.Normalize();
        offset += offset* radius_low;
        target_pos.pos = transform.position + offset;
        speed.direction = target_pos.pos - transform.position;
        speed.direction.Normalize();
    }
}
