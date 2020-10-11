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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LoadScene("Level_1");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("Loading");
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(1f);

        _loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        if (!_loadSceneOperation.isDone)
            yield return null;
    }
}
