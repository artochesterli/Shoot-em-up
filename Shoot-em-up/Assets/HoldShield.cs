using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldShield : MonoBehaviour
{
    private GameObject Player;
    private float ShieldTimeCount;
    private const float ShieldUpdateInterval = 2;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Avatar").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ShieldTimeCount += Time.deltaTime;
        if (ShieldTimeCount >= ShieldUpdateInterval)
        {
            ShieldTimeCount = 0;
            
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector3.right, Player.transform.position - transform.position));
        }
    }

}
