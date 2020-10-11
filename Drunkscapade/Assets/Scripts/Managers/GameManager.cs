using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform _spawnPoint;

    private Transform _currentCheckpoint;

    public Transform CurrentCheckPoint => _currentCheckpoint;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _currentCheckpoint = _spawnPoint;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();
    }

    public void ResetLevel()
    {
        SceneLoader.Instance.ResetCurrentScene();
    }

    public void LoadGame()
    {
        SceneLoader.Instance.LoadScene("Level_1");
    }

    public void WinGame()
    {
        SceneLoader.Instance.LoadScene("GameOver");
    }

    public void SetNewCheckpoint(Transform checkPoint)
    {
        _currentCheckpoint = checkPoint;
    }
}
