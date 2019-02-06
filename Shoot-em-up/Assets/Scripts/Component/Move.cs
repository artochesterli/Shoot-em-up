using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        var speed = GetComponent<Speed>();
        transform.position += speed.speed_value * speed.direction * Time.deltaTime;
    }
}
