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

    const float windForce = 10.0f;
    float cellWeigth;


    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        if (type == Type.WIND)
        {
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
            {
                Debug.Log("Collision with water");
                player.MakeWet();
            }
            else if (type == Type.DUST)
            {
                Debug.Log("Collision with Dust");
                player.MakeInvisible();
            }
            else if (type == Type.WIND)
            {
                Rigidbody body = other.gameObject.GetComponent<Rigidbody>();
                if (windDirection == WindDirection.LEFT)
                {
                    // BUGGG!! This f
                    body.velocity -= new Vector3(windForce, 0.0f, 0.0f);
                }
                else
                {
                    body.velocity += new Vector3(windForce, 0.0f, 0.0f);
                }
            }
            else if (type == Type.DEAD_CELL)
            {
                Game game = FindObjectOfType<Game>();
                game.GameOver();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (type == Type.WIND)
            {
                Rigidbody body = other.gameObject.GetComponent<Rigidbody>();
                if (windDirection == WindDirection.LEFT)
                {
                    body.velocity += new Vector3(windForce, 0.0f, 0.0f);
                }
                else
                {
                    body.velocity -= new Vector3(windForce, 0.0f, 0.0f);
                }
            }
        }
    }
}

