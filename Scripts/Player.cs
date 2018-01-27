using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add checks to ensure player stays within "screen"

public class Player : MonoBehaviour
{
    float invisibleTimeout;
    float wetTimeout;
    float wetScale;
    bool wet;

    Camera camera;

    // @doc - https://docs.unity3d.com/ScriptReference/Rigidbody.AddForce.html - 26.01.18
    public Rigidbody rb;
    public float maxSpeedUp;
    public float maxSpeedDown;
    public float extraSpeedDiving;
    public float leftSpeed;
    public float rightSpeed;
    public float thrust;

    Vector3 windVec;



    // SPRITE DATA 
    // @doc https://docs.unity3d.com/ScriptReference/SpriteRenderer-sprite.html - 27.01.18
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    private const int SPRITE_LEGS_OUT = 0;
    private const int SPRITE_LEGS_IN = 1;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = gameObject.GetComponentInChildren<Camera>();

        wetScale = 1.0f;

        windVec = new Vector3();
        controlsVec = new Vector3();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("virus-sprite");
    }

    // Update is called once per frame
    void Update()
    {
        // Key checking

        if (Input.GetKey("up"))
        {
            rb.AddForce(transform.up * thrust);
        }
        if (Input.GetKeyDown("down")) 
        {
            maxSpeedDown += extraSpeedDiving;
        } 
        if (Input.GetKeyUp("down")) 
        {
            maxSpeedDown -= extraSpeedDiving;
        }
        if (Input.GetKeyDown("down") || Input.GetKeyDown("up")) 
        {
            spriteRenderer.sprite = sprites[SPRITE_LEGS_IN];
        }
        if (Input.GetKeyUp("down") || Input.GetKeyUp("up"))
        {
            spriteRenderer.sprite = sprites[SPRITE_LEGS_OUT];
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
        if (spriteRenderer.enabled == false && invisibleTimeout <= 0.0f)
        {
            spriteRenderer.enabled = true;
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
        spriteRenderer.enabled = false;
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
