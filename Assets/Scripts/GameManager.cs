﻿using System.Collections;
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
    public int highScore = 1;
    public TextMeshProUGUI scoreField;
    public TextMeshProUGUI highscoreField;

    [Header("Ship prefabs")]
    public GameObject fatPrefab;
    public GameObject fastPrefab;

    [Header("Totals")]
    public int totalLevels = 2;
    public int totalEnemiesHit;

    public int totalEnemiesToDestroy;

    [Header("Non specific UI")]


    [Header("Game state screens")]
    public GameObject gameOverScreen;
    public GameObject levelFailedScreen;
    public GameObject pauseGameScreen;

    public bool isGamePaused;





    void Start()
    {
        isGamePaused = false;
        int shipType = PlayerPrefs.GetInt("shipType");
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
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            DoSomethingWithpause();
        }
    }

    public void DoSomethingWithpause()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0;
            pauseGameScreen.SetActive(true);
            isGamePaused = true;
        }
        else if (isGamePaused)
        {
            Time.timeScale = 1;
            pauseGameScreen.SetActive(true);
            isGamePaused = false;
        }
        else
            return;

    }

    public void OnEnemyHit()
    {
        totalEnemiesHit++;
        score += 5; // each asteroid destroyed earns 5 points...

        CheckGameOver();
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
