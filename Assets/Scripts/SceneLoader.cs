using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public int currentLevelNumber;



    void LoadLevel()
    {
        string fileName = "MainLevel" + currentLevelNumber;
        SceneManager.LoadScene(fileName, LoadSceneMode.Additive);
    }


    public void OnLoadSceneCalledAdditive(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum, LoadSceneMode.Additive);
    }

    public void OnLoadSceneCalled(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);

    }

    public void ReloadCurrentLevel()
    {
        string fileName = "Level" + currentLevelNumber;
        SceneManager.LoadScene(1);
        LoadLevel();
    }
    public void LoadNextLevel()
    {
        string fileName = "Level" + currentLevelNumber;
        SceneManager.UnloadSceneAsync(fileName);
        currentLevelNumber++;
        LoadLevel();
    }

}
