using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Avatar_Move : MonoBehaviour {

    public float speed;

    private const float size = 0.5f;
    private float destroy_x;
    private float destroy_y;
	// Use this for initialization
	void Start () {
        
        destroy_x = Camera.main.orthographicSize * Camera.main.pixelWidth / Camera.main.pixelHeight + size / 2;
        destroy_y = Camera.main.orthographicSize + size / 2;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += speed * transform.up * Time.deltaTime;
        if (transform.position.y >= destroy_y||transform.position.y<=-destroy_y||transform.position.x>destroy_x||transform.position.x< -destroy_x)
        {
            Destroy(gameObject);
        }
	}
}
