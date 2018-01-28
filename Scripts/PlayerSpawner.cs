using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner
    : MonoBehaviour
{
    [SerializeField]
    GameObject template;

    GameObject player;

    GameObject[] pool = new GameObject[80];

    float timeout;
    int counter = 0;
    // Use this for initialization

    // 0.285
    // 0.465
    // -1.054
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(template);
            var vec = new Vector3(pool[i].transform.position.x, pool[i].transform.position.y, pool[i].transform.position.z);
            vec.x += Random.Range(-0.5f, 0.5f);
            vec.z += Random.Range(-0.2f, 0.2f);

            pool[i].SetActive(false);
            pool[i].transform.position = vec;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeout -= Time.deltaTime;
        if (counter < pool.Length && timeout <= 0.0f)
        {
            pool[counter++].SetActive(true);
            timeout = 0.10f;
        }
    }
}
