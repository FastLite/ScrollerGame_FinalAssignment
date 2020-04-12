using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{


    public List<PickUpScript> pickUpList;

    private SceneLoader sLdr;

    public Vector3 initialPos = new Vector3(0, -5f, 0);

    [Header("Health Settings")]
    public Slider HealthSlider;

    [Header("Scores")]
    public int score;
    public int highScore = 1;
    public TextMeshProUGUI scoreField;
    public TextMeshProUGUI finaScorelField;
    public TextMeshProUGUI highscoreField;
    public TextMeshProUGUI finalHighscoreField;

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
    

    [Header("Time")]
    public int LevelStartTime;
    public int timeForLevel;
    public int timeToDisplay;
    public float elapsedTime;
    public TextMeshProUGUI TimeLeft;

    [Header("Non specific")]
    public GameObject pickupToinstantiate;

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
    public bool canPauseBeCalled;
    public bool isGameOver;

    [Header("Audio")]

    public AudioClip MusicFirstLevel;
    public AudioClip MusicSecondLevel;

    public AudioSource sourceOfAudio;



    void Start()
    {
        sourceOfAudio = gameObject.GetComponent<AudioSource>();


        sourceOfAudio.clip = MusicFirstLevel;
        sourceOfAudio.Play();

        isGameOver = false;
        canPauseBeCalled = true;

        sLdr = GameObject.FindObjectOfType<SceneLoader>();

        isGamePaused = false;


        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }

        shipType = PlayerPrefs.GetInt("shipType");



        


        switch (shipType)
        {
            case 1:
                Instantiate(fatPrefab, initialPos, Quaternion.identity);
                PlayerControllerFat shipFAT = GameObject.FindObjectOfType<PlayerControllerFat>();



                damage = shipFAT.bulletDamage;
                HealthSlider.maxValue = shipFAT.maximumHealth;

                Debug.Log("Ship FAT generated");

                break;
            case 2:
                Instantiate(fastPrefab, initialPos, Quaternion.identity);
                PlayerControllerFast shipFAST = GameObject.FindObjectOfType<PlayerControllerFast>();

               

                damage = shipFAST.bulletDamage;
                wasHitByFast = false;
                HealthSlider.maxValue = shipFAST.maximumHealth;

                Debug.Log("Ship fast generated");

                break;
        }


        ResetHealth();

    }

    
    void Update()
    {
        if (!isGameOver)
        CheckGameOver();

        if (score > highScore)
        {
            highScore = score;
        }
        elapsedTime = Time.time - sLdr.LevelStartTime;
        if (!shouldBossAppear)
        {
            timeToDisplay = Mathf.RoundToInt(timeForLevel - elapsedTime);
            TimeLeft.text = "Time Left: " + timeToDisplay.ToString() + " sec";
        }

        if (Input.GetKeyUp(KeyCode.Escape) && canPauseBeCalled)
        {
            DoSomethingWithpause();
        }


    }
    private void FixedUpdate()
    {
        if (elapsedTime > timeForLevel)
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
        
            totalEnemiesDestroyed++;
            score += GameObject.FindObjectOfType<Enemy>().pointCost; // each asteroid destroyed earns 5 points...
        
        
    }

    void levelFailed()
    {
        isGameOver = true;
        Debug.Log("level failed.. restart?");
        ResetPlayerPosition();
        levelFailedScreen.SetActive(true);
        CallPause();
        canPauseBeCalled = false;
        
    }

    public void ResetPlayerPosition()
    {
        GameObject ship;
        ship = GameObject.FindGameObjectWithTag("Player");

        ship.transform.position = initialPos;
    }
    

    public void RemovePause()
    {
        Time.timeScale = 1;
    }
    public void CallPause()
    {
        Time.timeScale = 0;
    }


    public void ResetEverythingAtOnce()
    {
        ResetHealth();
        ResetPlayerPosition();
        totalEnemiesToDestroy = 0;
        totalEnemiesDestroyed = 0;
        switch (shipType)
        {
            case 1:
                PlayerControllerFat shipFAT = GameObject.FindObjectOfType<PlayerControllerFat>();
                damage = shipFAT.bulletDamage;

                break;
            case 2:
                PlayerControllerFast shipFAST = GameObject.FindObjectOfType<PlayerControllerFast>();
                damage = shipFAST.bulletDamage;

                break;
        }
    }

    public void PauseCanBeCalledAgain()
    {
        canPauseBeCalled = true;
    }

    public void OnLevelComplete()
    {
        isGameOver = true;
        MusicFirstLevel.UnloadAudioData();

        sourceOfAudio.clip = MusicSecondLevel;




        sLdr.LoadNextLevel();
        shouldBossAppear = false;
        CallPause();
        levelCompletedScreen.SetActive(true);
        isGameOver = false;
        ResetEverythingAtOnce();


    }
    public void RegisterEnemy()
    {
        totalEnemiesToDestroy++;
        Debug.Log("enemy registred");
    }
    void CheckGameOver()
    {
        if (HealthSlider.value <= 0)
        {
            levelFailed();
        }
        if ((totalEnemiesDestroyed) == totalEnemiesToDestroy)
        {
            score += Mathf.RoundToInt(timeForLevel - elapsedTime) * 10;
            canPauseBeCalled = false;
            shouldBossAppear = true;
            Debug.Log(sLdr.currentLevelNumber);
            if (sLdr.currentLevelNumber < totalLevels)
            {
                if (highScore > PlayerPrefs.GetInt("highScore"))
                {
                    PlayerPrefs.SetInt("highScore", highScore);
                }
                Debug.Log("succesfssully completed..");

                sourceOfAudio.clip = MusicSecondLevel;
                sourceOfAudio.Play();

                scoreField.text = "current score is: " + score.ToString();
                highscoreField.text = "All time highscore is: " + PlayerPrefs.GetInt("highScore").ToString();


                OnLevelComplete();
            }
            else
            {
                finaScorelField.text = "Your final score is: " + score.ToString();
                finalHighscoreField.text = "All time highscore is: " + PlayerPrefs.GetInt("highScore").ToString();
                sourceOfAudio.Stop();
                CallPause();
                gameWinScreen.SetActive(true);
            }

        }

    }
}
