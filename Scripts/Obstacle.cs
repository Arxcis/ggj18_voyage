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
    public float maxDown;

    Rigidbody rb;

    const float windForce = 10.0f;

    // Only for wind
    SpriteRenderer renderer;
    Sprite[] sprites;
    float animationTimer;
    int currSprite;
    int spriteOffset;

    // Only for DEAD_CELL
    Vector3 rotationVec;

    // Use this for initialization
    void Start()
    {

        if (type == Type.WIND)
        {
            renderer = gameObject.GetComponent<SpriteRenderer>();
            sprites = Resources.LoadAll<Sprite>("spritesheet-wind");
            renderer.sprite = sprites[0];
            spriteOffset = (windDirection == WindDirection.LEFT)
                         ? 0
                         : 3;
        }
        else
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }

        if (type == Type.DEAD_CELL)
        {
            rotationVec = new Vector3(-1.0f, 0.5f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (type != Type.WIND && rb.velocity.y < -maxDown)
        {
            rb.velocity = new Vector3(0.0f, -maxDown, 0.0f);
        }

        if (type == Type.WIND)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= 0.15f)
            {
                currSprite = ((currSprite + 1) % 3) + spriteOffset;
                renderer.sprite = sprites[currSprite];
                animationTimer = 0.0f;
            }
        }

        if (type == Type.DEAD_CELL)
        {
            gameObject.transform.Rotate(rotationVec);
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
                player.EnterWind(windDirection);
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
                Player player = other.gameObject.GetComponent<Player>();
                player.ExitWind();
            }
        }
    }
}

