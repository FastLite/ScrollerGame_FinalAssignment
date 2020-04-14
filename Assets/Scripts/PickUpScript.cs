using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PICKUP_TYPE { HEALTH, DAMAGE };
public class PickUpScript : MonoBehaviour
{
    public PICKUP_TYPE pickupType;

    public GameObject healPrefab;
    public GameObject DDPrefab;
    public int damageIncreaseRate;


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
        if (collision.gameObject.CompareTag("BottomTrigger"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pickupType == PICKUP_TYPE.HEALTH)
            {
                RestoreHealth();
                GameObject go = Instantiate(healPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
                Destroy(go, 2);
            }

            else if (pickupType == PICKUP_TYPE.DAMAGE)
            {

                DoDoubleDamage();
                GameObject go = Instantiate(DDPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
                Destroy(go, 2);
            }

        }
        Destroy(gameObject);

    }
    public void DoDoubleDamage()
    {
        gMrg.damage *= damageIncreaseRate;


        Debug.Log("damage increased in 2 and now is " + gMrg.damage);
    }
    public void RestoreHealth()
    {

        gMrg.ResetHealth();
        Debug.Log("health restored");
    }



}
