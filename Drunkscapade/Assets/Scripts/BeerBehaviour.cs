using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBehaviour : MonoBehaviour
{
    [SerializeField] private float _beerPower = 25f;
    [SerializeField] private float _rotationSpeed = 1.5f;

    void Update()
    {
        transform.Rotate(0, _rotationSpeed, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.AddDrunkness(_beerPower);
                Destroy(gameObject);
            }
        }
    }
}
