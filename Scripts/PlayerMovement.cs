using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  
  // @doc - https://docs.unity3d.com/ScriptReference/Rigidbody.AddForce.html - 26.01.18
  public Rigidbody rb;
  public float maxSpeedUp;
  public float maxSpeedDown;
  public float leftSpeed;
  public float rightSpeed;   
  public float thrust;
	// Use this for initialization
	void Start () {
    rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("up")) {
      rb.AddForce(transform.up * thrust);
    }

    if (rb.velocity.y > maxSpeedUp) {
        rb.velocity = new Vector3(0.0f, maxSpeedUp, 0.0f);
        Debug.Log(rb.velocity);
    }

    if (rb.velocity.y < maxSpeedDown) {
      rb.velocity = new Vector3(0.0f, maxSpeedDown, 0.0f);
    }

    if (Input.GetKey("left")) { 
      rb.velocity = new Vector3(leftSpeed, rb.velocity.y, 0.0f);
    }

    if (Input.GetKey("right")) { 
      rb.velocity = new Vector3(rightSpeed, rb.velocity.y, 0.0f);
    }
	}
}
