using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle 
    : MonoBehaviour
{
    public enum Type
    {
        WATER,
        DUST,
        DEAD_CELLS,
        WIND // Unsure if this belongs here
    }

    [SerializeField]
    Type type;

    [SerializeField]
    float maxDown;


    Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < -maxDown)
        {
            rb.velocity = new Vector3(0.0f, -maxDown, 0.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case Type.WATER:
                    // Slow player down
                    other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, -1000.0f, 0.0f));
                    break;
                case Type.DEAD_CELLS:
                    // Crash with these, they push you down until you get away.
                    break;
                case Type.DUST:
                    // Become invisible for a small while
                    break;
                case Type.WIND:
                    // BLOW
                    break;
            }
        }
    }
}
