using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_0Script : MonoBehaviour
{
    Rigidbody rd;


    void Start()
    {
        rd = GetComponent<Rigidbody>();
        rd.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
