using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    public int Score { get; protected set; }

    private float xRange = 6;
    private float ySpawnPos = 15.5f;
    private float lowLimit = -2.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowLimit)
        {
            Destroy(gameObject);
        }
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
