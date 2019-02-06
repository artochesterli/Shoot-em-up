using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static GameObject Avatar;
    // Start is called before the first frame update
    private void Awake()
    {
        Avatar = GameObject.Find("Avatar").gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
