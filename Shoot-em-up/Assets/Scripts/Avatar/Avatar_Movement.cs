using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar_Movement : MonoBehaviour {

    private float bound_x;
    private float bound_y;
    private const float Avatar_Size = 0.5f;
    private const float margin = 0.5f;
	// Use this for initialization
	void Start () {
        
        bound_x = Camera.main.orthographicSize * Camera.main.pixelWidth / Camera.main.pixelHeight - Avatar_Size / 2 - margin;
        bound_y = Camera.main.orthographicSize - Avatar_Size / 2 - margin;
    }
	
	// Update is called once per frame
	void Update () {
        var speed = GetComponent<Speed>();
        if (Input.GetKey(KeyCode.W)&& transform.position.y < bound_y)
        {
            speed.direction = new Vector3(0, 1, 0);
        }
        else if (Input.GetKey(KeyCode.A)&& transform.position.x > -bound_x)
        {
            speed.direction = new Vector3(-1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S)&& transform.position.y > -bound_y)
        {
            speed.direction = new Vector3(0, -1, 0);
        }
        else if (Input.GetKey(KeyCode.D)&& transform.position.x < bound_x)
        {
            speed.direction = new Vector3(1, 0, 0);
        }
        else
        {
            speed.direction = new Vector3(0, 0, 0);
        }
        change_rotation();
	}

    void change_rotation()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, new Vector3(0, 0, 1));
    }



}
