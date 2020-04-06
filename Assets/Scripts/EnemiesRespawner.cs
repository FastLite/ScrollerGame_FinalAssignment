using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesRespawner : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collided" + collision.gameObject);
            //collision.gameObject.transform.position = new Vector3(, y + 20,);
        }
    }
}
