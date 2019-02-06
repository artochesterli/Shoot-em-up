using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chase_Avatar : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        GetComponent<Target_Object>().target = Main.Avatar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
