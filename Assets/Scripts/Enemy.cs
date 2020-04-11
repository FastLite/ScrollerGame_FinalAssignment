using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum ENEMY_TYPE { ONE_SHOT, HEALTH_TYPE, SHOOTINGWEEK_TYPE, SHOOTINGSTRONG_TYPE };
public class Enemy : MonoBehaviour
{
    [Header("basic stats")]
    public int pointCost = 5;
    public int maxHealthEnemy;
    public int collisionDamage = 10;
    public int shootDamage;

    [Header("rest")]
    public float fixedFireRateDelay;

    public float randomFireDelayAdd;
    public float finalDelay;


    public ENEMY_TYPE enemyType;

    public GameObject explosionPrefab;

    public ShootScript shootPrefab;
    public Transform shootSpawnPoint;
    public Transform pickupSpawnPoint;

    public GameManager gMrg;

    public Slider healthSlider;

    public bool isInView = false;


    private void Awake()
    {
        gMrg = GameObject.FindObjectOfType<GameManager>();
        gMrg.RegisterEnemy();

        //Set values on prefab insted of scripts

        if (enemyType == ENEMY_TYPE.HEALTH_TYPE)
        {

            healthSlider.maxValue = maxHealthEnemy;
            SetHealthToDefault();
        }
        else if (enemyType == ENEMY_TYPE.SHOOTINGWEEK_TYPE)
        {

            healthSlider.maxValue = maxHealthEnemy;
            SetHealthToDefault();
            SetRandomDelayAndCombine(2.5f);
        }
        else if (enemyType == ENEMY_TYPE.SHOOTINGSTRONG_TYPE)
        {

            healthSlider.maxValue = maxHealthEnemy;
            SetHealthToDefault();
            SetRandomDelayAndCombine(4.0f);

        }

    }

    public void SetRandomDelayAndCombine(float rate)
    {
        fixedFireRateDelay = rate;

        randomFireDelayAdd = Random.Range(-1.0f, 2.1f);
        finalDelay = fixedFireRateDelay + randomFireDelayAdd;
    }



    public void SetHealthToDefault()
    {
        healthSlider.value = healthSlider.maxValue;
    }

    void Fire()
    {


        ShootScript go = Instantiate(shootPrefab, shootSpawnPoint);

        go.transform.parent = null;

        go.GetComponent<Rigidbody2D>().AddForce(transform.up * go.launchForce * 1);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 2);

            gMrg.HealthSlider.value -= collisionDamage;

            gMrg.OnEnemyDestroy();
            gMrg.score = -pointCost;

            // Before destruction enemy should deal collision damage to the player


            Destroy(gameObject);
        }
    }
    private void LateUpdate()
    {
        if (enemyType == ENEMY_TYPE.SHOOTINGWEEK_TYPE || enemyType == ENEMY_TYPE.SHOOTINGSTRONG_TYPE)
            healthSlider.transform.parent.rotation = Quaternion.identity;
        else
            return;
    }

    public bool IsEnemyKilled(int damage)
    {
        if (enemyType == ENEMY_TYPE.HEALTH_TYPE || enemyType == ENEMY_TYPE.SHOOTINGWEEK_TYPE || enemyType == ENEMY_TYPE.SHOOTINGSTRONG_TYPE)
        {
            healthSlider.value -= damage;

            if (healthSlider.value <= 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        else if (enemyType == ENEMY_TYPE.ONE_SHOT)
        {
            return true;
        }
        else
        {
            Debug.Log("error, tupe not programmed" + enemyType);
            return false;
        }



    }


    public void SpawnPickup()
    {
        if (Random.Range(0, 101) >= 1)
        {

           // go1.transform.parent = null;
            Instantiate(gMrg.pickUpList[Random.Range(0, gMrg.pickUpList.Count)], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
          
            Debug.Log("pickup spawned");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("TopTrigger"))
        {
            isInView = true;
            Debug.Log("enemy collided with top trigger");

            if (enemyType == ENEMY_TYPE.SHOOTINGWEEK_TYPE || enemyType == ENEMY_TYPE.SHOOTINGSTRONG_TYPE)
            {


                InvokeRepeating("Fire", finalDelay, finalDelay);
            }
        }
    }

}
