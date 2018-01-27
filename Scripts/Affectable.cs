using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affectable 
    : MonoBehaviour
{
    MeshRenderer renderer;
    float timeout;

    // Use this for initialization
    void Start()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timeout -= Time.deltaTime;
        if (renderer.enabled == false && timeout <= 0.0f)
        {
            renderer.enabled = true;
        }
    }

    public void MakeInvisible()
    {
        renderer.enabled = false;
        timeout = 1.0f;
    }

    
}
