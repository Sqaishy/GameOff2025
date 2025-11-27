using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LeakyHoleSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> holeSpawnPoints;
    [SerializeField] private GameObject leakPrefab;

    private List<Transform> availableSpawnPoints;

    private void Awake()
    {
        availableSpawnPoints = new List<Transform>(holeSpawnPoints);
    }

    public void SpawnHole()
    {
        Transform spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];

        Instantiate(leakPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);

        availableSpawnPoints.Remove(spawnPoint);
    }
}
