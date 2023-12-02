using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;

    private BoxCollider2D collider;

    public float spawnRate = 1.0f;
    private float spawnCounter;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter < 0.0f)
        {
            SpawnAsteroid();
            spawnCounter = spawnRate;
        }
    }

    private void SpawnAsteroid()
    {
        float randX = UnityEngine.Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float randY = UnityEngine.Random.Range(collider.bounds.min.y, collider.bounds.max.y);

        GameObject asteroid = Instantiate(asteroidPrefab);
        asteroid.transform.position = transform.position + transform.right * randX + transform.up * randY;
    }
}
