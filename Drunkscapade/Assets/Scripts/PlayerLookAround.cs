using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAround : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private Vector2 _mouseYRotationRange;
    [SerializeField] private PlayerController _player;

    private float _xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _mouseYRotationRange.x, _mouseYRotationRange.y);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _player.RotatePlayer(Vector3.up * mouseX);
    }
}
