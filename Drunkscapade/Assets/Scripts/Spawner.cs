using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject carNpc;

    [SerializeField] private float spawnTime = 2f;
    private float cdSpawn;

    void Update()
    {
        cdSpawn += Time.deltaTime;

        if (cdSpawn >= spawnTime)
        {
            var spawnedCar = Instantiate(carNpc, transform.position, Quaternion.identity);
            spawnedCar.transform.forward = transform.forward;

            cdSpawn = 0;
        }
    }
}