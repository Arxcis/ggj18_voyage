using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOff : MonoBehaviour {

    private GameObject player;
    private GameObject particle; 

    private float timeElapsed;

    private bool exploded = false;

    delegate void DestroyMeIn(int num);

    // Use this for initialization
    void Start () {
        player   = GameObject.FindWithTag("Player");
        particle = GameObject.FindWithTag("explosion-particle");
    }
    
    // Update is called once per frame
    void Update () {
        
        if (Input.GetKeyDown("up") && !exploded) {

            Rigidbody rbplayer = player.GetComponent<Rigidbody>();
            rbplayer.velocity = new Vector3(0.0f, 10.0f, 0.0f);
            exploded = true;

            for (int i = 0; i < 100; ++i) {
                GameObject clone = Object.Instantiate(particle);
                clone.SetActive(true);
                clone.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(0.0f, 20.0f), Random.Range(-10.0f, 10.0f));
            }
            timeElapsed = 0.0f;
        }
    

        if (exploded && timeElapsed > 0.5f) {
            Destroy(gameObject);
        }

        timeElapsed += Time.deltaTime;
    }
}
