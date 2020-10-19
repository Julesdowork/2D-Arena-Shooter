using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;         // The name of this particular wave
    public Transform enemy;     // The enemies encountered in this wave
    public int count;           // How many enemies are in the wave
    public float rate;          // How many enemies spawn per second
}

public class WaveSpawner : MonoBehaviour
{
    // Spawning = this wave's enemies are spawning
    // Waiting = waiting for all the enemies in this wave to be destroyed
    // Counting = counting down to the start of the next wave
    public enum SpawnState { Spawning, Waiting, Counting };

    // A collection of waves this level contains
    [SerializeField] Wave[] waves;
    // The time it takes for a new wave to begin once the old one is finished
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] Transform[] spawnPoints;

    int nextWave = 0;
    SpawnState spawnState = SpawnState.Counting;
    float searchCountdown = 1f;
    float waveCountdown;

    public SpawnState State
    {
        get { return spawnState; }
    }

    public float WaveCountdown
    {
        get { return waveCountdown; }
    }

    public int NextWave
    {
        get { return nextWave + 1; }
    }

    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnState == SpawnState.Waiting)
        {
            if (!EnemiesStillAlive())
                WaveCompleted();
            else
                return;
        }
        if (waveCountdown <= 0)
        {
            if (spawnState != SpawnState.Spawning)
                StartCoroutine(SpawnWave(waves[nextWave]));
        }
        else
            waveCountdown -= Time.deltaTime;
    }

    bool EnemiesStillAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag(TagManager.ENEMY).Length == 0)
                return false;
        }

        return true;
    }

    void WaveCompleted()
    {
        spawnState = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        nextWave++;
        if (nextWave >= waves.Length)
        {
            nextWave = 0;
            Debug.Log("All waves completed!");
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        spawnState = SpawnState.Spawning;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        spawnState = SpawnState.Waiting;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, spawnPoint.position, Quaternion.identity);
    }
}
