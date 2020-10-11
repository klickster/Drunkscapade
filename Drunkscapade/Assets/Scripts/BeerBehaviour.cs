using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBehaviour : MonoBehaviour
{
    [SerializeField] private float _beerPower = 25f;
    [SerializeField] private float _rotationSpeed = 1.5f;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private AudioSource _beerSfx;

    void Update()
    {
        transform.Rotate(0, _rotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.AddDrunkness(_beerPower);
                _mesh.enabled = false;
                _beerSfx.Play();
                Destroy(gameObject, _beerSfx.clip.length);
            }
        }
    }
}
