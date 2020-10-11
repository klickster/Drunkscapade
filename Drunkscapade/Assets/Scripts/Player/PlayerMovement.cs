using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _testingMovement = true;
    [Header("Jump")]
    [SerializeField] private LayerMask _floorLayer;
    [SerializeField] private Transform _checkFloorTransform;
    [SerializeField] private float _floorDistance = .4f;
    [SerializeField] private float _jumpForce;

    private bool _isGrounded;
    private Vector3 _movementDirection;
    private PlayerController _player;
    private Rigidbody _rb;

    public bool IsGrounded => _isGrounded;
    
    void Awake()
    {
        _player = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _movementDirection = Vector3.zero;
        _isGrounded = Physics.CheckSphere(_checkFloorTransform.position, _floorDistance, _floorLayer);

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
            
        _player.AttemptToMovePlayer(_movementDirection * (_speed * Time.deltaTime));

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
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
