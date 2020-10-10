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
    
    private bool _isFallingAsleep;
    private float _wakeyness;

    void Start()
    {
        
    }

    void Update()
    {
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

    public void RotatePlayer(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }
}
