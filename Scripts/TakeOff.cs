using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOff : MonoBehaviour {

    private GameObject player;
    private GameObject petriDish;
    private GameObject petriDishGround;
    private GameObject level1Button; 
    private GameObject level1Arrow;

    private bool exploded = false;

    // Use this for initialization
    void Start () {
        player          = GameObject.FindWithTag("Player");
        petriDish       = GameObject.FindWithTag("petri-dish");
        petriDishGround = GameObject.FindWithTag("petri-dish-ground");
        level1Button    = GameObject.FindWithTag("level1-button");
        level1Arrow     = GameObject.FindWithTag("level1-arrow");
    }
    
    // Update is called once per frame
    void Update () {
        
        if (Input.GetKeyDown("up") && !exploded) {

            Rigidbody rbplayer = player.GetComponent<Rigidbody>();

            rbplayer.AddForce(new Vector3(0.0f, 500.0f, 0.0f));
            petriDish.SetActive(false);
            petriDishGround.SetActive(false);
            level1Button.SetActive(false); 
            level1Arrow.SetActive(false);
        }

    }
}
