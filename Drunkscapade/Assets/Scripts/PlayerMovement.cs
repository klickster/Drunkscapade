﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Vector3 _movementDirection;
    private PlayerController _player;
    
    void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    void Update()
    {
        _movementDirection = Vector3.zero;
        
        if(InputManager.Instance.IsMovingForward)
            MoveForward();
        
        if(InputManager.Instance.IsMovingBackward)
            MoveBackwards();
        
        if(InputManager.Instance.IsMovingLeft)
            MoveLeft();
        
        if(InputManager.Instance.IsMovingRight)
            MoveRight();
            
        _player.AttemptToMovePlayer(_movementDirection * (_speed * Time.deltaTime));
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
