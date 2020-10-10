using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _sleepRatio = 0.5f;
    [SerializeField] private float _wakeUpRatio = 5;
    [SerializeField] private float _maxWakeyness = 100f;
    [SerializeField] private CanvasController _canvasController;
    [SerializeField, Range(0, 1)] private float _drunkyness;

    private bool _isFallingAsleep;
    private float _wakeyness;
    [SerializeField] private Vector3 _drunkenMovement;
    private Vector3 _desiredMovement;
    private Vector3 _movement;

    void Start()
    {
        
    }

    void Update()
    {
        _movement = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.P))
            StartFallingSleep();

        if (_isFallingAsleep)
        {
            _wakeyness -= _sleepRatio;

            if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                _wakeyness += _wakeUpRatio;
            }

            var currentWakePercetange = _wakeyness / _maxWakeyness;
            _canvasController.UpdateWakeUpBar(currentWakePercetange);

            if (currentWakePercetange >= 1)
                WakeUp();
        }

        MovePlayer();
    }

    public void StartFallingSleep()
    {
        _wakeyness = 15;
        _isFallingAsleep = true;
    }

    public void WakeUp()
    {
        _isFallingAsleep = false;
        _wakeyness = _maxWakeyness;
        _canvasController.UpdateWakeUpBar(1);
    }

    public void AttemptToMovePlayer(Vector3 movement)
    {
        _desiredMovement = movement * (1 - _drunkyness);
    }

    public void MovePlayer()
    {
        _movement = _drunkenMovement + _desiredMovement;
        transform.position += _movement;
    }

    public void RotatePlayer(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }
}
