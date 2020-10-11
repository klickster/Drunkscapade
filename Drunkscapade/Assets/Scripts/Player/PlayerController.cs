using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Sleepy")]
    [SerializeField] private float _sleepGameRatio = 0.01f;
    [SerializeField] private float _sleepEventRatio = 0.5f;
    [SerializeField] private float _wakeUpRatio = 5;
    [SerializeField] private float _maxWakeyness = 100f;
    [SerializeField, Range(0f, 1f)] private float _startSleepEventPercentage;
    [SerializeField] private AudioSource _leftSlap;
    [SerializeField] private AudioSource _rightSlap;
    [Header("Tumble")]
    [SerializeField] private float _tumbleSpeed;
    [SerializeField] private Transform _tumbleOrientator;
    [SerializeField] private float _tumbleCooldown = 8f;
    [SerializeField] private float _tumbleDuration = 2f;
    [Header("DrunkNess")]
    [SerializeField] private float _maxDrunkness = 100f;
    [SerializeField] private float _initialDrunkness = 80f;
    [SerializeField] private float _drunkDecayAmount;
    [SerializeField] private float _drinkDuration;
    [SerializeField] private ParticleSystem _pukeParticles;
    [Header("Sound Effects")]
    [SerializeField] private AudioSource _pukeSfx;
    [SerializeField] private AudioSource _burpSfx;
    [Header("Canvas")]
    [SerializeField] private CanvasController _canvasController;

    private float _wakeyness;
    private float _wakeynessPercentage;
    private Rigidbody _rb;
    private PlayerMovement _playerMovement;
    private bool _isDead;
    private bool _canMove = true;

    private float _currentDrunkness;
    private float _drunknessPercentage;
    private bool _losingDrunkness;

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
    public float WakeynessPercentage => _wakeynessPercentage;
    public float DrunknessPercentage => _drunknessPercentage;
    public bool IsDead => _isDead;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        ResetPlayer();
        InvokeRepeating(nameof(Tumble), _tumbleCooldown, _tumbleCooldown);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayerPosition();
            ResetPlayer();
        }

        if (_isDead) return;

        _movement = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.P))
            Tumble();

        if (Input.GetKeyDown(KeyCode.B))
            AddDrunkness(15f);

        if (_losingDrunkness && _currentDrunkness > 0)
        {
            _currentDrunkness -= _drunkDecayAmount;
            _drunknessPercentage = _currentDrunkness / _maxDrunkness;
            _canvasController.UpdateDrunkBar(_drunknessPercentage, 0);
        }

        if (IsFallingAsleep)
        {
            _wakeyness -= _sleepEventRatio;

            if (Input.GetMouseButtonDown(0))
            {
                _leftSlap.time = 0;
                _leftSlap.Play();
                _wakeyness += _wakeUpRatio;
            }

            if (Input.GetMouseButtonDown(1))
            {
                _rightSlap.time = 0;
                _rightSlap.Play();
                _wakeyness += _wakeUpRatio;
            }
        }
        else
        {
            _wakeyness -= _sleepGameRatio;
        }

        UpdateSleepyness();

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

    private void UpdateSleepyness()
    {
        _wakeynessPercentage = _wakeyness / _maxWakeyness;
        _canvasController.UpdateWakeUpBar(_wakeynessPercentage);

        if (_wakeynessPercentage <= _startSleepEventPercentage)
            StartFallingSleep();

        if (_wakeynessPercentage >= 1)
            WakeUp();
    }

    public void StartFallingSleep()
    {
        _losingDrunkness = false;
        IsFallingAsleep = true;
        _canvasController.StartWakeUpEvent();
    }
    public void WakeUp()
    {
        _losingDrunkness = true;
        IsFallingAsleep = false;
        _wakeyness = _maxWakeyness;
        _canvasController.EndWakeUpEvent();
    }

    private void Tumble()
    {
        if (!_playerMovement.IsGrounded || IsFallingAsleep || !_canMove) return;

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

    public void AttemptToMovePlayer(Vector3 movement)
    {
        _desiredMovement = movement * (1 - (_drunknessPercentage / 1.2f));
    }

    public void MovePlayer()
    {
        if (IsFallingAsleep || !_canMove) return;

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
        if (kill)
            KillPlayer();
    }

    public void AddDrunkness(float amount)
    {
        _losingDrunkness = false;
        _currentDrunkness += amount;
        _burpSfx.Play();
        if(_currentDrunkness >= _maxDrunkness)
        {
            _currentDrunkness = _maxDrunkness;
            Puke();
        }

        _drunknessPercentage = _currentDrunkness / _maxDrunkness;
        _canvasController.UpdateDrunkBar(_drunknessPercentage, _drinkDuration);
        Invoke(nameof(ResumeLosingDrunkness), _drinkDuration);
    }

    public void Puke()
    {
        _canMove = false;
        _pukeParticles.Play();
        _pukeSfx.Play();
        Invoke(nameof(AllowMovement), 1f);
    }

    private void ResumeLosingDrunkness()
    {
        _canMove = true;
        _losingDrunkness = true;
    }

    private void AllowMovement()
    {
        _canMove = true;
    }

    public void KillPlayer()
    {
        _isDead = true;
        Invoke(nameof(ResetPlayer), 2f);
        Invoke(nameof(ResetPlayerPosition), 2f);
    }

    public void ResetPlayer()
    {
        _wakeyness = _maxWakeyness;
        _currentDrunkness = _initialDrunkness;
        _losingDrunkness = true;
        _isDead = false;
    }

    public void ResetPlayerPosition()
    {
        transform.position = GameManager.Instance.CurrentCheckPoint.position;
    }
}
