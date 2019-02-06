using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Out_Bound_Destroy : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Check_Out_Bound>().Out_Bound())
            Destroy(gameObject);
    }
}
