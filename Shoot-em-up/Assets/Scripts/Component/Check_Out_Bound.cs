using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Out_Bound : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Out_Bound()
    {
        float bound_x = Camera.main.orthographicSize * Camera.main.pixelWidth / Camera.main.pixelHeight;
        float bound_y = Camera.main.orthographicSize;
        if (transform.position.x > bound_x || transform.position.y > bound_y || transform.position.x < -bound_x || transform.position.y < -bound_y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
