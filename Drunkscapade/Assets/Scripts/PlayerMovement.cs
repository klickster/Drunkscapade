using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _testingMovement = true;
    [SerializeField] private float _jumpForce;
    
    private Vector3 _movementDirection;
    private PlayerController _player;
    private Rigidbody _rb;
    
    void Awake()
    {
        _player = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _movementDirection = Vector3.zero;

        if (!_testingMovement)
        {
            if (InputManager.Instance.IsMovingForward)
                MoveForward();

            if (InputManager.Instance.IsMovingBackward)
                MoveBackwards();

            if (InputManager.Instance.IsMovingLeft)
                MoveLeft();

            if (InputManager.Instance.IsMovingRight)
                MoveRight();
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
                MoveForward();

            if (Input.GetKey(KeyCode.S))
                MoveBackwards();

            if (Input.GetKey(KeyCode.A))
                MoveLeft();

            if (Input.GetKey(KeyCode.D))
                MoveRight();
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
            
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
