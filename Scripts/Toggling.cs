using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggling : MonoBehaviour
{
    GameObject regular;
    GameObject sick;
    float countDown = 3.0f;
    bool updated = false;
    // Use this for initialization
    void Start()
    {
        regular = GameObject.Find("Arm_Hair_Regular");
        sick = GameObject.Find("Arm_Hair_Sick");
        //sick.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        Debug.Log("Update");
        if (countDown <= 0.0f && !updated)
        {
            updated = true;
            var tmp = sick.transform.position;
            sick.transform.position = regular.transform.position;
            regular.transform.position = tmp;
        }
    }
}
