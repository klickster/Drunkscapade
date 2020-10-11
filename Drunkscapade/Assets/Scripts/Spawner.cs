using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] carsNpc;

    [SerializeField] private Vector2 spawnTimeRange = new Vector2(5, 20);
    private float cdSpawn;
    private float realSpawnTime;

    private void Start()
    {
        realSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
    }

    private void Update()
    {
        cdSpawn += Time.deltaTime;

        if (cdSpawn >= realSpawnTime)
        {
            SpawnCar();
            cdSpawn = 0;
            realSpawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        }
    }

    private void SpawnCar()
    {
        var spawnedCar = Instantiate(carsNpc[Random.Range(0, carsNpc.Length)], transform.position, Quaternion.identity, transform);
        spawnedCar.transform.forward = transform.forward;
    }
}