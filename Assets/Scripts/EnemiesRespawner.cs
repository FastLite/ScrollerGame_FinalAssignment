using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesRespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float originalX = collision.transform.position.x;
            Debug.Log("Enemy triggered" + collision.gameObject);
            collision.gameObject.transform.position = new Vector3(originalX + Random.Range(-3,3), transform.position.y + Random.Range(40,60), 0);
        }
    }
    
    
}
