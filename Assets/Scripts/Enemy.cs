using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void Awake()
    {
        GameObject.FindObjectOfType<GameManager>().RegisterEnemy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 2);


            
            // Before destruction enemy should deal collision damage to the player


            Destroy(gameObject);
        }
    }
}
