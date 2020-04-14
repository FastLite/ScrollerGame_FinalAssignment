using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public string level = "Level";
    public float LevelStartTime;

   
    public int currentLevelNumber;
    GameManager gMrg;

    private void Start()
    {
        CaptureStartLevelTime();
        currentLevelNumber = 1;
        gMrg = GameObject.FindObjectOfType<GameManager>();
    }

    public void CaptureStartLevelTime()
    {
        LevelStartTime = Time.time;
    }

    void LoadLevel()
    {
        string fileName = level + currentLevelNumber;
        CaptureStartLevelTime();
        SceneManager.LoadScene(fileName, LoadSceneMode.Additive);
        gMrg.canPauseBeCalled = true;
    }


    public void OnLoadSceneCalledAdditive(int sceneNum)
    {
        CaptureStartLevelTime();
        SceneManager.LoadScene(sceneNum, LoadSceneMode.Additive);
    }

    public void OnLoadSceneCalled(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
        CaptureStartLevelTime();
    }

    public void ReloadCurrentLevel()
    {
        
        string fileName = level + currentLevelNumber;
        SceneManager.UnloadSceneAsync(fileName);
        Debug.Log("Scene loaded : " + fileName);
        
        gMrg.ResetEverythingAtOnce();

        LoadLevel();
    }
    public void LoadNextLevel()
    {
        gMrg.MakeIsGameOverFalse();
        string fileName = level + currentLevelNumber;
        Time.timeScale = 0; //temporary solution
        SceneManager.UnloadSceneAsync(fileName);
        currentLevelNumber++;
        LoadLevel();
    }

}
