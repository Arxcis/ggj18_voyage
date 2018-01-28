using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    GameObject head;
    GameObject body;
    GameObject leg1;
    GameObject leg2;
    GameObject leg3;


    // Use this for initialization
    void Start () {
        head = gameObject.transform.Find("head").gameObject;
        body = gameObject.transform.Find("body").gameObject;
        leg1 = gameObject.transform.Find("leg1").gameObject;
        leg2 = gameObject.transform.Find("leg2").gameObject;
        leg3 = gameObject.transform.Find("leg3").gameObject;
    }
    // Update is called once per frame
    void Update () {
        
        head.transform.Rotate(new Vector3(0.0f, 0.0f, 15.0f * Time.deltaTime * 1.0f));
        body.transform.Rotate(new Vector3(0.0f, 0.0f, 15.0f * Time.deltaTime * -1.0f));
        leg1.transform.Rotate(new Vector3(0.0f, 0.0f, 15.0f * Time.deltaTime * 1.0f));
        leg2.transform.Rotate(new Vector3(0.0f, 0.0f, 15.0f * Time.deltaTime * -1.0f));
        leg3.transform.Rotate(new Vector3(0.0f, 0.0f, 15.0f * Time.deltaTime * 1.0f));
    }   
}
