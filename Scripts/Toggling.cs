using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Toggling : MonoBehaviour
{
    GameObject regular;
    GameObject sick;
    float countDown = 3.0f;
    bool updated = false;

    GameObject can;
    // Use this for initialization
    void Start()
    {
        regular = GameObject.Find("Arm_Hair_Regular");
        sick = GameObject.Find("Arm_Hair_Sick");
        can = GameObject.Find("Text");
        //sick.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0.0f && !updated)
        {
            updated = true;
            var tmp = sick.transform.position;
            sick.transform.position = regular.transform.position;
            regular.transform.position = tmp;

            Text t = can.GetComponent<Text>();
            t.text = "Smallpox killed 500 000 001 people in the 20th century alone";


        }
    }
}
