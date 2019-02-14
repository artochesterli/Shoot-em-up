using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce_back : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float bound_y = Camera.main.orthographicSize;
        float bound_x = Camera.main.orthographicSize*Camera.main.pixelWidth/Camera.main.pixelHeight;
        var speed = GetComponent<Speed>();
        if (transform.position.x > bound_x)
        {
            speed.direction.x = -Mathf.Abs(speed.direction.x);
        }
        else if(transform.position.x < -bound_x)
        {
            speed.direction.x = Mathf.Abs(speed.direction.x);
        }
        if(transform.position.y > bound_y)
        {
            speed.direction.y = -Mathf.Abs(speed.direction.y);
        }
        else if(transform.position.y < -bound_y)
        {
            speed.direction.y = Mathf.Abs(speed.direction.y);
        }
    }
}
