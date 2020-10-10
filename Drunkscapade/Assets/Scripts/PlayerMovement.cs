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
        _movementDirection = Vector3.zero;
        //TEMPORAL
        if(Input.GetKey(KeyCode.W))
            MoveForward();
        
        if(Input.GetKey(KeyCode.S))
            MoveBackwards();
        
        if(Input.GetKey(KeyCode.A))
            MoveLeft();
        
        if(Input.GetKey(KeyCode.D))
            MoveRight();
            
        transform.position += _movementDirection * (_speed * Time.deltaTime);
    }

    void MoveForward()
    {
        _movementDirection += transform.forward;
    }

    void MoveBackwards()
    {
        _movementDirection -= transform.forward;
    }

    void MoveRight()
    {
        _movementDirection += transform.right;
    }
    
    void MoveLeft()
    {
        _movementDirection -= transform.right;
    }
}
