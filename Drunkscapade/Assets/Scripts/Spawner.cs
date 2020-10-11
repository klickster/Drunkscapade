using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject carNpc;

    [SerializeField] private float spawnTime = 2f;
    private float cdSpawn;

    private void Awake()
    {
        SpawnCar();
    }

    private void Update()
    {
        cdSpawn += Time.deltaTime;

        if (cdSpawn >= spawnTime)
        {
            SpawnCar();
            cdSpawn = 0;
        }
    }

    private void SpawnCar()
    {
        var spawnedCar = Instantiate(carNpc, transform.position, Quaternion.identity);
        spawnedCar.transform.forward = transform.forward;
    }
}