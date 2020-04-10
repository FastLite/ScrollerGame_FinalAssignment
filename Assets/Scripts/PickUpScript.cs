using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PICKUP_TYPE { HEALTH, DAMAGE};
public class PickUpScript : MonoBehaviour
{
    public PICKUP_TYPE pickupType;

    GameManager gMrg;

    private void Awake()
    {
        gMrg = GameObject.FindObjectOfType<GameManager>();
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
            Destroy(gameObject);
        }
    }
    public void DoDoubleDamage()
    {
        gMrg.damage *= 2;
        Debug.Log("damage increased in 2 and now is " + gMrg.damage);
    }
    public void RestoreHealth()
    {
        gMrg.ResetHealth();
        Debug.Log("health restored");
    }



}
