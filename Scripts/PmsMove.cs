using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PmsMove : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            var movement = new Vector3(0.0f, 1.0f, 0.0f);
            gameObject.GetComponent<Rigidbody>().AddForce(movement * 0.5f);
        }

    }
}
