using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar_Shoot : MonoBehaviour {

    public float bullet_interval;
    public float bullet_initial_distance_to_avatar;

    private float bullet_interval_time_count_down;

	// Use this for initialization
	void Start () {
        bullet_interval_time_count_down = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            if (bullet_interval_time_count_down <= 0)
            {
                bullet_interval_time_count_down = bullet_interval;
                Instantiate(Resources.Load("Prefabs/Bullet_Avatar"), transform.position + transform.up * bullet_initial_distance_to_avatar, transform.rotation);
            }
            else
            {
                bullet_interval_time_count_down -= Time.deltaTime;
            }
        }
        else
        {
            bullet_interval_time_count_down = 0;
        }
	}
}
