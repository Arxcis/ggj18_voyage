using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add checks to ensure player stays within "screen"

public class Player : MonoBehaviour
{
    MeshRenderer meshRenderer;
    float invisibleTimeout;
    float wetTimeout;
    float wetScale;
    bool wet;

    Camera camera;


    // @doc - https://docs.unity3d.com/ScriptReference/Rigidbody.AddForce.html - 26.01.18
    public Rigidbody rb;
    public float maxSpeedUp;
    public float maxSpeedDown;
    public float leftSpeed;
    public float rightSpeed;
    public float thrust;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        camera = gameObject.GetComponentInChildren<Camera>();

        wetScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Key checking
        if (Input.GetKey("up"))
        {
            rb.AddForce(transform.up * thrust * wetScale);
        }

        if (rb.velocity.y > maxSpeedUp * wetScale)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeedUp * wetScale, 0.0f);
        }

        if (rb.velocity.y < maxSpeedDown * wetScale)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeedDown * wetScale, 0.0f);
        }

        if (Input.GetKeyDown("left"))
        {
            rb.velocity += new Vector3(leftSpeed * wetScale, rb.velocity.y * wetScale, 0.0f);
        }
        if (Input.GetKeyUp("left"))
        {
            rb.velocity -= new Vector3(leftSpeed * wetScale, rb.velocity.y * wetScale, 0.0f);
        }

        if (Input.GetKeyDown("right"))
        {
            rb.velocity += new Vector3(rightSpeed * wetScale, rb.velocity.y * wetScale, 0.0f);
        }
        if (Input.GetKeyUp("right"))
        {
            rb.velocity -= new Vector3(rightSpeed * wetScale, rb.velocity.y * wetScale, 0.0f);
        }

        // Effects checking
        invisibleTimeout -= Time.deltaTime;
        if (meshRenderer.enabled == false && invisibleTimeout <= 0.0f)
        {
            meshRenderer.enabled = true;
        }

        wetTimeout -= Time.deltaTime;
        if (wet && wetTimeout <= 0.0f)
        {
            wetScale = 1.0f;
        }

        camera.transform.position = new Vector3(0.0f, camera.transform.position.y, camera.transform.position.z);
    }

    public void MakeInvisible()
    {
        Debug.Log("MakeInvisible");
        meshRenderer.enabled = false;
        invisibleTimeout = 1.0f;
    }

    public void MakeWet()
    {
        Debug.Log("MakeWet");
        wetTimeout = 4.0f;
        wet = true;
        wetScale = 0.25f;
    }
}
