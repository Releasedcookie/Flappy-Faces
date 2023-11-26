using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour

{
    public GameObject Cloud;
    public float spawnrate = 2;
    private float timer = 0;
    public float heightOffset = 10;
    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCloud();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnrate = logic.SpawnRate / 2;

        if (timer < spawnrate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            SpawnCloud();
            timer = 0;
        }
    }

    public void SpawnCloud()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(Cloud, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}