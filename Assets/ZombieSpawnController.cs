using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawnController : MonoBehaviour
{
    public int initialZombiesPerWave = 5;
    public int currentZombiesPerWave;

    public float spawnDelay = 0.5f; // Delay between spawning  each zombie in a wave;

    public int currentWave = 0;
    public float waveCooldown = 10.0f; // Time in seconds between waves;

    public bool inCooldown;
    public float cooldownCounter = 0; // We only use this for testing and the UI;

    public List<Enemy> currentZombiesAlive;

    public GameObject zombiePrefab;

    private void Start()
    {
        currentZombiesPerWave = initialZombiesPerWave;

        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();

        currentWave++;

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i =0; i < currentZombiesPerWave; i++) 
        {
            // Generate a random offset within a specified range
            Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(-1f,1f), 0f, UnityEngine.Random.Range(-1f,1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Instantiate the Zombie
            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            // Get Enemy Script
            Enemy enemyScript = zombie.GetComponent<Enemy>();

            // Track this zombie
            currentZombiesAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        // Get all dead zombies
        List<Enemy> zombiesToRemove = new List<Enemy>();
        foreach (Enemy zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
            }
        }

        // Actually remove all dead zombies
        foreach (Enemy zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }

        zombiesToRemove.Clear();

        // Start Cooldown if all zombies are dead
        if (currentZombiesAlive.Count == 0 && inCooldown == false)
        {
            // Start coolDown for next wave 
            StartCoroutine(WaveCooldown());
        }

        // Run the cooldown counter
        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            // Reset the counter
            cooldownCounter = waveCooldown;
        }

     }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;

        currentZombiesPerWave *= 2;
        StartNextWave();
    }




}
