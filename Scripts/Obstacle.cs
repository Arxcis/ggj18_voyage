using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle 
    : MonoBehaviour
{
    public enum Type
    {
        WATER, // Fast
        DUST, // Medium
        DEAD_CELL, // Slow
        WIND // Unsure if this belongs here
    }

    public enum WindDirection
    {
        LEFT,
        RIGHT,
    }

    [SerializeField]
    Type type;

    [SerializeField]
    WindDirection windDirection;

    [SerializeField]
    float maxDown;

    Rigidbody rb;
//    SphereCollider sc;


    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        if (type == Type.DEAD_CELL)
        {
            gameObject.GetComponent<SphereCollider>().isTrigger = false;

            // Hack for dealing with to powerful player.
            rb.mass = 100;
        }
        
        if (type == Type.WIND)
        {
            //gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < -maxDown && type != Type.WIND)
        {
            rb.velocity = new Vector3(0.0f, -maxDown, 0.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (type == Type.WATER)
                player.MakeWet();
            else if (type == Type.DUST)
                player.MakeInvisible();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && type == Type.WIND)
        {
            float windForce = 100.0f;
            Rigidbody player = other.gameObject.GetComponent<Rigidbody>();
            if (windDirection == WindDirection.LEFT)
            {
                player.AddForce(new Vector3(-windForce, 0.0f));
            }
            else
            {
                player.AddForce(new Vector3(windForce, 0.0f));
            }

        }
    }
}

