using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPool
    : MonoBehaviour
{
    [SerializeField]
    GameObject template;

    GameObject player;

    GameObject[] pool = new GameObject[50];
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(template);
            float x = Random.Range(0.0f, 1.8f);
            float y = Random.Range(player.transform.position.y + 20.0f, 300.0f);
            pool[i].transform.position = new Vector3(x, y, 0.0f);

            float fallSpeed = Random.Range(1.5f, 3.5f);
            pool[i].GetComponent<Obstacle>().maxDown = fallSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if objects need to be reset

        // Reset objects that needs it
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].transform.position.y <= -1.0f)
            {
                // Random number between start of level and outside.
                // Random number between walls
                float x = Random.Range(0.0f, 1.8f);
                float y = Random.Range(player.transform.position.y + 20.0f, 300.0f);
                pool[i].transform.position = new Vector3(x, y, 0.0f);
            }
        }
    }
}
