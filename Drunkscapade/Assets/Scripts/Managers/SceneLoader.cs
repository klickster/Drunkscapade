using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private AsyncOperation _loadSceneOperation;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void ResetCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("Loading");
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(1f);

        _loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        if (!_loadSceneOperation.isDone)
            yield return null;
    }
}
