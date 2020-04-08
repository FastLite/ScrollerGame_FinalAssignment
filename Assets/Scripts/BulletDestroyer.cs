using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("object collided");
        if (collision.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
    
}
