using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Align_Direction : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var speed = GetComponent<Speed>();
        float angle = Mathf.Atan2(speed.direction.y, speed.direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    }
}
