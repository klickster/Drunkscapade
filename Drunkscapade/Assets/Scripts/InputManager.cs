using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private KeyCode _moveForward;
    
    private UnityEvent _onPressMoveForward = new UnityEvent();
    private UnityEvent _onReleaseMoveForward = new UnityEvent();

    public UnityEvent OnPressMoveForward => _onPressMoveForward;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(_moveForward))
        {
            _onPressMoveForward.Invoke();
        }

        if (Input.GetKeyUp(_moveForward))
        {
            _onReleaseMoveForward.Invoke();
        }
    }
}
