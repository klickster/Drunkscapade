using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Vector3 _movementDirection;
    
    void Start()
    {
        InputManager.Instance.OnPressMoveForward.AddListener(MoveForward);
    }

    void Update()
    {
        transform.position += _movementDirection * (_speed * Time.deltaTime);
    }

    void MoveForward()
    {
        Debug.Log("Moving forward");
        _movementDirection += transform.forward;
    }

    void MoveBackwards()
    {
        _movementDirection -= transform.forward;
    }
}
