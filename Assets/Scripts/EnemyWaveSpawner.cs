using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int waveSize = 3;

    public float waveInterval = 1.0f;
    public float enemySeperation = 3.0f;
    private float waveTimer;

    private List<GameObject> currentWave = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave.Count == 0)
            waveTimer -= Time.deltaTime;
        if (waveTimer < 0.0f)
        {
            SpawnWave();
            waveTimer = waveInterval;
        }

        currentWave.RemoveAll(w => w == null);
    }

    void SpawnWave()
    {
        for (int i = 0; i < waveSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.transform.localPosition = Vector3.right * enemySeperation * (i - (waveSize /2 ));
            currentWave.Add(enemy);
        }
    }
}
