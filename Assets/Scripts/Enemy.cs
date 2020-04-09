using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum ENEMY_TYPE { ONE_SHOT, HEALTH_TYPE, SHOOTINGWEEK_TYPE, SHOOTINGSTRONG_TYPE};
public class Enemy : MonoBehaviour
{
    public int pointCost = 5;

    public float fireRateDelay;

    public float randomFireDelayAdd;

    public int shootDamage;
    public int collisionDamage = 10;

    public ENEMY_TYPE enemyType;

    public GameObject explosionPrefab;

    public ShootScript shootPrefab;
    public Transform shootSpawnPoint;

    public GameManager gMrg;

    public Slider healthSlider;

    

    private void Awake()
    {
        gMrg = GameObject.FindObjectOfType<GameManager>();
        //gMrg.RegisterEnemy();

        if (enemyType == ENEMY_TYPE.HEALTH_TYPE)
        {
            SetValues(50, 0, 10, 10);
            SetHealthToDefault();
        }
        else if (enemyType == ENEMY_TYPE.SHOOTINGWEEK_TYPE)
        {
            SetValues(100, 10, 20, 20);
            SetFireRate(2.5f);
            SetHealthToDefault();
        }
        else if (enemyType == ENEMY_TYPE.SHOOTINGSTRONG_TYPE)
        {
            SetValues(125, 15, 30, 30);
            SetFireRate(4);
            SetHealthToDefault();
            
        }
        
    }

    private void Start()
    {


        if (enemyType == ENEMY_TYPE.SHOOTINGWEEK_TYPE || enemyType == ENEMY_TYPE.SHOOTINGSTRONG_TYPE)
        {
            InvokeRepeating("Fire", fireRateDelay + randomFireDelayAdd, fireRateDelay + randomFireDelayAdd);
        }
    }

    public void SetHealthToDefault()
    {
        healthSlider.value = healthSlider.maxValue;
    }
   
    public void SetValues(int maxHealthEnemy, int damageEnemy, int collisionDmg, int cost)
    {
        collisionDamage = collisionDmg;
        shootDamage = damageEnemy;
        healthSlider.maxValue = maxHealthEnemy;
        pointCost = cost;
    }

    public void SetFireRate(float rate)
    {
        fireRateDelay = rate;
    }

    void Fire()
    {
        randomFireDelayAdd = Random.Range(-1, 2);

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
        if (enemyType==ENEMY_TYPE.HEALTH_TYPE || enemyType == ENEMY_TYPE.SHOOTINGWEEK_TYPE || enemyType == ENEMY_TYPE.SHOOTINGSTRONG_TYPE)
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
        else if (enemyType == ENEMY_TYPE.ONE_SHOT )
        {
            return true;
        }
        else
        {
            Debug.Log("error, tupe not programmed" + enemyType);
            return false;
        }



    }






}
