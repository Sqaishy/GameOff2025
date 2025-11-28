using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LeakyHoleSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> holeSpawnPoints;
    [SerializeField] private GameObject leakPrefab;
    [Tooltip("The amount of holes to spawn at once when the submarine crashes")]
    [Min(1)]
    [SerializeField] private int spawnAmount = 1;

    private List<Transform> availableSpawnPoints;

    private void Awake()
    {
        availableSpawnPoints = new List<Transform>(holeSpawnPoints);
    }

    public void SpawnHole()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            if (availableSpawnPoints.Count == 0)
                break;

            Transform spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];

            Instantiate(leakPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);

            availableSpawnPoints.Remove(spawnPoint);
        }
    }
}
