using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{

    [Header("Health Settings")]
    public int maxHealth;
    public int currentHealth;

    public Slider remainingHealthSlider;

    [Header("Scores")]
    public int score;
    public int highScore;
    public TextMeshProUGUI scoreField;
    public TextMeshProUGUI highscoreField;

    [Header("Ship prefabs")]
    public GameObject fatPrefab;
    public GameObject fastPrefab;

    public int totalLevels = 2;

    public int totalEnemiesToDestroy;

    [Header("Non specific UI")]
    

    [Header("Game state screens")]
    public GameObject gameOverScreen;
    public GameObject levelFailedScreen;


















    void Start()
    {
        int shipType = FindObjectOfType<SceneLoader>().shipIndex;
        Vector3 innitialPos = new Vector3(0, -5f, 0);
        GameObject ship;
        if (shipType == 1)
        {
            ship = Instantiate(fatPrefab, innitialPos, Quaternion.identity);

            Debug.Log("Ship FAT generated");
        }
        else
        {
            ship = Instantiate(fastPrefab, innitialPos, Quaternion.identity);

            Debug.Log("Ship fast generated");
        }

        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }
    }

   
    void Update()
    {
        
    }


    public void RegisterEnemy()
    {
        totalEnemiesToDestroy++;
    }
    void CheckGameOver()
    {
       
        //if ((totalEnemiesHit + totalEnemiesMissed) == totalEnemiesToDestroy)
        //{
        //    if (currentLevelNumber < totalLevels)
        //    {
                
        //        if (totalEnemiesHit == totalEnemiesToDestroy)
        //        {
        //            Debug.Log("succesfssully completed..");
        //            levelCompletedScreen.SetActive(true);
        //        }
        //        else 
        //        {
        //            Debug.Log("level failed.. restart?");
        //            levelFailedScreen.SetActive(true);
        //        }
                
        //    }
        //    else  
        //        gameOverScreen.SetActive(true);
        //}

    }
}
