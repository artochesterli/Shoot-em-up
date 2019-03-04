using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emit_Bullet : MonoBehaviour
{
    public float Interval;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot_Bullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Shoot_Bullet()
    {
        float time_count = 0;
        while (true)
        {
            if (time_count >= Interval)
            {
                time_count = 0;
                if (Main.Avatar != null)
                {
                    GameObject bullet = (GameObject)Instantiate(Resources.Load("Prefabs/Bullet_Enemy"), transform.position, new Quaternion(0, 0, 0, 0));
                    Vector3 offset = Main.Avatar.transform.position - transform.position;
                    offset.Normalize();
                    bullet.GetComponent<Speed>().direction = offset;
                }
            }
            time_count += Time.deltaTime;
            yield return null;
        }
    }
}
