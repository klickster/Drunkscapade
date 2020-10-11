using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Sleepy")]
    [SerializeField] private float _sleepRatio = 0.5f;
    [SerializeField] private float _wakeUpRatio = 5;
    [SerializeField] private float _maxWakeyness = 100f;
    [Header("Tumble")]
    [SerializeField] private float _tumbleSpeed;
    [SerializeField] private Transform _tumbleOrientator;
    [SerializeField] private float _tumbleCooldown = 8f;
    [SerializeField] private float _tumbleDuration = 2f;
    [Header("Canvas")]
    [SerializeField] private CanvasController _canvasController;
    [SerializeField, Range(0, 1)] private float _drunkyness;

    private float _wakeyness;
    private Rigidbody _rb;
    private PlayerMovement _playerMovement;
    private bool _isDead;

    private bool _tumbling;
    private Vector3 _tumbleDirection;
    private float _currentTumbleSpeed;
    private float _tumbleSpeedDecreaseRatio;
    private float _initialTumblingTime;

    private bool _falling;
    private float _t;
    private bool _fallLeft;

    private Vector3 _drunkenMovement;
    private Vector3 _desiredMovement;
    private Vector3 _movement;

    public bool IsFallingAsleep { get; private set; }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        InvokeRepeating(nameof(Tumble), _tumbleCooldown, _tumbleCooldown);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            _isDead = false;

        if (_isDead) return;

        _movement = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.P))
            Tumble();

        if (IsFallingAsleep)
        {
            _wakeyness -= _sleepRatio;

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                _wakeyness += _wakeUpRatio;
            }

            var currentWakePercetange = _wakeyness / _maxWakeyness;
            _canvasController.UpdateWakeUpBar(currentWakePercetange);
            if (currentWakePercetange >= 1)
                WakeUp();
        }

        if (_tumbling)
        {
            _t = (Time.time - _initialTumblingTime) / _tumbleDuration;

            _currentTumbleSpeed = Mathf.Lerp(_tumbleSpeed, 0, _t);
            _drunkenMovement = _tumbleDirection * _currentTumbleSpeed * Time.deltaTime;
            if (_t >= 1)
            {
                StopTumble();
            }
        }

        MovePlayer();
    }

    private void Tumble()
    {
        if (!_playerMovement.IsGrounded) return;

        _tumbleDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1f, 1f));
        _tumbleOrientator.DOLocalRotate(new Vector3(0, 0, 24 * (_tumbleDirection.x < 0 ? -1 : 1)),
                                    _tumbleDuration);
        _initialTumblingTime = Time.time;
        _currentTumbleSpeed = _tumbleSpeed;
        _tumbling = true;
    }

    private void StopTumble()
    {
        _tumbling = false;
        _tumbleOrientator.DOLocalRotate(new Vector3(0, 0, 0), _tumbleDuration / 2);
    }

    public void StartFallingSleep()
    {
        _wakeyness = 15;
        IsFallingAsleep = true;
    }

    public void WakeUp()
    {
        IsFallingAsleep = false;
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

    public void PushPlayer(Vector3 force, bool kill = false)
    {
        _rb.AddForce(force, ForceMode.Impulse);
        _isDead = kill;
    }
}
