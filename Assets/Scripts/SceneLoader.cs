using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public string level = "Level";

   
    public int currentLevelNumber;



    void LoadLevel()
    {
        string fileName = level + currentLevelNumber;
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
        string fileName = level + currentLevelNumber;
        
        LoadLevel();
    }
    public void LoadNextLevel()
    {
        string fileName = level + currentLevelNumber;
        SceneManager.UnloadSceneAsync(fileName);
        currentLevelNumber++;
        LoadLevel();
    }

}
