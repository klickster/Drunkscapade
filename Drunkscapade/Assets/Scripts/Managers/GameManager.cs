using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int _maxLevels;

    private int _levelIndex = 1;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LoadNextLevel();
    }

    public void WinCurrentLevel()
    {
        _levelIndex++;

        if(_levelIndex > _maxLevels)
        {
            WinGame();
        }
        else
        {
            LoadNextLevel();
        }
    }

    public void ResetLevel()
    {
        SceneLoader.Instance.ResetCurrentScene();
    }

    public void LoadNextLevel()
    {
        SceneLoader.Instance.LoadScene("Level_" + _levelIndex);
    }

    public void WinGame()
    {
        SceneLoader.Instance.LoadScene("GameOver");
    }
}
