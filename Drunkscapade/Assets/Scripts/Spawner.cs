using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] carsNpc;

    [SerializeField] private float spawnTime = 2f;
    private float cdSpawn;
    private float realSpawnTime;

    private void Start()
    {
        realSpawnTime = Random.Range(spawnTime, spawnTime * 2f);
    }

    private void Update()
    {
        cdSpawn += Time.deltaTime;

        if (cdSpawn >= realSpawnTime)
        {
            SpawnCar();
            cdSpawn = 0;
            realSpawnTime = Random.Range(spawnTime, spawnTime * 1.5f);
        }
    }

    private void SpawnCar()
    {
        var spawnedCar = Instantiate(carsNpc[Random.Range(0, carsNpc.Length)], transform.position, Quaternion.identity, transform);
        spawnedCar.transform.forward = transform.forward;
    }
}