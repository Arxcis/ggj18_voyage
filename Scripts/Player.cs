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

    Vector3 windVec;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        camera = gameObject.GetComponentInChildren<Camera>();

        wetScale = 1.0f;

        windVec = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        // Key checking

        if (Input.GetKey("up"))
        {
            rb.AddForce(transform.up * thrust);
        }

        Vector3 temp = new Vector3(0.0f, rb.velocity.y, 0.0f);
        if (rb.velocity.y > maxSpeedUp)
        {
            temp.y = maxSpeedUp;
        }
        else if (rb.velocity.y < maxSpeedDown)
        {
            temp.y = maxSpeedDown;
        }

        if (Input.GetKey("left"))
        {
            temp.x = leftSpeed;
        }
        if (Input.GetKey("right"))
        {
            temp.x = rightSpeed;
        }

        temp += windVec;
        temp *= wetScale;

        rb.velocity = temp;

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

    public void EnterWind(Obstacle.WindDirection windDirection)
    {
        Debug.Log("Enter wind");

        if (windDirection == Obstacle.WindDirection.LEFT)
        {
            windVec.x = -1.0f;
        }
        else
        {
            windVec.x = 1.0f;
        }
    }

    public void ExitWind()
    {
        Debug.Log("Exit wind");
        windVec.x = 0.0f;
        windVec.y = 0.0f;
        windVec.z = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("border"))
        {
            Game game = FindObjectOfType<Game>();
            game.GameOver();
        }
    }
}
