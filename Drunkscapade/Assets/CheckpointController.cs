using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private GameObject _checkPointText;

    private void OnCollisionEnter(Collision collision)
    {
        if (!_checkPointText.activeSelf) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if(player != null)
            {
                player.Puke();
                _checkPointText.SetActive(false);
                GameManager.Instance.SetNewCheckpoint(transform);
            }
        }
    }
}
