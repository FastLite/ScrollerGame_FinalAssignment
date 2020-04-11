using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PICKUP_TYPE { HEALTH, DAMAGE};
public class PickUpScript : MonoBehaviour
{
    public PICKUP_TYPE pickupType;

    GameObject healthEffect;
    GameObject DDEffect;

    public float fallingSpeed = 2f;

    GameManager gMrg;

    private void Awake()
    {
        gMrg = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        transform.Translate(0, -fallingSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pickupType == PICKUP_TYPE.HEALTH)
            {
                RestoreHealth();
            }
               
            else if (pickupType == PICKUP_TYPE.DAMAGE)
            {
                DoDoubleDamage();
            }
            
        }
        Destroy(gameObject);

    }
    public void DoDoubleDamage()
    {
        gMrg.damage *= 2;

        Instantiate(DDEffect, transform.position, Quaternion.identity);

        Destroy(DDEffect, 2);
        Debug.Log("damage increased in 2 and now is " + gMrg.damage);
    }
    public void RestoreHealth()
    {
        Instantiate(healthEffect, transform.position, Quaternion.identity);

        Destroy(healthEffect, 2);
        gMrg.ResetHealth();
        Debug.Log("health restored");
    }



}
