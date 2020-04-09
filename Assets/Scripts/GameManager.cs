using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{


    private SceneLoader sLdr;


    [Header("Health Settings")]
    public Slider HealthSlider;

    [Header("Scores")]
    public int score;
    public int highScore = 1;
    public TextMeshProUGUI scoreField;
    public TextMeshProUGUI highscoreField;

    [Header("Ship Statistics")]
    public int shipType;
    public int damage;


    [Header("Ship prefabs")]
    public GameObject fatPrefab;
    public GameObject fastPrefab;

    [Header("Totals")]
    public int totalLevels = 2;
    public int totalEnemiesToDestroy;
    public int totalEnemiesDestroyed;
    public int totalTimeForLevel = 20;

    [Header("Time")]
    public int LevelStartTime;
    public int timeMark2;
    public int timeToDisplay;
    public float elapsedTime;
    public TextMeshProUGUI TimeLeft;

    [Header("Non specific UI")]


    [Header("Game state screens")]
    public GameObject gameWinScreen;
    public GameObject levelCompletedScreen;
    public GameObject levelFailedScreen;
    public GameObject pauseGameScreen;

    [Header("Boolians")]
    public bool isGamePaused;
    public bool shouldBossAppear;
    public bool wasHitByFast;
    public bool isbossKilled;
    





    void Start()
    {


        sLdr = GameObject.FindObjectOfType<SceneLoader>();

        isGamePaused = false;


        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }

        shipType = PlayerPrefs.GetInt("shipType");
        Vector3 innitialPos = new Vector3(0, -5f, 0);
        GameObject ship;

        switch (shipType)
        {
            case 1:
                ship = Instantiate(fatPrefab, innitialPos, Quaternion.identity);

                break;
            case 2:
                ship = Instantiate(fastPrefab, innitialPos, Quaternion.identity);
                break;


        }


        switch (shipType)
        {
            case 1:
                PlayerControllerFat shipFAT = GameObject.FindObjectOfType<PlayerControllerFat>();



                damage = shipFAT.bulletDamage;
                HealthSlider.maxValue = shipFAT.maximumHealth;

                Debug.Log("Ship FAT generated");

                break;
            case 2:
                PlayerControllerFast shipFAST = GameObject.FindObjectOfType<PlayerControllerFast>();

                wasHitByFast = false;

                damage = shipFAST.bulletDamage;
                HealthSlider.maxValue = shipFAST.maximumHealth;

                Debug.Log("Ship fast generated");

                break;
        }


        ResetHealth();

    }
    void Update()
    {
        if (score > highScore)
        {
            highScore = score;
        }
        elapsedTime = Time.time - sLdr.LevelStartTime;
        if (!shouldBossAppear)
        {
            timeToDisplay = Mathf.RoundToInt(totalTimeForLevel - elapsedTime);
            TimeLeft.text = "Time Left: " + timeToDisplay.ToString();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            DoSomethingWithpause();
        }


    }
    private void FixedUpdate()
    {
        if (elapsedTime > 20)
        {
            levelFailed();
        }
        else
        {
            return;
        }
    }

    public void ResetHealth()
    {
        HealthSlider.value = HealthSlider.maxValue;
    }

    public void ChangePlayersHealth(int healthChange)
    {

        HealthSlider.value += healthChange;

        Debug.Log("Health changed on value of" + healthChange);

        if (HealthSlider.value <= 0)
        {
            levelFailed();
            ResetHealth();
        }

    }


    public void DoSomethingWithpause()
    {
        if (!isGamePaused)
        {
            CallPause();
            pauseGameScreen.SetActive(true);
            isGamePaused = true;
        }
        else if (isGamePaused)
        {
            RemovePause();
            pauseGameScreen.SetActive(false);
            isGamePaused = false;
        }
        else
            return;

    }

    public void OnEnemyDestroy()
    {
        if (shipType == 2)
        {
            if (!wasHitByFast)
            {
                totalEnemiesDestroyed++;
                wasHitByFast = true;
            }
            else if (wasHitByFast)
            {
                wasHitByFast = false;
            }
        }
        else
        {
            totalEnemiesDestroyed++;
            score += GameObject.FindObjectOfType<Enemy>().pointCost; // each asteroid destroyed earns 5 points...
        }
        CheckGameOver();
    }

    void levelFailed()
    {
        Debug.Log("level failed.. restart?");
        levelFailedScreen.SetActive(true);
        CallPause();
    }


    public void RemovePause()
    {
        Time.timeScale = 1;
    }
    public void CallPause()
    {
        Time.timeScale = 0;
    }

    public void RegisterEnemy()
    {
        totalEnemiesToDestroy++;
    }
    void CheckGameOver()
    {
        if ((totalEnemiesDestroyed) == totalEnemiesToDestroy)
        {
            shouldBossAppear = true;
            Debug.Log(sLdr.currentLevelNumber);
            if (sLdr.currentLevelNumber < totalLevels)
            {
                if (highScore > PlayerPrefs.GetInt("highScore"))
                {
                    PlayerPrefs.SetInt("highScore", highScore);
                }
                Debug.Log("succesfssully completed..");

                scoreField.text = "current score is: " + score.ToString();
                highscoreField.text = "All time highscore is: " + PlayerPrefs.GetInt("highScore").ToString();

                ResetHealth();

                sLdr.LoadNextLevel();
                shouldBossAppear = false;
                CallPause();
                levelCompletedScreen.SetActive(true);
            }
            else
            {
                CallPause();
                gameWinScreen.SetActive(true);
            }

        }
        
    }
}
