using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public int damage;

    public GameObject explosionEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {


            GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 2);


            Destroy(collision.gameObject);

            Destroy(gameObject);


            //GameObject.FindObjectOfType<GameManager>().OnEnemyHit();  

        }

    }


}
