using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{


    public GameObject explosionEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid")
        {


            GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 2);


            Destroy(collision.gameObject);

            Destroy(gameObject);


            //GameObject.FindObjectOfType<GameManager>().OnEnemyHit();  

        }

    }


}
