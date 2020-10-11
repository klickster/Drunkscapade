using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffObject : MonoBehaviour
{
    [SerializeField] private GameObject _objectToTurnOff;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _objectToTurnOff.SetActive(false);
        }
    }
}
